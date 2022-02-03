using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open() => base.onOpened(new AccountEventAtgs($"Открыт депозитный счет, Id счета {this.Id}", this.Sum));

        public override void Put(decimal sum)
        {
            if(_days%30 == 0)
                base.Put(sum);
            else
            {
                base.onAdded(new AccountEventAtgs($"на счет можно положить только после 30-ти дневного периода", 0));
            }
        }
        public override decimal WithDraw(decimal sum)
        {
            if(_days%30 == 0)
            {
                return base.WithDraw(sum);
            }
            else
            {
                base.onWithDrawed(new AccountEventAtgs($"Вывести средства можно только после 30-ти денвного периода", 0));
            }
            return 0;
        }

        protected internal override void Calculate()
        {
            if(_days%30 == 0)
            base.Calculate();
        }
    }
}
