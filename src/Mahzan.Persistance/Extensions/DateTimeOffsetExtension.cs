using System;

namespace Mahzan.Persistance.Extensions
{
    /// <summary>
    /// </summary>
    public static class DateTimeOffsetExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static DateTimeOffset TruncateToMillisecondPrecision(this DateTimeOffset dateTimeOffset)
        {
            var hangingTicks = dateTimeOffset.Ticks % TimeSpan.TicksPerMillisecond;
            return dateTimeOffset.AddTicks(-hangingTicks);
        }
    }
}