using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mahzan.Dapper.DTO.Users.LogIn;
using Mahzan.Dapper.Exceptions.Users.LogIn;
using Mahzan.Dapper.Rules.Users.LogIn;

namespace Mahzan.Dapper.Repositories.Users.Login
{
    public class LoginRepository : DataConnection, ILoginRepository
    {
        private readonly ILoginRules _loginRules;

        public LoginRepository(
            IDbConnection dbConnection,
            ILoginRules loginRules) : base(dbConnection)
        {
            _loginRules = loginRules;
        }

        public async Task<Models.Entities.Users> HandleRepository(LoginDto loginDto)
        {

            await _loginRules.HandleRules(loginDto);

            StringBuilder sql = new StringBuilder();
            sql.Append("Select  * ");
            sql.Append("From    users ");
            sql.Append("Where   user_name = @user_name ");
            sql.Append("And     password = @password");

            IEnumerable<Models.Entities.Users> users;
            users = await Connection
                .QueryAsync<Models.Entities.Users>(
                    sql.ToString(),
                    new
                    {
                        user_name = loginDto.UserName,
                        password = loginDto.Password
                    }
                );

            if (!users.Any())
            {
                throw new LoginArgumentException(
                    $"No fue posible iniciar sesión, por favor verifica tu usuario y contraseña."
                    );
            }


            return users.FirstOrDefault();
        }
    }
}
