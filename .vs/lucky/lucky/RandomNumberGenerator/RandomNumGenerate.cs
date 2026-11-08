using System;
using System.Collections.Generic;
using System.Text;

namespace lucky.RandomNumberGenerator
{
    public class RandomNumGenerate : IGenerate
    {
        private readonly Random _random = new Random();

        public int Generate(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
