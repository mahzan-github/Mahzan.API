using System.Data;
using System.Threading.Tasks;
using Mahzan.Persistance.Dapper;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    public abstract class BaseUpdateRepository<TEntity> : IBaseUpdateRepository<TEntity>
    {
        internal BaseUpdateRepository(NpgsqlConnection connection)
        {
            if (!DapperSetup.Initialized)
            {
                throw new DataException("Dapper is not correctly initialized");
            }

            Connection = connection;
        }

        /// <summary>
        /// </summary>
        protected NpgsqlConnection Connection { get; }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<TEntity> Update(TEntity entity)
        {
            //TODO: Implement transaction handling code or connection check?
            HandlePrevalidations(entity);
            
            return UpdateInternal(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract Task<TEntity> UpdateInternal(TEntity dto);
        
        /// <summary>
        ///     Validaciones que no requieren conexión a base de datos.
        ///     Debe lanzar excepciones para indicar un error en dichas validaciones.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual void HandlePrevalidations(TEntity dto)
        {

        }  
    }
}