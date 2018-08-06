using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Orleans2StatelessWorkers
{
    [StatelessWorker]
    public class HashGeneratorGrain : Grain, IHashGeneratorGrain
    {
        private HashAlgorithm hashAlgorithm;

        public HashGeneratorGrain()
        {
            this.hashAlgorithm = MD5.Create();
        }

        public Task<string> GenerateHashAsync(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = hashAlgorithm.ComputeHash(inputBytes);
            var hashBase64Str = Convert.ToBase64String(hashBytes);

            return Task.FromResult(hashBase64Str);
        }
    }
}
