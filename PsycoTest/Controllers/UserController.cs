using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsycoTest.Controllers
{
    public class UserController : Controller
    {


        DBModel.ModelDBPsycoTest dBPsyco = new DBModel.ModelDBPsycoTest();

        public ActionResult UserProfile(int id = 0)
        {
            DBModel.User user = dBPsyco.User.Find(Session["UserId"]);
            ViewBag.GroupId = dBPsyco.Group.ToList();

            Models.User profile = new Models.User();
            profile.UserLogin = user.UserLogin;
            profile.UserPassword = user.UserPassword;
            profile.UserFirstName = user.UserFirstName;
            profile.UserLastName = user.UserLastName;
            profile.UserPatronymic = user.UserPatronymic;
            profile.UserDateBirth = user.UserDateBirth;
            profile.UserGroupId = user.UserGroupId;

            return View(profile);
        }

        [HttpPost]
        public ActionResult UserProfile(Models.User profile)
        {
            ViewBag.GroupId = dBPsyco.Group.ToList();
            DBModel.User user = dBPsyco.User.Find(Session["UserId"]);


            if (ModelState.IsValid)
             {
                user.UserLogin = profile.UserLogin;
                user.UserPassword = profile.UserPassword;
                user.UserFirstName = profile.UserFirstName;
                user.UserLastName = profile.UserLastName;
                user.UserPatronymic = profile.UserPatronymic;
                user.UserDateBirth = profile.UserDateBirth;
                user.UserGroupId = profile.UserGroupId;

                dBPsyco.Entry(user).State = System.Data.Entity.EntityState.Modified;

                dBPsyco.SaveChanges();

                Session["UserLogin"] = profile.UserLogin;
                Session["UserFirstName"] = dBPsyco.User.Where(u => u.UserLogin == profile.UserLogin).Select(u => u.UserFirstName).FirstOrDefault();
                Session["UserLastName"] = dBPsyco.User.Where(u => u.UserLogin == profile.UserLogin).Select(u => u.UserLastName).FirstOrDefault();
                Session["UserPatronymic"] = dBPsyco.User.Where(u => u.UserLogin == profile.UserLogin).Select(u => u.UserPatronymic).FirstOrDefault();

                return RedirectToAction("UserProfile", "User");
            }

            return View(user);
        }
    }
}