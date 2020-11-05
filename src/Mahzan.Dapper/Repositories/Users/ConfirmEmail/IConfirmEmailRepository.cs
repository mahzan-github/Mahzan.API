using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Dapper.Repositories.Users.ConfirmEmail
{
    public interface IConfirmEmailRepository
    {
        Task HandleRepository(Guid userId, Guid tokenConfirmEmail);
    }
}
