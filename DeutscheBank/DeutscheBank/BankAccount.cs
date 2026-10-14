using System;
using System.Collections.Generic;

namespace DeutscheBank
{
    /// <summary>
    /// Базовый класс для банковского счёта. Содержит общие свойства и методы для всех типов счетов.
    /// </summary>
    public class BankAccount
    {
        private readonly decimal _mininumBalance;
        private List<Transaction> _allTransactions = new List<Transaction>();
        /// <summary>
        ///    Владелец счёта
        /// </summary>
        public string Owner { get; private set; }
        /// <summary>
        ///     Номер счёта
        /// </summary>
        public string Number { get; }
        /// <summary>
        ///     Баланс счёта
        /// </summary>
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (Transaction transaction in _allTransactions)
                {
                    balance += transaction.amount;
                }
                return balance;
            }
        }
        private static int account_num_seed = 1000_000_000;
        /// <summary>
        ///     Статус аккаунта
        /// </summary>
        public enum AccountStatus
        {
            /// <summary>
            ///     Обычный аккаунт
            /// </summary>
            Regular,
            /// <summary>
            ///     Премиум аккаунт
            /// </summary>
            Premium, 
            /// <summary>
            ///     VIP аккаунт
            /// </summary>  
            VIP
        }
        /// <summary>
        ///     Статус аккаунта
        /// </summary>
        public AccountStatus Status { get; private set; }
        /// <summary>
        ///       Создание аккаунта с начальным балансом и статусом Regular
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        public BankAccount(string name, decimal initialBalance)
            : this(name, initialBalance, 0, AccountStatus.Regular)
        {
        }

        /// <summary>
        ///      Создание аккаунта с начальным балансом и указанным статусом
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="status"></param>
        public BankAccount(string name, decimal initialBalance, AccountStatus status)
            : this(name, initialBalance, 0, status)
        {
        }
        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="initialBalance">Начальный баланс.</param>
        /// <param name="minimumBalance">Минимальный баланс.</param>
        /// <param name="status">Статус аккаунта.</param>
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance, AccountStatus status)
        {
            Owner = name;
            Number = account_num_seed.ToString();
            account_num_seed++;
            _mininumBalance = minimumBalance;
            Status = status;
            if (initialBalance > 0) MakeDeposite(initialBalance, DateTime.UtcNow, "Initial balance");

        }
        /// <summary>
        /// Пополняет счёт на указанную сумму.
        /// </summary>
        /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="note">Комментарий к операции.</param>
        /// <param name="operationType">Тип операции для журнала аудита.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
        /// </exception>
        public void MakeDeposite(decimal amount, DateTime date, string note, AuditOperationType operationType = AuditOperationType.Deposit)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "are you stupid");
            }
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
            AuditLog.Add(Number, operationType, amount, date);
        }
        /// <summary>
        /// Снимает со счёта указанную сумму.
        /// </summary>
        /// <param name="amount">сумма</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="note">Комментарий к операции.</param>
        /// <param name="operationType">Тип операции для журнала аудита.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
        /// </exception>
        public void MakeWithdrawal(decimal amount, DateTime date, string note, AuditOperationType operationType = AuditOperationType.Withdrawal)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(amount);
            Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _mininumBalance);
            Transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);

            var actualOperationType = operationType;
            if (actualOperationType == AuditOperationType.Withdrawal &&
                note.Contains("interest", StringComparison.OrdinalIgnoreCase))
            {
                actualOperationType = AuditOperationType.Interest;
            }

            AuditLog.Add(Number, actualOperationType, Math.Abs(amount), date);

            if (overdraftTransaction != null)
            {
                _allTransactions.Add(overdraftTransaction);
                AuditLog.Add(Number, AuditOperationType.Withdrawal, Math.Abs(overdraftTransaction.amount), overdraftTransaction.date);
            }

            if (Status == AccountStatus.VIP && amount > 0)
            {
                MakeDeposite(amount * 0.05m, date, "cashback cashback cashback 5%");
            }
        }
        /// <summary>
        /// Проверяет, превышает ли снятие минимальный баланс. Если превышает, то возвращает транзакцию о превышении лимита снятия.
        /// </summary>
        /// <param name="isOverdrawn"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Withdrawal would exceed minimum balance.");
            }
            else
            {
                return default;
            }
        }
        /// <summary>
        /// Возвращает историю операций по счёту в виде строки.
        /// </summary>
        /// <returns></returns>
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in _allTransactions)
            {
                balance += item.amount;
                report.AppendLine($"{item.date.ToShortDateString()}\t{item.amount}\t{balance}\t{item.note}");
            }
            return report.ToString();
        }
        /// <summary>
        /// Если показывается это, то я сделал что-то не так
        /// </summary>
        public virtual void PerformMonthAndTransactions()
        {

        }

        /// <summary>
        /// Возвращает владельца, номер и тип счёта
        /// </summary>
        ///<returns>Имя владельца, номер и тип счёта</returns>
        public override string ToString()
        {
            return $"Owner: {Owner}\taccount number: {Number}, (тип счёта: {GetType()})";
        }
    }
}