using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Models.Entities;
using Mahzan.Models.Enums.MembersLicense;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.Exeptions.User;
using Mahzan.Persistance.V1.Exeptions.User.SignUp;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.User.SignUp
{
    public class SignUpRepository:BaseInsertRepository<SignUpDto>,
    ISignUpRepository
    {
        public SignUpRepository(
            NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<SignUpDto> InsertInternal(SignUpDto dto)
        {

            Guid userId = await InsertInUser(dto);

            Guid memberId = await InsertInMember(userId, dto);

            await InsertUserRole(userId);

            await InsertIntoMembersRoles(userId);

            return dto with
            {
                UserId = userId
            };
        }

        protected override async void HandlePrevalidations(SignUpDto dto)
        {
            if (UserNameExist(dto.UserName))
            {
                throw new SignUpArgumentException(
                    $"El usuario {dto.UserName} ya existe."
                );  
            }
            
        }

        #region :: Create New User Steps ::

        private async Task<Guid> InsertInUser(SignUpDto dto)
        {
            string sql = @"
                insert into users(
                    user_id,
                    user_name,
                    password,
                    active,
                    confirm_email,
                    token_confirm_email,
                    email,
                    created_at) 
                values (
                    @user_id,
                    @user_name,
                    @password,
                    @active,
                    @confirm_email,
                    @token_confirm_email,
                    @email,
                    @created_at) 
                returning user_id;";
            
            Guid userId = await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new
                    {
                        user_id = Guid.NewGuid(),
                        user_name = dto.UserName,
                        password = dto.Password,
                        active = true,
                        confirm_email = false,
                        token_confirm_email = Guid.NewGuid(),
                        email = dto.Email,
                        created_at = DateTimeOffset.Now
                    }
                );

            return userId;
        }

        private async Task<Guid> InsertInMember(Guid userId, SignUpDto dto)
        {
            Guid memberId = Guid.Empty;

            StringBuilder insertMember = new StringBuilder();
            insertMember.Append("Insert into members");
            insertMember.Append("(");
            insertMember.Append("member_id,");
            insertMember.Append("name,");
            insertMember.Append("phone,");
            insertMember.Append("user_id");
            insertMember.Append(")");
            insertMember.Append("Values ");
            insertMember.Append("(");
            insertMember.Append("@member_id,");
            insertMember.Append("@name,");
            insertMember.Append("@phone,");
            insertMember.Append("@user_id");
            insertMember.Append(") ");
            insertMember.Append("returning member_id ");

            memberId = await Connection
                .ExecuteScalarAsync<Guid>(
                    insertMember.ToString(),
                    new
                    {
                        member_id = Guid.NewGuid(),
                        name = dto.Name,
                        phone = dto.Phone,
                        user_id = userId
                    }
                );

            return memberId;    
        }

        private async Task InsertUserRole(Guid userId)
        {
            StringBuilder sqlUser = new StringBuilder();
            sqlUser.Append("insert into user_role ");
            sqlUser.Append("(");
            sqlUser.Append("user_id,");
            sqlUser.Append("role_id");
            sqlUser.Append(") ");
            sqlUser.Append("Values ");
            sqlUser.Append("(");
            sqlUser.Append("@user_id,");
            sqlUser.Append("@role_id");
            sqlUser.Append(")");

            await Connection
                .QueryAsync<Models.Entities.Users>(
                    sqlUser.ToString(),
                    new
                    {
                        user_id = userId,
                        role_id = new Guid("fb4b765a-7fb9-4293-a548-924f6fc6dfb2")
                    }
                );
        }
        
        private async Task InsertIntoMembersRoles(
            Guid memberId) 
        {
            string sql = @"
                insert into members_license
                (
                member_license_id,
                license_type,
                created_at,
                start_license_at,
                end_license_at,
                member_id
                )
                values
                (
                @member_license_id,
                @license_type,
                @created_at,
                @start_license_at,
                @end_license_at,
                @member_id
                )
                returning member_license_id;
            ";

            await Connection
                .ExecuteScalarAsync<Guid>(
                    sql,
                    new {
                        member_license_id = Guid.NewGuid(),
                        license_type = LicenseTypeEnum.TRIAL_LICENSE.ToString(),
                        created_at = DateTimeOffset.Now,
                        start_license_at = DateTimeOffset.Now,
                        end_license_at = DateTimeOffset.Now.AddDays(30),
                        member_id = memberId
                    }
                );
        }

        #endregion

        #region :: Prevalidations ::

        private bool UserNameExist(string userName)
        {
            
            bool result = false;

            string sql = @"select * from users where user_name = @user_name ";
            
            IEnumerable<Users> users;
            users = Connection
                .Query<Users>(
                    sql,
                    new
                    {
                        user_name = userName
                    }
                );

            if (users.Any())
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}