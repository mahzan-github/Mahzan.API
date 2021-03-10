using Dapper;

namespace Mahzan.Persistance.Dapper
{
    /// <summary />
    public static class DapperSetup
    {
        /// <summary>
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// </summary>
        public static void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            Initialized = true;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(new DateTimeHandler());
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            // SqlMapper.AddTypeHandler(new FundsRetentionStatusEnumHandler());
            // SqlMapper.AddTypeHandler(new FundsRetentionTypeEnumHandler());
            // SqlMapper.AddTypeHandler(new SolicitorTypeHandler());
            // SqlMapper.AddTypeHandler(new FundsRetentionChangeReasonStatusHandler());
        }
    }
}