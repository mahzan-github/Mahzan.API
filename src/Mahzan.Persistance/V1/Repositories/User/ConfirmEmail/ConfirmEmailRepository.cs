using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Mahzan.Models.Entities;
using Mahzan.Persistance.V1.Dto.User;
using Mahzan.Persistance.V1.Exeptions.User.ConfirmEmail;
using Mahzan.Persistance.V1.Filters.User;
using Mahzan.Persistance.V1.Repositories._Base;
using Mahzan.Persistance.V1.ViewModel.User;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories.User.ConfirmEmail
{
    public class ConfirmEmailRepository:BaseUpdateRepository<ConfirmEmailDto>,
        IConfirmEmailRepository
    {
        public ConfirmEmailRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        protected override async Task<ConfirmEmailDto> UpdateInternal(ConfirmEmailDto dto)
        {
            Users user = await FindUser(
                new Guid(dto.UserId),
                new Guid(dto.TokenConfrimEmail)
                );

            return await  UpdateUser(user, dto);
        }

        protected override void HandlePrevalidations(ConfirmEmailDto dto)
        {
            if (!IsUserIdValid(dto.UserId))
            {
                throw new ConfirmEmailArgumentException(
                    $"El UserId {dto.UserId} no es un Guid válido."
                    );
            }

            if (!IsTokenConfirmEmailValid(dto.TokenConfrimEmail))
            {
                throw new ConfirmEmailArgumentException(
                    $"El TokenConfrimEmail {dto.TokenConfrimEmail} no es un Guid válido."
                );
            }
        }

        #region :: Confirm Email Steps ::

        private async Task<Users> FindUser(Guid userId, Guid tokenConfirmEmail)
        {
            
            StringBuilder sql = new StringBuilder();

            sql.Append("Select * from users ");
            sql.Append("Where user_id = @user_id ");
            sql.Append("And token_confirm_email = @token_confirm_email ");
            
            IEnumerable<Users> users = await Connection
                .QueryAsync<Users>(
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
            
            return users.FirstOrDefault();
        }

        private async Task<ConfirmEmailDto> UpdateUser(Users user, ConfirmEmailDto dto)
        {
            bool confirmEmail = false;
            
            StringBuilder updateUser = new StringBuilder();
            updateUser.Append("Update   users ");
            updateUser.Append("Set      confirm_email = true ");
            updateUser.Append("where    user_id=@user_id ");
            updateUser.Append("returning  confirm_email;");

            confirmEmail= await Connection
                .ExecuteScalarAsync<bool>(
                    updateUser.ToString(),
                    new
                    {
                        user_id = user.UserId
                    });

            return dto with
            {
                ConfirmEmail = confirmEmail,
                UserName = user.UserName
            };
        }

        #endregion

        #region :: Prevalidations ::

        private static bool IsUserIdValid(string userIdOnFilter)
        {
            bool result;
            try
            {
                Guid.TryParse(userIdOnFilter, out Guid userId);
                result = true;
            }
            catch 
            {
                result = false;
            }
            return result;
        }
        
        private static bool IsTokenConfirmEmailValid(string tokenConfirmEmail)
        {
            bool result;
            try
            {
                Guid.TryParse(tokenConfirmEmail, out Guid userId);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion
    }
}