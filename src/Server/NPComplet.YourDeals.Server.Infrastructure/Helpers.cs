using System;
using System.Threading;

namespace NPComplet.YourDeals.Server.Infrastructure
{
    /// <summary>
    /// </summary>
    public static class UniqueNumberGenerator
    {
        private static long _lastTimeStamp = DateTime.UtcNow.Ticks;

        /// <summary>
        /// </summary>
        public static string GetUnique
        {
            get
            {
                long original, newValue;
                do
                {
                    original = _lastTimeStamp;
                    var now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                    (ref _lastTimeStamp, newValue, original) != original);

                return newValue.ToString();
            }
        }
    }
}