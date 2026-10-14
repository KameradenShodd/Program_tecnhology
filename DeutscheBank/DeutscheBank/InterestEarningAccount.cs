using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    /// <summary>
    /// Сберегательный счёт с начислением процентов на баланс свыше 500 единиц.
    /// </summary>
    public class InterestEarningAccount : BankAccount
    {
        /// <summary>
        /// Создание сберегательного счёта с начислением процентов на баланс свыше 500 единиц.
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="initialBalance">начальный баланс</param>
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {

        }
        /// <summary>
        /// Выполняет ежемесячные операции и начисление процентов на баланс свыше 500 единиц.
        /// </summary>
        public override void PerformMonthAndTransactions()
        {
            switch (Status)
            {
                case AccountStatus.Regular:
                    if (Balance >= 50m)
                    {
                        MakeWithdrawal(50m, DateTime.UtcNow, "Monthly service fee");
                    }
                    break;

                case AccountStatus.Premium:
                    if (Balance > 10_000m)
                    {
                        MakeDeposite(
                            (Balance - 10_000m) * 0.01m,
                            DateTime.UtcNow,
                            "Premium 1%",
                            AuditOperationType.Interest);
                    }
                    break;

                case AccountStatus.VIP:
                    if (Balance > 10_000m)
                    {
                        MakeDeposite(
                            (Balance - 10_000m) * 0.03m,
                            DateTime.UtcNow,
                            "VIP 3%",
                            AuditOperationType.Interest);
                    }
                    break;
            }

            if (Balance > 500m)
            {
                MakeDeposite(
                    Balance * 0.02m,
                    DateTime.UtcNow,
                    "Apply month interest",
                    AuditOperationType.Interest);
            }
        }
    }
}