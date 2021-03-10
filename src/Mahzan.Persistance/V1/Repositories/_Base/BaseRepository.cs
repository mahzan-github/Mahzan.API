using System.Data;
using System.Threading.Tasks;
using Mahzan.Persistance.Dapper;
using MAhzan.Persistance.V1.Repositories._Base;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    {
        internal BaseRepository(NpgsqlConnection connection)
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
        public Task<TEntity> Insert(TEntity entity)
        {
            //TODO: Implement transaction handling code or connection check?
            return InsertInternal(entity);
        }

        /// <summary>
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected abstract Task<TEntity> InsertInternal(TEntity dto);
    }
}