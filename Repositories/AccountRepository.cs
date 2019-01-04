using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhatsUp.Models;
using WhatsUp.Repositories;

namespace WhatsUp.Repositories 
{
    public class AccountRepository : IAccountRepository
    {
        private WhatsUpContextDB db = new WhatsUpContextDB();

        public Account GetAccount(string username, string password)
        {
            return db.Accounts.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
        }

        public Account GetAccountById(int? id)
        {
            return db.Accounts.Find(id);
        }

        public Account GetAccount(int mobileNumber)
        {
            return db.Accounts.Single(a => a.MobileNumber == mobileNumber); 
        }

        public List<Account> GetAccounts(IEnumerable<Contact> contacten)
        {
            List<Account> accounts = new List<Account>();
            foreach (var contact in contacten)
            {
                Account account = db.Accounts.Find(contact.ContactAccountId);
                accounts.Add(account);
            }
            return accounts; 
        }
        public void RegisterAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();

            Account newAccount = db.Accounts.Single(a => a.MobileNumber == account.MobileNumber);

            List<Contact> contacts = db.Contacts.ToList();

            for (int i = 0; i < contacts.Count(); i++)
            {
                if (contacts[i].MobileNumber == newAccount.MobileNumber)
                {
                    contacts[i].ContactAccountId = newAccount.Id;
                    AddContactAccount(contacts[i]);
                }
            }
        }

        public bool ContactHasAccount(int mobileNumber)
        {
            return db.Accounts.Any(a => a.MobileNumber == mobileNumber);
        }

        private void AddContactAccount(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified; 
            db.SaveChanges();
        }
    }
}