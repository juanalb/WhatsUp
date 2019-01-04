using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsUp.Models;

namespace WhatsUp.Repositories
{
    interface IMessageRepository
    {
        IEnumerable<Message> GetAllMessages(int chatId);
        Message GetMessage(int chatId);
        Message GetLastMessage(int chatId);
        void SendMessage(Message message);  
    }
}
