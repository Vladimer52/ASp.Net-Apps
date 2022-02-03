using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventAtgs e);
    public class AccountEventAtgs
    {
        public string Message { get; private set; }

        public decimal Sum { get; private set; }

        public AccountEventAtgs(string mes, decimal sum)
        {
            Message = mes;
            Sum = sum;
        }
    }
}
