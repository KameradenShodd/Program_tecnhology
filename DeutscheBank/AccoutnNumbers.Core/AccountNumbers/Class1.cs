using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Core.AccountNumbers
{
    public sealed class SequentialAccountNumberGenerator: IAccountNumberGenerator
    {
        private int _seed;
        public SequentialAccountNumberGenerator(int seed = 1234567890) => _seed = seed;
        public string Next()
        {
            return (_seed++).ToString();
        }
    }
}
