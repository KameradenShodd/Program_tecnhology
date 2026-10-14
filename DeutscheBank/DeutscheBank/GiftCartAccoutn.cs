using System;
using System.Collections.Generic;
using System.Text;

namespace DeutscheBank
{
    public class GiftCartAccoutn : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;
        /// <summary>
        /// Создание подарочного счёта с ежемесячным пополнением.
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="initialBalance">начальный баланс</param>
        /// <param name="monthlyDeposit">ежемесячный депозит</param>
        public GiftCartAccoutn(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
        {
            _monthlyDeposit = monthlyDeposit;
        }
        /// <summary>
        /// Выполняет ежемесячные операции и пополнение счёта.
        /// </summary>
        public override void PerformMonthAndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposite(_monthlyDeposit, DateTime.UtcNow, "Apply month deposit");
            }
        }
        /// <summary>
        /// Возвращает строковое представление объекта GiftCartAccoutn, включая информацию о ежемесячном депозите.
        /// </summary>
        /// <returns>базовое строковое представление + информация о ежемесячном депозите</returns>
        public override string ToString()
        {
            return base.ToString() + $" {_monthlyDeposit}";
        }
    }
}