using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsUp.Models.Presentation_Models
{
    public class ChatView
    {
        public int chatId { get; set; }//Hier nog overweging te maken om aparte model te maken voor index v chat en chat zelf...
        public Chat chat { get; set; }
        public Account userAccount { get; set; }
        [Display(Name = "Last message")]
        public Message lastMessage { get; set; }
        [Required]
        public Message message { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public DateTime timeOfMessage { get; set; }
        [Display(Name = "Message")]
        public string messageText { get; set; }
        public IEnumerable<Message> messages { get; set; }


    }
}