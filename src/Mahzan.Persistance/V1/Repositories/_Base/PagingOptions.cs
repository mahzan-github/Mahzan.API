using System;

namespace Mahzan.Persistance.V1.Repositories._Base
{
    /// <summary>
    /// </summary>
    public record PagingOptions
    {
        /// <summary>
        /// </summary>
        public string PageToken { get; init; } = String.Empty;

        /// <summary>
        ///     Number of records to return. Use 0 to return all records.
        /// </summary>
        public int PageSize { get; init; }

        /// <summary>
        /// </summary>
        public string OrderBy { get; init; } = String.Empty;
    }
}