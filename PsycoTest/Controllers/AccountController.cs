using PsycoTest.DBModel;
using PsycoTest.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PsycoTest.Controllers
{
    public class AccountController : Controller
    {
     
        DBModel.DBPsycoTest DBPsyco = new DBModel.DBPsycoTest();


        [HttpGet]
        public ActionResult Register()
        {
            Models.User newUser = new Models.User();
            ViewBag.GroupId = DBPsyco.Group.ToList();

            return View(newUser);

        }

        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            ViewBag.GroupId = DBPsyco.Group.ToList();

            if (ModelState.IsValid)
            {
                if (user.UserLogin == "Admin" || user.UserLogin == "admin")
                {
                    ModelState.AddModelError("Ошибка", "Пользователь с логином администратора уже существует!");
                    return View(user);
                }
                else
                {
                    if (!DBPsyco.User.Any(m => m.UserLogin == user.UserLogin))
                    {
                        var age = DateTime.Now.Year - user.UserDateBirth.Year;

                        if (age < 16)
                        {
                            ModelState.AddModelError("Ошибка", "Дата рождения пользователя должна быть больше 16 лет!");
                            return View(user);
                        }
                        else
                        {
                            DBModel.User newUser = new DBModel.User();
                            newUser.UserLogin = user.UserLogin;
                            newUser.UserPassword = user.UserPassword;
                            newUser.UserFirstName = user.UserFirstName;
                            newUser.UserLastName = user.UserLastName;
                            newUser.UserPatronymic = user.UserPatronymic;
                            newUser.UserDateBirth = user.UserDateBirth;
                            newUser.UserGroupId = user.UserGroupId;
                            DBPsyco.User.Add(newUser);
                            DBPsyco.SaveChanges();
                            return RedirectToRoute(new { controller = "Account", action = "Login" });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Ошибка", "Пользователь с логином " + user.UserLogin + " уже существует");
                        return View(user);
                    }
                }

            }
            return View(user);


        }

        public ActionResult Login()
        {
            Models.UserLogin EntryUser = new Models.UserLogin();

            return View(EntryUser);
        }

        [HttpPost]
        public ActionResult Login(Models.UserLogin userSet)
        {
            if(ModelState.IsValid)
            {
                if (DBPsyco.User.Where(m => m.UserLogin == userSet.Login && m.UserPassword == userSet.Password).FirstOrDefault() == null)
                {
                    if (userSet.Login == "Admin" && userSet.Password == "Admin123")
                    {
                        Session["UserLogin"] = userSet.Login;
                        Session["UserFirstName"] = "Мария";
                        Session["UserLastName"] = "Меркулова";
                        Session["UserPatronymic"] = "Александровна";
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }
                    else
                    {
                        ModelState.AddModelError("Ошибка", "Такого пользователя не существует");
                        return View(userSet);
                    }
                }
                else
                {
                    Session["UserLogin"] = userSet.Login;
                    Session["UserFirstName"] = DBPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserFirstName).FirstOrDefault();
                    Session["UserLastName"] = DBPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserLastName).FirstOrDefault();
                    Session["UserPatronymic"] = DBPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserPatronymic).FirstOrDefault();
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            return View(userSet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToRoute(new { controller = "Account", action = "Login" });
        }
    }
}