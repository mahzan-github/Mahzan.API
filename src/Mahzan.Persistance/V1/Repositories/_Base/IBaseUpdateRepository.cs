using System.Threading.Tasks;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    public interface IBaseUpdateRepository<TEntity>
    {
        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity);    
    }
}