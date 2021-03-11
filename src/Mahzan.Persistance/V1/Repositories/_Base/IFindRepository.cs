using System.Collections.Generic;
using System.Threading.Tasks;
using Mahzan.Persistance.V1.ViewModel.Paged;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    /// <summary>
    ///     Repository interface for find-only versioned-data repositories
    /// </summary>
    /// <typeparam name="TEntity">Type for business entities</typeparam>
    /// <typeparam name="TFilter">Type for filter</typeparam>
    public interface IFindRepository<TEntity, in TFilter>
    {
        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<PagedViewModel<TEntity>> FindPaged(TFilter filter, PagingOptions paging);

        /// <summary>
        ///     TODO: OrderBy
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> FindAll(TFilter filter);

        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<TEntity> FindSingle(TFilter filter);
    }
}