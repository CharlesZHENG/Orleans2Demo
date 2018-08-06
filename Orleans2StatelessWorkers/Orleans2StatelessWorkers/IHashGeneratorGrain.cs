﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orleans2StatelessWorkers
{
    public interface IHashGeneratorGrain : IGrainWithIntegerKey
    {
        Task<string> GenerateHashAsync(string input);
    }
}
