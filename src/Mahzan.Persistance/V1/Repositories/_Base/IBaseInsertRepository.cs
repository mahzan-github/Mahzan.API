using System.Threading.Tasks;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    public interface IBaseInsertRepository<TEntity>
    {
        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);  
    }
}