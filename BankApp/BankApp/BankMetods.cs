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
        private static void WithDraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для снятия со счета: ");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.WithDraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Введите сумму, чтобы положить на счет:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета: ");

            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите Id счета, котрый надо закрыть");

            int id = Convert.ToInt32((Console.ReadLine()));
            bank.Close(id);
        }
    }
}
