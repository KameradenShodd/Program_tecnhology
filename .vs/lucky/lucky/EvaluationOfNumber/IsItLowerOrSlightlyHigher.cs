
using System;
using System.Collections.Generic;
using System.Text;
namespace lucky.EvaluationOfNumber
{
    public class IsItLowerOrSlightlyHigher : IIsItLowerOrHigher
    {
    public Result Evaluate(int searchedNumber, int guess) => guess < searchedNumber ? Result.TooLow : guess > searchedNumber ? Result.TooHigh : Result.PERFECT; }
}
