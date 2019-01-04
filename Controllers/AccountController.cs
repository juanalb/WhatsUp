using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WhatsUp.Models;
using WhatsUp.Repositories;

namespace WhatsUp.Controllers
{
    public class AccountController : Controller
    {
        private WhatsUpContextDB db = new WhatsUpContextDB();
        private AccountRepository repo = new AccountRepository(); 
         
        // GET: Account
        public ActionResult Login()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInViewModel model)
        {
            if (ModelState.IsValid)
            {
                Account account = repo.GetAccount(model.Username, model.Password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.Username, false);
                    Session["loggedInUser"] = account;
                    return RedirectToAction("Index", "Contacts");
                }
                else
                { 
                    ModelState.AddModelError("login-error", "The mobilenumber or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,MobileNumber,Username,Password,Name")] Account account)
        {
            if (ModelState.IsValid)
            {
                repo.RegisterAccount(account); 
                
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("Register-error", "Fields are incorrect");
            }
            return View(); 
        }
    }
}