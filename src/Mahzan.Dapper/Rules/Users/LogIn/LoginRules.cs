using Dapper;
using Mahzan.Dapper.DTO.Users.LogIn;
using Mahzan.Dapper.Exceptions.Users.LogIn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Rules.Users.LogIn
{
    public class LoginRules : DataConnection, ILoginRules
    {
        public LoginRules(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task HandleRules(LoginDto loginDto)
        {
            //El usuario debe existir
            if (!await UserExist(loginDto.UserName))
            {
                throw new LoginArgumentException(
                    $"El usuario {loginDto.UserName} no ha sido registrado."
                    );
            }

            if (!await UserIsActive(loginDto.UserName))
            {
                throw new LoginInvalidOperationException(
                    $"No es posible iniciar la sesión, por favor confirma tu correo."
                    );
            }
        }

        private async Task<bool> UserIsActive(string userName)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();

            sql.Append("Select * from users ");
            sql.Append("where user_name = @user_name ");

            IEnumerable<Models.Entities.Users> users;
            users = await Connection
                .QueryAsync<Models.Entities.Users>(
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

        private async Task<bool> UserExist(string userName)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();

            sql.Append("Select * from users ");
            sql.Append("where user_name = @user_name ");

            IEnumerable<Models.Entities.Users> users;
            users = await Connection
                .QueryAsync<Models.Entities.Users>(
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
    }
}
