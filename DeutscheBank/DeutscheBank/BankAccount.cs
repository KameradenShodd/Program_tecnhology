namespace DeutscheBank
{
    internal class BankAccount
    {
        private List<Transaction> _allTransactions = new List<Transaction>();
        public string Owner { get; private set; }
        public string Number { get; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (Transaction transaction in _allTransactions)
                {
                    balance += transaction.amount;
                }
                return balance;
            }
        }
        private static int account_num_seed = 1000_000_000;
        public BankAccount(string name, decimal crediten)
        {
            MakeDeposite(crediten, DateTime.UtcNow, "rip bozo");
            Owner = name;
            Number = account_num_seed.ToString();
            account_num_seed++;
        }
        public void MakeDeposite(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "are you stupid");
            }
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "are you stupid");
            }
            if (Balance < amount)
            {
                throw new InvalidOperationException("are you broke");
            }
            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);
        }
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in _allTransactions)
            {
                balance += item.amount;
                report.AppendLine($"{item.date.ToShortDateString()}\t{item.amount}\t{balance}\t{item.note}");
            }
            return report.ToString();
        }
    }
}
