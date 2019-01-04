using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhatsUp.Models;
using WhatsUp.Repositories;

namespace WhatsUp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private WhatsUpContextDB db = new WhatsUpContextDB();
        private Account account = (Account)HttpContext.Current.Session["loggedInUser"];

        public void CreateChat(Account userAccount, Account contactAccount)
        {
            Chat chat = new Chat();
            chat.Account1_Id = userAccount.Id;
            chat.Account2_Id = contactAccount.Id;

            db.Chats.Add(chat);
            db.SaveChanges();
        }

        public IEnumerable<Chat> GetAllChats(Account userAccount)
        {
            return db.Chats.Where(c => c.Account1_Id == userAccount.Id || c.Account2_Id == userAccount.Id);
        }

        public Chat GetChat(int? id)
        {
            return db.Chats.Single(c => c.Id == id);
        }

        public Chat GetChat(Account userAccount, int contactAccountId)
        {
            try
            {
               return db.Chats.Single(c => c.Account1_Id == userAccount.Id && c.Account2_Id == contactAccountId || c.Account1_Id == contactAccountId && c.Account2_Id == userAccount.Id);
            }
            catch(Exception)
            {
                return null;    
            }
        }

    }
}