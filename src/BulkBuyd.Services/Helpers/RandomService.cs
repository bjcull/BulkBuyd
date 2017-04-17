using System;
using System.Threading;
using BulkBuyd.Domain.Services;

namespace BulkBuyd.Services.Helpers
{
    public class RandomService : IRandomService
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public double NextDouble()
        {
            return random.Value.NextDouble();
        }

        public int Next()
        {
            return random.Value.Next();
        }

        public int Next(int maxValue)
        {
            return random.Value.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Value.Next(minValue, maxValue);
        }

        /// <summary>
        /// Whilst this method is thread-safe, the returned instance is not. Do not share the <see cref="System.Random"/> with another thread.
        /// Ideally you should be using the other methods, however this is neccessary if you need to pass a <see cref="System.Random"/> to another class.
        /// </summary>
        /// <returns>A non thread-safe instance of <see cref="System.Random"/>.</returns>
        public Random GetUnderlyingRandom()
        {
            return random.Value;
        }
    }
}
