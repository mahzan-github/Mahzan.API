using System.Threading.Tasks;

namespace Mahzan.Business.V1.CommandHandlers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommandHandler<in TCommand, TResponse>
    {
        /// <summary>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<TResponse> Handle(TCommand command);
    }
}