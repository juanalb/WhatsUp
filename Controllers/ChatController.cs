using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhatsUp.Models;
using WhatsUp.Models.Presentation_Models;
using WhatsUp.Repositories;

namespace WhatsUp.Controllers
{
    public class ChatController : Controller
    {

        private ChatRepository chatRepo = new ChatRepository();
        private AccountRepository accountRepo = new AccountRepository();
        private ContactRepository contactRepo = new ContactRepository();
        private MessageRepository messageRepo = new MessageRepository(); 

        // GET: Chats
        [Authorize]
        public ActionResult Index()
        {
            Account account = (Account)Session["loggedInUser"];
            ChatsView model = new ChatsView();

            model.chats = chatRepo.GetAllChats(account);
            foreach (Chat chat in model.chats)
            {
                Message lastMessage = messageRepo.GetLastMessage(chat.Id);
                chat.Messages = new List<Message>();
                chat.Accounts = new List<Account>();
                chat.Messages.Add(lastMessage);

                if(chat.Account1_Id == account.Id)
                {
                    chat.Accounts.Add(accountRepo.GetAccountById(chat.Account2_Id));
                }
                else
                {
                    chat.Accounts.Add(accountRepo.GetAccountById(chat.Account1_Id));
                }
            }
            model.userAccount = account;       
            return View(model);
        }

        [Authorize]
        public ActionResult Chat(int id)
        {
            Account account = (Account)Session["loggedInUser"];
            ChatView model = new ChatView();

            model.userAccount = account;
            model.chat = chatRepo.GetChat(id);
            model.messages = messageRepo.GetAllMessages(id);
            model.chat.Accounts = new List<Account>(); 
            if (model.chat.Account1_Id == account.Id)
            {
                model.chat.Accounts.Add(accountRepo.GetAccountById(model.chat.Account2_Id));
            }
            else
            {
                model.chat.Accounts.Add(accountRepo.GetAccountById(model.chat.Account1_Id));
            }
            //Je krijgt een ChatId mee vanuit de index
            //Je wilt een model terugsturen met: Account, ?Chat, Messages (ChatViewModel)
            return View(model); 
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Chat(/*[Bind(Include = "Id,Chat,Sender_Id,Receiver_Id,MessageText")]*/ Message message, int id)
        {
            if(ModelState.IsValid)
            {
                message.Chat_Id = id; 
                message.TimeOfMessage = DateTime.Now; 
                messageRepo.SendMessage(message);
            }
            else
            {
                ModelState.AddModelError("Chat-error", "Those are some weird characters you want to send...");
            }


            return RedirectToAction("Chat", new { id = id });
        }
        
        //id ==  mobilenumber
        [Authorize]
        public ActionResult NewChat(int id)
        {
            Account userAccount = (Account)Session["loggedInUser"];
            Account contactAccount = accountRepo.GetAccount(id);

            Chat chat = chatRepo.GetChat(userAccount, contactAccount.Id);
            
            if(chat == null)
            {
                
                chatRepo.CreateChat(userAccount, contactAccount);
            }

           return RedirectToAction("Index"); 
        }
    }
}