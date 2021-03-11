using System.Threading.Tasks;
using Mahzan.Business.V1.Commands.User;
using Mahzan.Persistance.V1.ViewModel.User;

namespace Mahzan.Business.V1.CommandHandlers.User.LogIn
{
    public interface ILogInCommandHandler : ICommandHandler<LogInCommad, LogInViewModel>
    {

    }
}