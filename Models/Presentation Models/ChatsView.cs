using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsUp.Models.Presentation_Models
{
    public class ChatsView
    {
        public IEnumerable<Chat> chats { get; set; }
        public IEnumerable<Account> contacts { get; set; }
        [Display(Name = "Last Message")]
        public IEnumerable<Message> lastMessages { get; set; }
        public Account userAccount { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public DateTime timeOfMessage { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string messageText { get; set; }

    }
}