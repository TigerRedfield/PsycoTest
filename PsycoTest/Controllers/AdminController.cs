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
        DBModel.ModelDBPsycoTest dBPsyco = new DBModel.ModelDBPsycoTest();
        [HttpGet]
        public ActionResult Users()
        {
            List<DBModel.User> dBModel = new List<DBModel.User>();   
            dBModel = dBPsyco.User.ToList();
            return View(dBModel.ToList());
        }

        public ActionResult WorkGroups()
        {
            List<DBModel.Group> groups = new List<DBModel.Group>();
            groups = dBPsyco.Group.ToList();
            return View(groups.ToList());
        }


        public ActionResult DeleteGroup(int id = 0)
        {
            DBModel.Group group = dBPsyco.Group.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("DeleteGroup")]
        public ActionResult DeleteConfirmedGroup(int id)
        {

            DBModel.Group group = dBPsyco.Group.Find(id);
            dBPsyco.Group.Remove(group);
            dBPsyco.SaveChanges();

            return RedirectToRoute(new { controller = "Admin", action = "WorkGroups" });

        }

        public ActionResult EditGroup(int id = 0)
        {

            DBModel.Group group = dBPsyco.Group.Find(id);
            ViewBag.GroupId = dBPsyco.Group.ToList();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost]
        public ActionResult EditGroup(DBModel.Group group)
        {
            if (ModelState.IsValid)
            {
                dBPsyco.Entry(group).State = EntityState.Modified;
                dBPsyco.SaveChanges();
                return RedirectToAction("WorkGroups");
            }
            return View(group);
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

        [HttpGet]
        public ActionResult CreateGroup()
        {
            Models.Group newGroup = new Models.Group();

            return View(newGroup);

        }

        [HttpPost]
        public ActionResult CreateGroup(Models.Group group)
        {
            if (ModelState.IsValid)
            {
                    if (!dBPsyco.Group.Any(m => m.GroupNumber == group.GroupNumber))
                    {
                            DBModel.Group newGroup = new DBModel.Group();
                            newGroup.GroupNumber = group.GroupNumber; 
                            dBPsyco.Group.Add(newGroup);
                            dBPsyco.SaveChanges();
                            return RedirectToRoute(new { controller = "Admin", action = "WorkGroups" });
                        
                    }
                    else
                    {
                        ModelState.AddModelError("Ошибка", "Такая группа с номером " + group.GroupNumber + " уже существует");
                        return View(group);
                    }
            }
            return View(group);


        }

        protected override void Dispose(bool disposing)
        {
            dBPsyco.Dispose();
            base.Dispose(disposing);
        }
    }
}