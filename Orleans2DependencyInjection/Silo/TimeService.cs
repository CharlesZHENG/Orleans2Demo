using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Silo
{
    public class TimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
