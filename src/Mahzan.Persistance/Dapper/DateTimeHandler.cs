using System;
using System.Data;
using Dapper;

namespace Mahzan.Persistance.Dapper
{
    /// <summary>
    /// </summary>
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        /// <summary>
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            throw new NotImplementedException("Writing DateTime fields is not supported..");
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime Parse(object value)
        {
            throw new NotImplementedException("Reading DateTime fields is not supported. Use DateTimeOffset instead.");
        }
    }

    /// <summary>
    /// </summary>
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        /// <summary>
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            throw new NotImplementedException("Writing DateTimeOffset fields is not supported.");
        }

        /// <summary>
        ///     https://stackoverflow.com/questions/12510299/get-datetime-as-utc-with-dapper
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override DateTimeOffset Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }
    }
}