using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int MobileNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }

}