using System;

namespace DeutscheBank
{
    /// <summary>
    /// Сервис переводов между счетами.
    /// Не хранит состояние, только координирует операции двух счетов.
    /// </summary>
    public static class MoneyTransferService
    {
        /// <summary>
        /// Переводит деньги со счёта <paramref name="from"/> на счёт <paramref name="to"/>.
        /// </summary>
        /// <param name="from">Счёт-источник.</param>
        /// <param name="to">Счёт-получатель.</param>
        /// <param name="amount">Сумма перевода. Должна быть больше нуля.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="note">Комментарий к переводу.</param>
        public static void Transfer(
            BankAccount from,
            BankAccount to,
            decimal amount,
            DateTime date,
            string note)
        {
            ArgumentNullException.ThrowIfNull(from);
            ArgumentNullException.ThrowIfNull(to);

            if (ReferenceEquals(from, to))
            {
                throw new InvalidOperationException("Нельзя перевести деньги на тот же самый счёт.");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма перевода должна быть положительной.");
            }

            // Общий идентификатор, чтобы связать парные операции в истории/аудите.
            string transferId = Guid.NewGuid().ToString("N");

            string withdrawalNote = $"Transfer {transferId} to {to.Number}: {note}";
            string depositNote = $"Transfer {transferId} from {from.Number}: {note}";

            // Сначала списываем.
            from.MakeWithdrawal(amount, date, withdrawalNote);

            try
            {
                // Затем зачисляем.
                to.MakeDeposite(amount, date, depositNote);
            }
            catch
            {
                // Компенсация: если зачисление всё-таки упало, возвращаем деньги на источник.
                from.MakeDeposite(
                    amount,
                    DateTime.UtcNow,
                    $"Rollback transfer {transferId}",
                    AuditOperationType.Deposit);

                throw;
            }
        }
    }
}