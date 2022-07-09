using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TareaWeb1.Models;

namespace TareaWeb1.Controllers
{
    public class HomeController : Controller
    {
        ContactListMVCEntities db = new ContactListMVCEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(a => a.username.Equals(user.username) && a.password.Equals(user.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.id;
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Contacts");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            var id = Session["UserID"];
            if (id != null)
            {
                var obj = db.Contacts.ToList().Where(x => x.UserId.Equals(id));
                return View(obj);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Update(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if(contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }
        [HttpPost]
        public ActionResult Update(Contact contact)
        {

            if (ModelState.IsValid)
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Contacts");
            }

            return View(contact);
            
        }
        public ActionResult Create(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if(contact == null)
            {
                
                return View(contact);
            }
            return RedirectToAction("Contacts");
        }
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(new Contact
                {
                    Nombre = contact.Nombre,
                    ContactsId = db.Contacts.Count() + 1,
                    Apellido = contact.Apellido,
                    Email = contact.Email,
                    UserId = (int)Session["UserID"],
                    Telefono = contact.Telefono
                });
                db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            return View(contact);
            
        }

        public ActionResult Delete(int id)
        {
            var data = db.Contacts.FirstOrDefault(x => x.ContactsId == id);
            if(data != null)
            {
                db.Contacts.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            return View();
        }


    }
}