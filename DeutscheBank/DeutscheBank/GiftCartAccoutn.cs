using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public class GiftCartAccoutn : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;

        public GiftCartAccoutn(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
        {
        }
        public override void PerformMonthAndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposite(_monthlyDeposit, DateTime.UtcNow, "Apply month deposit");

            }
        }
        public override string ToString()
        {
            return base.ToString() + $" {_monthlyDeposit}";
        }

    }
}
