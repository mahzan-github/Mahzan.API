using System.Data;
using Npgsql;

namespace Mahzan.Persistance.Extensions
{
    /// <summary>
    /// </summary>
    public static class NpgsqlConnectionExtension
    {
        /// <summary>
        ///     TODO: Evaluate if needed
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static bool IsOpened(this NpgsqlConnection connection)
        {
            return connection.State.HasFlag(ConnectionState.Connecting) ||
                   connection.State.HasFlag(ConnectionState.Open);
        }
    }
}