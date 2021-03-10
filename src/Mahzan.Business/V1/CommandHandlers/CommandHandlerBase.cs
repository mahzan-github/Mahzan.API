using System;
using System.Threading.Tasks;
using Mahzan.Persistance.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Mahzan.Business.V1.CommandHandlers
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class CommandHandlerBase<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
    {
        /// <summary>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="logger"></param>
        protected CommandHandlerBase(NpgsqlConnection connection, ILogger<CommandHandlerBase<TCommand, TResponse>> logger)
        {
            Logger = logger;
            Connection = connection;
        }

        /// <summary>
        /// </summary>
        protected ILogger<CommandHandlerBase<TCommand, TResponse>> Logger { get; }

        /// <summary>
        /// </summary>
        protected NpgsqlConnection Connection { get; }

        /// <summary>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TCommand command)
        {
            await HandlePrevalidations(command);

            // Si ya existe una conexión abierta, la lógica de negocio se
            // ejecutará en el contexto de dicha conexión.
            // (Incluyendo una transacción previamente abierta).
            // Es válido utilizar Savepoints pero no se deben abrir transacciones.
            if (Connection.IsOpened())
            {
                return await HandleTransaction(command);
            }

            await Connection.OpenAsync();
            TResponse returnValue;
            try
            {
                await using var transaction = await Connection.BeginTransactionAsync();
                returnValue = await HandleTransaction(command);
                await transaction.CommitAsync();
            }
            finally
            {
                try
                {
                    await Connection.CloseAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error while closing database connection", ex);
                }
            }

            return returnValue;
        }

        /// <summary>
        ///     Validaciones que no requieren conexión a base de datos.
        ///     Debe lanzar excepciones para indicar un error en dichas validaciones.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual Task HandlePrevalidations(TCommand command)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Validaciones y lógica de negocio que requiere ejecutarse en la transacción de base de datos.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected abstract Task<TResponse> HandleTransaction(TCommand command);
    }
}