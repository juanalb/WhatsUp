using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUp.Models; 

namespace WhatsUp.Repositories
{
    interface IAccountRepository
    {
        Account GetAccount(string username, string password);
        Account GetAccountById(int? id);
        Account GetAccount(int mobileNumber);
        List<Account> GetAccounts(IEnumerable<Contact> contacten);
        void RegisterAccount(Account account);
        bool ContactHasAccount(int mobileNumber);
    }
}
