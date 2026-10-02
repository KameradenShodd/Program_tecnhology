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
            try { account2.MakeWithdrawal(1000, DateTime.UtcNow, "oh daaaaaaaaamn"); } catch (InvalidOperationException e) { Console.WriteLine(e.Message); }
            InterestEarningAccount interest = new InterestEarningAccount("John Poor", 1000);
            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("John Poor", 0, 1000m);
            lineOfCredit.MakeWithdrawal(10m, DateTime.UtcNow, "Take out loan");

            GiftCartAccoutn giftcart = new GiftCartAccoutn("John Poor", 500m, 1000m);
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interest);
            accounts.Add(lineOfCredit);
            accounts.Add(giftcart);
            foreach (var account in accounts)
            {
                Console.WriteLine($"Account: {account}");
                account.PerformMonthAndTransactions();
                Console.WriteLine(account.GetAccountHistory());
            }
            //lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "Take out loan");

        }
    }
}