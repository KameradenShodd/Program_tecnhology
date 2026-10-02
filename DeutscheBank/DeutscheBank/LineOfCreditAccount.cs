using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {

        }


        public override void PerformMonthAndTransactions()
        {
            if (Balance < 0)
            {
                decimal interest = -Balance * 0.02m;
                MakeWithdrawal(interest, DateTime.UtcNow, "Apply month interest");
            }
        }
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "apply overdraft") : default;
            
        }
    }
