using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Core.Transactions
{
    public sealed record Transaction(decimal Amount, DateTime Date, string Note)
}
