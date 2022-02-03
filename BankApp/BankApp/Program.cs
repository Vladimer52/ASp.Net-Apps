using System;
using BankLibrary;

namespace BankApp
{
    //добавить ссылку на проект BankLibrary перед использованием(ПКМ на зависимостях, добавить проект)
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("UnitBank");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor=color;
                try
                {
                    int cmd = Convert.ToInt32(Console.ReadLine());

                    switch (cmd)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                            case 2:
                            WithDraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                                break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                } 
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберете тип счета: 1. до востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            switch (type)
            {
                case 1:
                    accountType = AccountType.Deposit;
                    break;
                case 2:
                    accountType = AccountType.Ordinary;
                    break;
                default: throw new Exception("Не удалось создать счет");
            }

            bank.Open(accountType, sum, AddSumHandler, WithDrawSumHandler, 
                (o, e) => Console.WriteLine(e.Message),
                CloseAccountHandler, OpenAccountHandler);

        }
    }
}
