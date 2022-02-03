using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {

        protected internal event AccountStateHandler WithDrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        static int counter = 0; //safe in memory, undependency by created class
        protected int _days = 0; //days after create accoint

        public int Id { get; set; }
        public decimal Sum  { get; set; }
        public int Percentage { get; set; }

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }
        private void CallEvent(AccountEventAtgs e, AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }
        }

        //call each event as virtual method
        protected virtual void onOpened(AccountEventAtgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void onClosed(AccountEventAtgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void onWithDrawed(AccountEventAtgs e)
        {
            CallEvent(e, WithDrawed);
        }
        protected virtual void onCalculated(AccountEventAtgs e)
        {
            CallEvent(e, Calculated);
        }
        protected virtual void onAdded(AccountEventAtgs e)
        {
            CallEvent(e, Added);
        }


        public virtual decimal WithDraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                onWithDrawed(new AccountEventAtgs($"Со счета снято {sum}", sum));
            }
            else
            {
                onWithDrawed(new AccountEventAtgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }

        public virtual void Put(decimal sum)
        {
            Sum += sum;
            onAdded(new AccountEventAtgs($"На счет поступило {sum}", sum));
        }

        protected internal virtual void Open()
        {
            onOpened(new AccountEventAtgs($"Открыт новый счет  Id - {Id}", Sum));
        }
        protected internal virtual void Close()
        {
            onClosed(new AccountEventAtgs($"Счет {Id} закрыт, Итоговая сумма = {Sum}", Sum));
        }

        protected internal void IncDays()
        {
            _days++;
        }
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum += increment;
            onCalculated(new AccountEventAtgs($"Начислены проценты в размере {increment}", increment));
        }
    }
}
