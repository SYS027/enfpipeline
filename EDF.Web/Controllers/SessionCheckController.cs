using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITASSET.Controllers.WebControllers
{
    public class SessionCheckController : Controller
    {
        // GET: SessionCheck
        public ActionResult Index()
        {

            SessionObject obj = new SessionObject();
            //obj.EmpId = 1;
            //obj.EmpName = "Rajesh Chandran";
            //obj.RoleId = 1;
            //obj.RoleName = "Admin";

            obj.EmpId = 2;
            obj.EmpName = "Pushpraj";
            obj.RoleId = 2;
            obj.RoleName = "Employee";


            Session["EmpSession"] = obj;
            //obj.EmpId = 3;
            //obj.EmpName = "Nadeem Nakade";
            //obj.RoleId = 3;
            //obj.RoleName = "Manager";

            return RedirectToAction("Employee", "AddTourRequest"); ;
        }
    }
}