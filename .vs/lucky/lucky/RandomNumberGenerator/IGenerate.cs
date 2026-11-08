using System;
using System.Collections.Generic;
using System.Text;

namespace lucky.RandomNumberGenerator
{
    public interface IGenerate
    {
        int Generate(int min, int max);
    }
}
