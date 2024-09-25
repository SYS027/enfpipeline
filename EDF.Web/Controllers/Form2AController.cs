using EDF.Web.DataAccessLayer;
using EDF.Web.Models;
using EDF.Web.Models.Form;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDF.Web.Controllers
{
    [CustomAuthorize]
    public class Form2AController : Controller
    {
        // GET: Form2A

        DalForm2A objForm2 = new DalForm2A();
        
        public ActionResult Index()
        {
            string employeecode = UserManager.User.employeecode;
            Session["curr_elg_period"] = "";
            ViewBag.PropertyDetails = objForm2.GetPropertyDetails(employeecode);

            return View();
        }

        public ActionResult Item()
        {
            string employeecode = UserManager.User.employeecode;
            // ViewBag.EmployeeCode = employeecode;
            ViewBag.AsOnDate = objForm2.GetEmployeeElgPrd2A(employeecode);
            ViewBag.Form2_A_AutoFill = objForm2.GetForm2_A_AutoFill(employeecode);

            Session["curr_elg_period"] = ViewBag.AsOnDate[0].action_date_1;
            ViewBag.stat = objForm2.GetFormDropdownList(1);
            ViewBag.property = objForm2.GetFormDropdownList(4);
            ViewBag.propertyowned = objForm2.GetFormDropdownList(9);
            ViewBag.acquisition = objForm2.GetFormDropdownList(14);

            return View();
        }

        public JsonResult AddFormtwoData(Form2AModel objModel)
        {
            objModel.action_date = (DateTime?)Session["curr_elg_period"];
            objModel.employeecode = UserManager.User.employeecode;
            var result = objForm2.AddFormtwoData(objModel);
            return Json(result);
        }
    }
}