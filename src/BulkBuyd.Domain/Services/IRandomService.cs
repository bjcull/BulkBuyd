using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkBuyd.Domain.Services
{
    public interface IRandomService
    {
        double NextDouble();
        int Next();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);

        /// <summary>
        /// Whilst this method is thread-safe, the returned instance is not. Do not share the <see cref="System.Random"/> with another thread.
        /// Ideally you should be using the other methods, however this is neccessary if you need to pass a <see cref="System.Random"/> to another class.
        /// </summary>
        /// <returns>A non thread-safe instance of <see cref="System.Random"/>.</returns>
        Random GetUnderlyingRandom();
    }
}
