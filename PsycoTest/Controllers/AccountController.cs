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
     
        DBModel.ModelDBPsycoTest DbPsyco = new DBModel.ModelDBPsycoTest();


        [HttpGet]
        public ActionResult Register()
        {
            Models.User newUser = new Models.User();
            ViewBag.GroupId = DbPsyco.Group.ToList();

            return View(newUser);

        }

        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            ViewBag.GroupId = DbPsyco.Group.ToList();

            if (ModelState.IsValid)
            {
                if (user.UserLogin == "Admin" || user.UserLogin == "admin")
                {
                    ModelState.AddModelError("Ошибка", "Пользователь с логином администратора уже существует!");
                    return View(user);
                }
                else
                {
                    if (!DbPsyco.User.Any(m => m.UserLogin == user.UserLogin))
                    {
                        var age = DateTime.Now.Year - user.UserDateBirth.Year;

                        if (age < 16)
                        {
                            ModelState.AddModelError("Ошибка", "Возраст пользователя должен быть больше или равен 16 лет!");
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
                            DbPsyco.User.Add(newUser);
                            DbPsyco.SaveChanges();
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
                if (DbPsyco.User.Where(m => m.UserLogin == userSet.Login && m.UserPassword == userSet.Password).FirstOrDefault() == null)
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
                    Session["UserId"] = DbPsyco.User.Single(x => x.UserLogin == userSet.Login).UserId;
                    Session["UserLogin"] = userSet.Login;
                    Session["UserFirstName"] = DbPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserFirstName).FirstOrDefault();
                    Session["UserLastName"] = DbPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserLastName).FirstOrDefault();
                    Session["UserPatronymic"] = DbPsyco.User.Where(u => u.UserLogin == userSet.Login).Select(u => u.UserPatronymic).FirstOrDefault();
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