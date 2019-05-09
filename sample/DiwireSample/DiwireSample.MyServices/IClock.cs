using System;

namespace DiwireSample.MyServices
{
    internal interface IClock
    {
        DateTime UtcNow { get; }
    }
}
