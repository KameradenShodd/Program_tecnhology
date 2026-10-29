
namespace BankAccount.Core.Withdrawals
//default policy: if balance < minimum then sREFUSE
{
    internal interface StrictWithdrawalPolicy : IWithdrawalPolicy
    {
        public WithdrawalDecision Evaluate(decimal currentBalance, decimal amount, decimal minimumBalance) => currentBalance - amount < minimumBalance ? WithdrawalDecision.Reject("Not enough rupies my friend") : WithdrawalDecision.Allow();
    }
}
