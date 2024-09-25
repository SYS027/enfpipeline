using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDF.Web.DataAccessLayer;
using EDF.Web.Models;

namespace EDF.Web.Controllers
{
    [CustomAuthorize]
    public class Form5Controller : Controller
    {
        // GET: Form5

        DalForm5 objForm = new DalForm5();

        public ActionResult Index()
        {
            string employeecode = UserManager.User.employeecode;

            ViewBag.RelativeDetails = objForm.GetRelativeList(employeecode);

            return View();
        }
        public ActionResult Item()
        {
            string employeecode = UserManager.User.employeecode;
            ViewBag.EmployeeDetails = objForm.GetEmployeeDetails(employeecode);
            ViewBag.RelativeDetailsAutoFill = objForm.GetRelativeList_AutoFill(employeecode);
            ViewBag.ElgPrd = objForm.GetEmployeeElgPrd(employeecode);

            ViewBag.status = objForm.GetFormDropdownList(1);
            ViewBag.RelationList = objForm.GetFormDropdownList(22);
            ViewBag.NatureEngagement = objForm.GetFormDropdownList(31);

            return View();
        }

        public ActionResult AddForm5(EmployeeDetails obj)
        {
            var result = objForm.AddRelative(obj);
            return Json(result);
        }
    }
}