using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLibrary;

namespace BankApp
{
    internal partial class Program
    {
        private static void OpenAccountHandler(object _, AccountEventAtgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void AddSumHandler(object _, AccountEventAtgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithDrawSumHandler(object _, AccountEventAtgs e)
        {
            Console.WriteLine(e.Message);
            if(e.Sum > 0)
            {
                Console.WriteLine("Идем тратить деньги");

            }
        }

        private static void CloseAccountHandler(object _, AccountEventAtgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
