using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    //type of account
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    public class Bank<T> where T : Account
    {
        T[] accounts;
        public string Name { get; private set; }
        public Bank(string name)
        {
            Name = name;
        }

        public void Open(AccountType accountType, decimal sum,
                         AccountStateHandler addSumHandler, AccountStateHandler withDrawSumHandler,
                         AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler,
                         AccountStateHandler openAccountHandler)
        {
            T newAccount = null;
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccaunt(sum, 1) as T; break;
                    case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 1) as T; break;
            }

            if(newAccount == null)
            {
                throw new Exception("Ошибка создания счета");
            } 
            if(accounts == null)
            {
                accounts = new T[] { newAccount}; // adding new Account in array of Accaunts
            } else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                {
                    tempAccounts[i] = accounts[i];
                }
                tempAccounts[accounts.Length -1] = newAccount;
                accounts = tempAccounts;
            }
            //added subscribe on the events
            newAccount.Added += addSumHandler;
            newAccount.WithDrawed += withDrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Put(sum);
        }

        public void WithDraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.WithDraw(sum);
        }

        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Close();

            if (accounts.Length <= 1)
                account = null;
            else
            {
                //decrese array of accounts
                T[] tempAccounts = new T[accounts.Length-1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i!= index)
                    {
                        tempAccounts[j++] = accounts[i];
                    }
                }
                accounts = tempAccounts;
            }
        }

        public void CalculatePercentage()
        {
            if (accounts == null) return; //if array doesnt created, exit
            foreach(var acc in accounts)
            {
                acc.IncDays();
                acc.Calculate();
            }
        }

        public T FindAccount(int id)
        {
            if(accounts == null) return null;
            foreach (var acc in accounts)
            {
                if (acc.Id == id) return acc;
            }
            return null;
        }
        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if(accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }
    }
}
