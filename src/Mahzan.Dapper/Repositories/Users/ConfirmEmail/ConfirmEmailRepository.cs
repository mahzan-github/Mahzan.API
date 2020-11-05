using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Dapper.Exceptions.Users.ConfirmEmail;

namespace Mahzan.Dapper.Repositories.Users.ConfirmEmail
{
    public class ConfirmEmailRepository : DataConnection, IConfirmEmailRepository
    {
        public ConfirmEmailRepository(
            IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task HandleRepository(Guid userId, Guid tokenConfirmEmail)
        {
            using (var transaction = Connection.BeginTransaction())
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("Select * from users ");
                sql.Append("Where user_id = @user_id ");
                sql.Append("And token_confirm_email = @token_confirm_email ");

                IEnumerable<Models.Entities.Users> users;
                users = await Connection
                    .QueryAsync<Models.Entities.Users>(
                        sql.ToString(),
                        new
                        {
                            user_id = userId,
                            token_confirm_email = tokenConfirmEmail
                        }
                    );

                if (!users.Any())
                {
                    throw new ConfirmEmailInvalidOperationException(
                        $"No fue posible confirmar tu email, usuario o token no válido"
                        );
                }
                else
                {
                    StringBuilder updateUser = new StringBuilder();
                    updateUser.Append("Update   users ");
                    updateUser.Append("Set      confirm_email = true ");
                    updateUser.Append("where    user_id=@user_id ");

                    await Connection
                        .ExecuteAsync(
                            updateUser.ToString(),
                            new
                            {
                                user_id = userId
                            });
                }

                transaction.Commit();
            }


        }
    }
}
