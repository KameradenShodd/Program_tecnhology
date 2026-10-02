using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public class InterestEarningAccount:BankAccount
    {
        public InterestEarningAccount(string name, decimal initialBalance): base(name, initialBalance)
        {

        }
        public override void PerformMonthAndTransactions()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.02m;
                MakeDeposite(interest, DateTime.UtcNow, "Apply month interest");
            }
        }
    }
}
