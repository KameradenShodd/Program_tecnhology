using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public class LineOfCreditAccount : BankAccount
    {
        /// <summary>
        /// Линия кредита с лимитом кредитования. Баланс может быть отрицательным до указанного лимита.
        /// </summary>
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit, AccountStatus status)
           : base(name, initialBalance, -creditLimit, status)
        {
        }

        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
            : this(name, initialBalance, creditLimit, AccountStatus.Regular)
        {
        }

        /// <summary>
        /// Выполняет ежемесячные операции по начислению процентов на отрицательный баланс.
        /// </summary>
        public override void PerformMonthAndTransactions()
        {
            if (Balance < 0)
            {
                decimal interest = -Balance * 0.02m;
                MakeWithdrawal(interest, DateTime.UtcNow, "Apply month interest");
            }
        }
        /// <summary>
        /// Проверяет, превышен ли лимит кредитования при снятии средств. Если баланс отрицательный, применяется комиссия за превышение лимита.
        /// </summary>
        /// <param name="isOverdrawn">Указывает, превышен ли лимит.</param>
        /// <returns>Транзакция о превышении лимита или null.</returns>
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "apply overdraft") : default;

    }
}