using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhatsUp.Models;

namespace WhatsUp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        WhatsUpContextDB db = new WhatsUpContextDB();

        //Sorted on the time
        public IEnumerable<Message> GetAllMessages(int chatId)
        {
            return db.Messages.Where(m => m.Chat_Id == chatId).OrderBy(m => m.TimeOfMessage);
        }

        public Message GetLastMessage(int chatId)
        {
            try
            {
                return  db.Messages.Where(m => m.Chat_Id == chatId)
                             .OrderByDescending(m => m.TimeOfMessage)
                             .First();
            }
            catch(Exception)
            {
                return null;
            }
                   
        }

        public Message GetMessage(int chatId)
        {
            return db.Messages.Find(chatId);
        }

        public void SendMessage(Message message)
        {
            db.Entry(message).State = EntityState.Added;
            db.SaveChanges();
        }
    }
}