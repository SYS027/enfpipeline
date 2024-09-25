using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITASSET.Controllers.WebControllers;
using EDF.Web.Models;
using EDF.Web.DataAccessLayer;
using Newtonsoft.Json;

namespace EDF.Web.Controllers
{
    public class LoginController : Controller
    {
        DalForm5 objForm = new DalForm5();
        DalForm2A objForm2A = new DalForm2A();
        DalLogin objlogin = new DalLogin();
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Index(string login_id)
        //{
        //    Login userlg = new Login();

        //    if (string.IsNullOrEmpty(login_id) && Request.Url.Host.Contains("localhost"))
        //    {
        //        login_id = "2941";
        //        //login_id = "2020";
        //        Session["login_user"] = Login.encrypt(login_id);

        //        var result = objlogin.GetLoginEmpDetails(login_id);
        //        Session["UserDetails"] = JsonConvert.SerializeObject(result);

        //        //Session["employeecode"] = result.employeecode;
        //        //Session["employeename"] = result.employeename;

        //    }
        //    else
        //    {
        //        Session["login_user"] = login_id;
        //        login_id = Login.Decrypt(Request.QueryString["login_id"]);


        //        var result = objlogin.GetLoginEmpDetails(login_id);
        //        Session["UserDetails"] = JsonConvert.SerializeObject(result);

        //        //Session["employeecode"] = result.employeecode;
        //        //Session["employeename"] = result.employeename;

        //    }

        //    SessionObject obj = new SessionObject();

        //    return RedirectToAction("Index", "Main");

        //}

        //public JsonResult EligibilityCheck()
        //{
        //    string employeecode = UserManager.User.employeecode;
        //    var eligibilityCount = objForm.GetEmployeeElgPrd(employeecode).Count();
        //    var elgibForm2ACount = objForm2A.GetEmployeeElgPrd2A(employeecode).Count();

        //    return Json(new { countForm5 = Convert.ToString(eligibilityCount), countForm2A = Convert.ToString(elgibForm2ACount) });
        //}

        public ActionResult Index(string ReturnUrl)
        {

            if (Session["UserDetails"] != null)
            {
                return RedirectToAction("Index", "Main");
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel objModel)
        {
            var result = objlogin.ValidateUser(objModel);

            if (result != null)
            {
                if (result.employeeid != 0)
                {
                    string UserDetails = JsonConvert.SerializeObject(result);
                    Session["UserDetails"] = UserDetails;
                    //Session["EmployeeSideBarInfor"] = objDALUserManagement.GetEmplSidebarInfo(result.UserID);
                    //Session["user_menus"] = objDALUserManagement.GetMenus();
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ViewBag.errorMessage = MessageHelper.InvalidCredentials;
                }
            }
            else
            {
                ViewBag.errorMessage = MessageHelper.InvalidCredentials;
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index");
        }

    }
}
