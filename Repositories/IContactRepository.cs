using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUp.Models;

namespace WhatsUp.Repositories
{
    interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts(Account account);
        Contact GetContact(int? id);
        Contact GetContact(int mobileNumber);
        void CreateContact(Contact contact, Account account);
        void UpdateContact(Contact contact, Account account); 
        void DeleteContact(Contact contact);
    }
}
