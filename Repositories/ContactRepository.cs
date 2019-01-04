using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhatsUp.Models;
using WhatsUp.Repositories;

namespace WhatsUp.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private WhatsUpContextDB db = new WhatsUpContextDB();
        private Account account = (Account)HttpContext.Current.Session["loggedInUser"]; 

        public void CreateContact(Contact contact, Account account)
        {
            contact.OwnerAccountId = account.Id;

            List<Account> accounts = db.Accounts.ToList();

            //Kijk of nieuw contact al een account heeft. 
            if (accounts.Any(a => a.MobileNumber == contact.MobileNumber))
            {
                Account contactAccount = db.Accounts.Single(a => a.MobileNumber == contact.MobileNumber);
                contact.ContactAccountId = contactAccount.Id;
            }
     
            db.Contacts.Add(contact);
            db.SaveChanges(); 
        }

        public void DeleteContact(Contact contact)
        {
            db.Contacts.Remove(contact);
            db.SaveChanges();
        }

        public IEnumerable<Contact> GetAllContacts(Account account)
        {
            return db.Contacts.Where(c => c.OwnerAccountId == account.Id).OrderBy(c => c.Name);
        }

        public Contact GetContact(int? id)
        {
            return db.Contacts.Find(id);
        }

        public Contact GetContact(int mobileNumber)
        {
            return db.Contacts.Find(mobileNumber);
        }

        public void UpdateContact(Contact contact, Account account)
        {
            contact.OwnerAccountId = account.Id; 
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}