using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Mahzan.Persistance.Dapper;
using Mahzan.Persistance.V1.ViewModel.Paged;
using Npgsql;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    public abstract class BaseFindRepository<TEntity, TFilter> : IBaseFindRepository<TEntity, TFilter>
    {
        private readonly Func<TEntity, string> _toPageToken;

        internal BaseFindRepository(NpgsqlConnection connection, Func<TEntity, string> toPageToken)
        {
            if (!DapperSetup.Initialized)
            {
                throw new Exception("Dapper is not correctly initialized");
            }

            _toPageToken = toPageToken;

            Connection = connection;
        }

        /// <summary>
        /// </summary>
        protected NpgsqlConnection Connection { get; }


        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<TEntity>> FindAll(TFilter filter)
        {
            return await FindInternal(filter, new PagingOptions());
        }

        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<PagedViewModel<TEntity>> FindPaged(TFilter filter, PagingOptions paging)
        {
            if (paging.PageSize == 0)
            {
                throw new ArgumentException("PageSize must have a value higher that zero", nameof(paging));
            }

            var internalData = await FindInternal(filter, paging with { PageSize = paging.PageSize + 1 });

            if (internalData.Count > paging.PageSize + 1)
            {
                throw new DataException("More records returned than expected");
            }

            if (paging.PageSize >= internalData.Count)
            {
                return new PagedViewModel<TEntity>
                {
                    Items = internalData.ToImmutableList(),
                    NextPageToken = String.Empty
                };
            }

            return new PagedViewModel<TEntity>
            {
                Items = internalData.Take(paging.PageSize).ToImmutableList(),
                NextPageToken = _toPageToken(internalData[^1])
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> FindSingle(TFilter filter)
        {
            //TODO: Optimize so we don't need to enumerate to List
            var result = await FindInternal(filter, new PagingOptions { PageSize = 2 });

            return result.Count switch
            {
                0 => throw new Exception("FindSingle: Not rows were returned."),
                > 1 => throw new Exception("FindSingle: More than one row was returned."),
                _ => result[0]
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        protected abstract Task<IReadOnlyList<TEntity>> FindInternal(TFilter filter, PagingOptions pagingOptions);
    }
}