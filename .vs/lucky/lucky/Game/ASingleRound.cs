using System;
using System.Collections.Generic;
using System.Text;
using static lucky.Game.ASingleRound;
using lucky.Checker;
using lucky.EvaluationOfNumber;
using lucky.Our_or_In_put;

namespace lucky.Game

{
    internal class ASingleRound
    {
        public class GameRound
        {
            private readonly IOutput _output;
            private readonly IChecker _reader;
            private readonly IIsItLowerOrHigher _evaluator;

            public GameRound(IOutput output, IChecker reader, IIsItLowerOrHigher evaluator)
            {
                _output = output;
                _reader = reader;
                _evaluator = evaluator;
            }

            public int? Play(int secret)
            {
                int attempts = 0;
                while (true)
                {
                    int? guess = _reader.ReadGuess();
                    if (guess is null)
                    {
                        _output.WriteLine("Too many invalid inputs. Round aborted.");
                        return null;
                    }

                    attempts++;
                    switch (_evaluator.Evaluate(secret, guess.Value))
                    {
                        case Result.TooLow:
                            _output.WriteLine("Your number is less");
                            break;
                        case Result.TooHigh:
                            _output.WriteLine("Your number is greater");
                            break;
                        case Result.PERFECT:
                            _output.WriteLine("You won");
                            return attempts;
                    }
                }
            }
        }
    }
}
