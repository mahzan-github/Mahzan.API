using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Models.Entities;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Exeptions.User.LogIn;
using Mahzan.Persistance.V1.Filters.User;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.ViewModel.User;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.User.LogIn
{
    public class LogInRepository:BaseUpdateRepository<LogInDto>,
        ILogInRepository
    {
        public LogInRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<LogInDto> UpdateInternal(LogInDto dto)
        {
            Users user = await FindUser(dto);

            await UpdateUserLastLoginAt(user);

            Members member = await FindMember(user);

            Roles role = await FindRoleName(user);

            return dto with
            {
                UserId = user.UserId,
                MemberId = member.MemberId,
                RoleName = role.Name
            };
        }

        protected override void HandlePrevalidations(LogInDto dto)
        {
            //El usuario debe existir
            if (!UserExist(dto.UserName))
            {
                throw new LoginArgumentException(
                    $"El usuario {dto.UserName} no ha sido registrado."
                );
            }

            if (!UserIsActive(dto.UserName))
            {
                throw new LoginInvalidOperationException(
                    $"No es posible iniciar la sesión, por favor confirma tu correo."
                );
            }
        }
        
        #region :: Prevalidations ::
        
        private bool UserIsActive(string userName)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();

            sql.Append("Select * from users ");
            sql.Append("where user_name = @user_name ");


            IEnumerable<Users> users = Connection
                .Query<Users>(
                    sql.ToString(),
                    new
                    {
                        user_name = userName
                    }
                );

            if (users.Any())
            {
                if (users.FirstOrDefault().ConfirmEmail)
                {
                    return true;
                }
            }

            return result;
        }

        private bool UserExist(string userName)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();

            sql.Append("Select * from users ");
            sql.Append("where user_name = @user_name ");


            IEnumerable<Users> users =  Connection
                .Query<Users>(
                    sql.ToString(),
                    new
                    {
                        user_name = userName
                    }
                );

            if (users.Any())
            {

                return true;

            }

            return result;
        }
        
        #endregion

        #region :: Log In Steps ::

        private async Task<Users> FindUser(LogInDto dto)
        {
            string sql = @"
                select  * 
                from users 
                where   user_name=@user_name
                and     password= @password
            ";

            IEnumerable<Users> users = await Connection
                .QueryAsync<Users>(
                    sql,
                new {
                    user_name = dto.UserName,
                    password =   dto.Password
                }
            );

            if (!users.Any())
            {
                throw new LoginArgumentException(
                    $"No fue posible iniciar sesión, por favor revisa tu usuario y contraseña."
                );
            }

            return users.FirstOrDefault();
        }

        private async Task UpdateUserLastLoginAt(Users user)
        {
            string sql = @"
                update users set last_login_at=@last_login_at
                where user_id = @user_id
            ";

            await Connection
                .ExecuteAsync(
                    sql,
                    new
                    {
                        last_login_at = DateTimeOffset.Now,
                        user_id = user.UserId
                    }
                );
        }

        private async Task<Members> FindMember(Users user)
        {
            string sql = @"
                select  * 
                from    members 
                where   user_id=@user_id
            ";

            IEnumerable<Members> members = await Connection
                .QueryAsync<Members>(
                    sql,
                    new {
                        user_id = user.UserId
                    }
                );

            if (!members.Any())
            {
                throw new LoginArgumentException(
                    $"No fue posible encontrar el usuario como miembro de mahzan."
                );
            }

            return members.FirstOrDefault();   
        }

        private async Task<Roles> FindRoleName(Users user)
        {
            string sql = @"
                select  * 
                from    roles
                inner join user_role on user_role.role_id = roles.role_id
                where   user_role.user_id=@user_id
            ";

            IEnumerable<Roles> userRole = await Connection
                .QueryAsync<Roles>(
                    sql,
                    new {
                        user_id = user.UserId
                    }
                );

            if (!userRole.Any())
            {
                throw new LoginArgumentException(
                    $"No fue posible encontrar un role asignado para el usuario."
                );
            }

            return userRole.FirstOrDefault();      
        }

        #endregion
    }
}