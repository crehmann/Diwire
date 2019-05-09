using System;
using System.Collections.Generic;
using System.Text;

namespace DiwireSample.MyServices
{
    internal class FooService : IFooService
    {
        private readonly IClock _clock;

        public FooService(IClock clock)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public string HelloWorldString => $"Hello World ({_clock.UtcNow})";
    }
}
