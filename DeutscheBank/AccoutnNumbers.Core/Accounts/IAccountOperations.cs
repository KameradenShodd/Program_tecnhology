using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Core.Accounts
{
    internal interface IAccountOperations
    {
        decimal Balance { get; }
        void Deposit(decimal amount, DateTime date, string note);
        void Withdraw(decimal amount, DateTime date, string note);



    }
}
