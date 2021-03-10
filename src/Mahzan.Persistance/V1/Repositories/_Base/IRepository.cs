using System.Threading.Tasks;

namespace MAhzan.Persistance.V1.Repositories._Base
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);  
    }
}