using System;
using System.Collections.Generic;
using System.Text;

namespace lucky.EvaluationOfNumber
{
    public enum Result { TooLow, TooHigh, PERFECT }
    public interface IIsItLowerOrHigher { Result Evaluate(int searchedNumber, int guess); }
}
