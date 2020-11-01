using Dapper;
using Mahzan.Dapper.DTO.Users.SignUp;
using Mahzan.Dapper.Exceptions.Users.SignUp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Rules.Users.SignUp
{
    public class SignUpRules : DataConnection, ISignUpRules
    {
        public SignUpRules(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task HandleRules(SignUpDto signUpDto)
        {
            //Valida que el usuario no exista
            if (await UserNameExist(signUpDto.UserName))
            {
                throw new SignUpArgumentException(
                    $"El usuario {signUpDto.UserName} ya existe."
                    );
            }

            //Valida que el usuario no se encuentre inactivo

        }

        private async Task<bool> UserNameExist(string userName)
        {
            bool result = false;

            StringBuilder sql = new StringBuilder();
            sql.Append("Select * from users ");
            sql.Append("Where user_name = @user_name ");

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
                result = true;
            }

            return result;
        }

    }
}
