

namespace DoesLuckyExist
{
    internal class Program
    {
        static void Main(string[] args)
        {

            {
                int min = 0;
                int max = 100;
                int count = 0;
                int CountGame = 1;
                char answer = 'T';
                int RndNum = GeneratorNumbers();
                do
                {
                    int Attempt = 0;
                    CountGame = +1;
                    while (true)
                    {
                        Run(Attempt);
                        bool again = CompareNumbers(RndNum, Attempt, count);
                        if (again == true)
                        {
                            answer = PlayAgain(answer);
                        }
                    }
                } while (answer == 'Y');

                Console.WriteLine($"min = {min}, difference between count and countGame {count / CountGame}");
            }

        }
        public static void Run(int Attempt)///Начало игры, ожидание ввода числа от пользователя, в случае провала закрытие игры
        {
            Console.WriteLine("Input number from [1;100]");
            for (int i = 0; i < 3; i++)
            {
                if (!int.TryParse(Console.ReadLine(), out Attempt) || (Attempt < 0 || Attempt > 100))
                {
                    Console.WriteLine("Input number from [1;100]");
                }
                else break;
                if (i == 2)
                {
                    Console.WriteLine("You are stupid");
                    return;
                }

            }
        }
        public static bool CompareNumbers(int RndNum, int Attempt, int count)///Основное тело игры, поиск загаданного числа
        {
            count = +1;
            if (RndNum < Attempt)
            {
                Console.WriteLine("Your number is greater");
                return false;
            }
            else if (RndNum > Attempt)
            {
                Console.WriteLine("Your number is less");
                return false;
            }
            else
            {
                Console.WriteLine("You won...just now");
                return true;
            }
        }
        public static int GeneratorNumbers()///Генератор чисел задающий число от 0 до 99
        {
            Random rnd = new Random();
            int Rnd = rnd.Next(0, 100);

            return Rnd;
        }
        public static char PlayAgain(char answer)/// Начинает игру снова
        {
            Console.WriteLine("Do you want to play again?");
            answer = Convert.ToChar(Console.Read());
            return answer;
        }

    }


}
