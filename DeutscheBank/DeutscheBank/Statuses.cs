using System;

namespace DeutscheBank
{
    /// <summary>
    /// Ежемесячные правила, зависящие от статуса клиента.
    /// </summary>
    public static class StatusRules
    {
        public static void ApplyMonthly(BankAccount account)
        {
            switch (account.Status)
            {
                case BankAccount.AccountStatus.Regular:
                    if (account.Balance >= 50m)
                        account.MakeWithdrawal(50m, DateTime.UtcNow, "Monthly service fee");
                    break;
                case BankAccount.AccountStatus.Premium:
                    if (account.Balance > 10_000m)
                        account.MakeDeposite((account.Balance - 10_000m) * 0.01m, DateTime.UtcNow, "Premium 1%");
                    break;
                case BankAccount.AccountStatus.VIP:
                    if (account.Balance > 10_000m)
                        account.MakeDeposite((account.Balance - 10_000m) * 0.03m, DateTime.UtcNow, "VIP 3%");
                    break;
            }
        }
    }
}