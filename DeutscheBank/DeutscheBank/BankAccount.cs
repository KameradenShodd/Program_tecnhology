namespace DeutscheBank
{
    public class BankAccount
    {
        private readonly decimal _mininumBalance;
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
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
        {

        }
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            Owner = name;
            Number = account_num_seed.ToString();
            account_num_seed++;
            _mininumBalance = minimumBalance;
            if (initialBalance > 0) MakeDeposite(initialBalance, DateTime.UtcNow, "Initial balance");

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
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _mininumBalance);
            Transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);
            if(overdraftTransaction != null)
            {
                _allTransactions.Add(overdraftTransaction);
            }
        }
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Withdrawal would exceed minimum balance.");
            }
            else
            {
                return default;
            }
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

        public virtual void PerformMonthAndTransactions()
        {
            
        }

        public override string ToString()
        {
            return $"Owner: {Owner}\taccount number: {Number}, (тип счёта: {GetType()})";
        }
    }
}
