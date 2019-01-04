using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUp.Models;

namespace WhatsUp.Repositories
{
    interface IChatRepository
    {
        void CreateChat(Account userAccount, Account contactAccount);
        Chat GetChat(int? id);
        Chat GetChat(Account userAccount, int contactAccountId);
        IEnumerable<Chat> GetAllChats(Account userAccount);

    }
}
