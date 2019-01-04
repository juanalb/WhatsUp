using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhatsUp.Models;
using WhatsUp.Repositories;

namespace WhatsUp.Controllers
{
    public class ContactsController : Controller
    {
        private WhatsUpContextDB db = new WhatsUpContextDB();
        private ContactRepository contactRepo = new ContactRepository();
        private AccountRepository accountRepo = new AccountRepository(); 
        private Account account;

        // GET: Contacts
        // Toon alle contacten van de OwnerAccount
        [Authorize]
        public ActionResult Index()
        {
            account = (Account)Session["loggedInUser"];
            return View(contactRepo.GetAllContacts(account));
        }

        // GET: Contacts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,MobileNumber,OwnerAccountId,ContactAccountId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                Account account = (Account)Session["loggedInUser"];
                contactRepo.CreateContact(contact, account);
                if(accountRepo.ContactHasAccount(contact.MobileNumber))
                {
                    return RedirectToAction("NewChat", "Chat", new { id = contact.MobileNumber });
                }
            }
            else
            {
                ModelState.AddModelError("Create contact-error", "Some of the fields are wrong");
            }

            return RedirectToAction("Index"); 
        }

        // GET: Contacts/Edit/5
        // Zoek ContactAccountId
        // OwnerAccount aan de hand van de session bepalen
        // Wijzigen van naam en nummer
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactRepo.GetContact(id); 
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,MobileNumber,OwnerAccountId,ContactAccountId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                Account account = (Account)Session["loggedInUser"];
                contactRepo.UpdateContact(contact, account);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactRepo.GetContact(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contactRepo.GetContact(id);
            contactRepo.DeleteContact(contact);
            return RedirectToAction("Index");
        }
    }
}
