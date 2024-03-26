using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PsycoTest.Controllers
{
    public class TestsController : Controller
    {
        DBModel.ModelDBPsycoTest dBPsyco = new DBModel.ModelDBPsycoTest();

        public ActionResult ListTest()
        {
            List<DBModel.Test> dBModel = new List<DBModel.Test>();
            dBModel = dBPsyco.Test.ToList();
            return View(dBModel.ToList());
        }


        public ActionResult Test(int id = 0)
        {
            DBModel.Test test = dBPsyco.Test.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }
    }
}