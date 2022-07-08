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
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id)
        {
            var data = db.Contacts.Where(x => x.ContactsId == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, Contact contact)
        {

            var data = db.Contacts.FirstOrDefault(x => x.ContactsId == id);
            if(data != null)
            {
                data.ContactsId = contact.ContactsId;
                data.Nombre = contact.Nombre;
                data.Apellido = contact.Apellido;
                data.Email = contact.Email;
                data.Telefono = contact.Telefono;
                data.UserId = contact.UserId;
                db.SaveChanges();
                return RedirectToAction("Contacts");
            }
            return View();
            
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
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