using System;

namespace DiwireSample.MyServices
{
    internal class Clock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
