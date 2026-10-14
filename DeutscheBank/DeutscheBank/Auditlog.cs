using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{

    /// <summary>
    /// Тип аудита
    /// </summary>
    public enum AuditOperationType
    {
        /// <summary>
        /// Пополнение
        /// </summary>
        Deposit,
        /// <summary>
        ///     Списание
        /// </summary>
        Withdrawal,
        /// <summary>
        ///     Начисление процентов
        /// </summary>
        Interest
    }
    /// <summary>
    /// Аудит
    /// </summary>
    /// <param name="AccountNumber"></param>
    /// <param name="OperationType"></param>
    /// <param name="Amount"></param>
    /// <param name="Date"></param>
    public record AuditEntry(
        string AccountNumber,
        AuditOperationType OperationType,
        decimal Amount,
        DateTime Date);
    /// <summary>
    /// Аудит лог
    /// </summary>
    public static class AuditLog
    {
        private static readonly List<AuditEntry> _entries = new List<AuditEntry>();
        private static readonly object _lock = new object();
        /// <summary>
        /// Получение записей в виде спииска
        /// </summary>

        public static IReadOnlyList<AuditEntry> Entries
        {
            get
            {
                lock (_lock)
                {
                    return _entries.ToArray();
                }
            }
        }
        /// <summary>
        /// + Запись в аудитлог
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="operationType"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        public static void Add(string accountNumber, AuditOperationType operationType, decimal amount, DateTime date)
        {
            lock (_lock)
            {
                _entries.Add(new AuditEntry(accountNumber, operationType, amount, date));
            }
        }
    }
}