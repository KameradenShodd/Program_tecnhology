

using BankAccount.Core.Transactions;

namespace BankAccount.Core.Accounts
//ISP: Clients see only what they truly neefd
{
    public interface IAccountStatement
    {
        IReadOnlyList<Transaction> Transactions { get; }
    }
}
