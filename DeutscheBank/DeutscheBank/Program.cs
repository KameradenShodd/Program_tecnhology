namespace DeutscheBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("John Rich", 10000000);
            BankAccount account2 = new BankAccount("John Poor", 10);
            Console.WriteLine($"{account1.Owner}: {account1.Number} {account1.Balance}");
            Console.WriteLine($"{account2.Owner}: {account2.Number} {account2.Balance}");

            account1.MakeDeposite(12000, DateTime.UtcNow, "robbed some guy");
            Console.WriteLine($"{account1.Balance}");
            account1.MakeWithdrawal(12000, DateTime.UtcNow, "got robbed by other guy");
            Console.WriteLine($"{account1.Balance}");
            Console.WriteLine(account1.GetAccountHistory());

            try { account2.MakeWithdrawal(1000, DateTime.UtcNow, "oh fuuuuuuuuuuuuuck"); } catch (InvalidOperationException e) { Console.WriteLine(e.Message); }
        }
    }
}