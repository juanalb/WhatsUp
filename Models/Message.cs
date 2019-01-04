using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsUp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int Chat_Id { get; set; }
        public int Sender_Id { get; set; }
        public int Receiver_Id { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeOfMessage { get; set; }
    }
}