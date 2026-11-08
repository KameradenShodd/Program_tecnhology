using lucky.Other;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using lucky.Our_or_In_put;
namespace lucky.Checker
{
    internal class Guesser
    {
        private readonly IInput _input;
        private readonly IOutput _output;
        private readonly Rules _rules;

        public Guesser(IInput input, IOutput output, Rules rules) => (_input, _output, _rules) = (input, output, rules);

        public int? ReadGuess()
        {
            for (int i = 0; i < _rules.max_invalids; i++)
            {
                _output.WriteLine($"Input number from [{_rules.min};{_rules.max}]");

                if (int.TryParse(_input.ReadLine(), out int guess) &&
                    guess >= _rules.min && guess <= _rules.max)
                    return guess;
            }
            return null;
        }
    }
}
