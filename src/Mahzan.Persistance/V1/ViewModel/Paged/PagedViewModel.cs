using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mahzan.Persistance.V1.ViewModel.Paged
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record PagedViewModel<T>
    {
        /// <summary>
        /// </summary>
        public IReadOnlyList<T> Items { get; init; } = ImmutableList<T>.Empty;

        /// <summary>
        /// </summary>
        public string NextPageToken { get; init; } = string.Empty;
    }
}