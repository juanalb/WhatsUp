using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int Account1_Id { get; set; }
        public int Account2_Id { get; set; }

        public List<Message> Messages;
        public ICollection<Account> Accounts;

    }
}