using System;

namespace Bitbucket.Net.Common
{
    public static class UnixDateTimeExtensions
    {
        public static DateTimeOffset FromUnixTimeSeconds(this long value)
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero)
                .AddMilliseconds(value)
                .ToLocalTime();
        }

        public static long ToUnixTimeSeconds(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.Subtract(new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero)).Ticks;
        }
    }
}
