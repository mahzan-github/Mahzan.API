using Dapper;
using Mahzan.Dapper.DTO.Users.SignUp;
using Mahzan.Dapper.Rules.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Repositories.Users.SignUp
{
    public class SignUpRepository : DataConnection, ISignUpRepository
    {
        private readonly ISignUpRules _signUpRules;

        public SignUpRepository(
            IDbConnection dbConnection,
            ISignUpRules signUpRules) : base(dbConnection)
        {
            _signUpRules = signUpRules;
        }

        public async Task<Models.Entities.Users> HandleRepository(SignUpDto signUpDto)
        {
            Guid userId;

            //Ejecuta reglas
            await _signUpRules
                .HandleRules(signUpDto);

            using (var transaction = Connection.BeginTransaction())
            {

                //Inserta User
                userId = await InsertUser(signUpDto);

                //Inserta Member
                await InsertMember(userId, signUpDto);

                //Inserta User Role
                await InsertUserRole(userId);

                transaction.Commit();
            }

            StringBuilder sqlUser = new StringBuilder();
            sqlUser.Append("Select * from Users ");
            sqlUser.Append("Where user_id = @user_id ");

            IEnumerable<Models.Entities.Users> user;
            user = await Connection
                .QueryAsync<Models.Entities.Users>(
                    sqlUser.ToString(),
                    new
                    {
                        user_id = userId
                    }
                );

            return user.FirstOrDefault();
        }

        private async Task<Guid> InsertUser(SignUpDto signUpDto)
        {


            StringBuilder insertUser = new StringBuilder();
            insertUser.Append("Insert into users ");
            insertUser.Append("(");
            insertUser.Append("user_id,");
            insertUser.Append("user_name,");
            insertUser.Append("password,");
            insertUser.Append("active,");
            insertUser.Append("confirm_email,");
            insertUser.Append("token_confirm_email,");
            insertUser.Append("email");
            insertUser.Append(") ");
            insertUser.Append("Values ");
            insertUser.Append("(");
            insertUser.Append("@user_id,");
            insertUser.Append("@user_name,");
            insertUser.Append("@password,");
            insertUser.Append("@active,");
            insertUser.Append("@confirm_email,");
            insertUser.Append("@token_confirm_email,");
            insertUser.Append("@email");
            insertUser.Append(") ");
            insertUser.Append("RETURNING user_id; ");


            Guid userId = await Connection
                .ExecuteScalarAsync<Guid>(
                    insertUser.ToString(),
                    new
                    {
                        user_id = Guid.NewGuid(),
                        user_name = signUpDto.UserName,
                        password = signUpDto.Password,
                        active = true,
                        confirm_email = false,
                        token_confirm_email = Guid.NewGuid(),
                        email = signUpDto.Email
                    }
                );

            return userId;
        }

        private async Task InsertMember(Guid userId, SignUpDto signUpDto)
        {


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



            await Connection
                .ExecuteScalarAsync<Guid>(
                    insertMember.ToString(),
                    new
                    {
                        member_id = Guid.NewGuid(),
                        name = signUpDto.Name,
                        phone = signUpDto.Password,
                        user_id = userId
                    }
                );
        }

        private async Task InsertUserRole(Guid userId)
        {
            StringBuilder sqlUser = new StringBuilder();
            sqlUser.Append("Insert into user_role ");
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
    }
}
