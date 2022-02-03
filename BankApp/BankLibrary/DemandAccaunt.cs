using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DemandAccaunt : Account
    {
        public DemandAccaunt(decimal sum, int percentage) : base(sum, percentage)
        {

        }

        protected internal override void Open() => base.onOpened(new AccountEventAtgs(
                                $"Открыт новый счет до востребования, Id счета {this.Id}", this.Sum));
    }
}
