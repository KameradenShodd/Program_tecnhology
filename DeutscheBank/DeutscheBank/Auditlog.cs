using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public enum AuditOperationType
    {
        Deposit,
        Withdrawal,
        Interest
    }

    public record AuditEntry(
        string AccountNumber,
        AuditOperationType OperationType,
        decimal Amount,
        DateTime Date);

    public static class AuditLog
    {
        private static readonly List<AuditEntry> _entries = new List<AuditEntry>();
        private static readonly object _lock = new object();

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

        public static void Add(string accountNumber, AuditOperationType operationType, decimal amount, DateTime date)
        {
            lock (_lock)
            {
                _entries.Add(new AuditEntry(accountNumber, operationType, amount, date));
            }
        }
    }
}