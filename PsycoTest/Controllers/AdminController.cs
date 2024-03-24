using PsycoTest.DBModel;
using PsycoTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PsycoTest.Controllers
{
    public class AdminController : Controller
    {
        DBModel.DBPsycoTest dBPsyco = new DBModel.DBPsycoTest();
        [HttpGet]
        public ActionResult Users()
        {
            List<DBModel.User> dBModel = new List<DBModel.User>();   
            dBModel = dBPsyco.User.ToList();
            return View(dBModel.ToList());
        }

        public ActionResult Delete(int id = 0)
        {
            DBModel.User user = dBPsyco.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(int id = 0)
        {

            DBModel.User user = dBPsyco.User.Find(id);
            ViewBag.GroupId = dBPsyco.Group.ToList();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(DBModel.User user)
        {
            ViewBag.GroupId = dBPsyco.Group.ToList();

            if (ModelState.IsValid)
            {
                if (user.UserLogin == "Admin" || user.UserLogin == "admin")
                {
                    ModelState.AddModelError("Ошибка", "Пользователь с логином администратора уже существует!");
                    return View(user);
                }
                else
                {
                        var age = DateTime.Now.Year - user.UserDateBirth.Year;

                        if (age < 16)
                        {
                            ModelState.AddModelError("Ошибка", "Дата рождения пользователя должна быть больше 16 лет!");
                            return View(user);
                        }
                        else
                        {
                            dBPsyco.Entry(user).State = EntityState.Modified;
                            dBPsyco.SaveChanges();
                            return RedirectToAction("Users");
                        }
                    
                  }
  
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            DBModel.User user = dBPsyco.User.Find(id);
            dBPsyco.User.Remove(user);
            dBPsyco.SaveChanges();

            return RedirectToRoute(new { controller = "Admin", action = "Users" });

        }

        protected override void Dispose(bool disposing)
        {
            dBPsyco.Dispose();
            base.Dispose(disposing);
        }
    }
}