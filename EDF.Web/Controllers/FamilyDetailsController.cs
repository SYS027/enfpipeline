using EDF.Web.Models;
using ENF_NEW.Data_Access_Layer.DAL;
using ENF_NEW.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ENF_NEW.Controllers
{
    public class FamilyDetailsController : Controller
    {

        DALFamilyDetails familyDetails = new DALFamilyDetails();
        // GET: FamilyDetails        

        public ActionResult Index()
        {
            ViewBag.employeecode = UserManager.User.employeecode;
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            ViewBag.EmployeeName = UserManager.User.employeename;
            var deactive_reasonlst = familyDetails.GetDeactiveReason();
            ViewBag.deactive_reasonlst = new SelectList(deactive_reasonlst, "reason_id", "reason_name");
            List<FamilyDetails> details = familyDetails.GetFamilyDetailsList(employeeID);
            return View(details);

        }

        public ActionResult AddFamilyDetails(int? id)
        {
            var model = new FamilyDetails();
            ViewBag.employeecode = UserManager.User.employeecode;
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            ViewBag.EmployeeName = UserManager.User.employeename;

            //var employeeID = ViewBag.EmployeeID;
            var relation = familyDetails.GetRelationtype();
            ViewBag.relation = new SelectList(relation, "relationship_code", "relationship_name");

            var banklst = familyDetails.GetBankList();
            ViewBag.banklst = new SelectList(banklst, "bank_id", "bank_name");

            var country = familyDetails.GetCountryList();
            ViewBag.country = new SelectList(country, "cm_id", "cm_name");

            var state = familyDetails.GetStateList(id);
            ViewBag.state = new SelectList(state, "state_id", "state_name");

            var city = familyDetails.GetCityList(id);
            ViewBag.city = new SelectList(city, "city_id", "city_name");

            var selfDetails = familyDetails.GetSelfDetails(employeeID);
            if (selfDetails != null)
            {
                model.country_id = selfDetails.country_id;
                model.state_id = selfDetails.state_id;
                model.city_id = selfDetails.city_id;
                model.pin_code = selfDetails.pin_code;
                model.address = selfDetails.address;
            }

            return View(model);

        }

        public JsonResult statefilter(int id)
        {
            var state = familyDetails.GetStateList(id);
            return Json(state);
        }

        public JsonResult cityfilter(int id)
        {
            var city = familyDetails.GetCityList(id);
            return Json(city);
        }

        public JsonResult SaveFamilyDetails(FamilyDetails famInfo)
        {
            return Json(familyDetails.AddfamilyDetails(famInfo));
        }

        public ActionResult EditFamilyDetails(int? id)
        {
            var model = new FamilyDetails();
            ViewBag.employeecode = UserManager.User.employeecode;
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            ViewBag.EmployeeName = UserManager.User.employeename;

            //var employeeID = ViewBag.EmployeeID;
            var relation = familyDetails.GetRelationtype();
            ViewBag.relation = new SelectList(relation, "relationship_code", "relationship_name");

            var banklst = familyDetails.GetBankList();
            ViewBag.banklst = new SelectList(banklst, "bank_id", "bank_name");

            var country = familyDetails.GetCountryList();
            ViewBag.country = new SelectList(country, "cm_id", "cm_name");

            var state = familyDetails.GetStateList(id);
            ViewBag.state = new SelectList(state, "state_id", "state_name");

            var city = familyDetails.GetCityList(id);
            ViewBag.city = new SelectList(city, "city_id", "city_name");

            List<FamilyDetails> details = familyDetails.GetFamilyDetailsById(id);
            var selfDetails = familyDetails.GetSelfDetails(employeeID);
            if (selfDetails != null)
            {
                model.country_id = selfDetails.country_id;
                model.state_id = selfDetails.state_id;
                model.city_id = selfDetails.city_id;
                model.pin_code = selfDetails.pin_code;
                model.address = selfDetails.address;
            }
            var famDetails = details.FirstOrDefault();
          
            bool isSameAddress = famDetails != null &&
                                 famDetails.country_id == selfDetails.country_id &&
                                 famDetails.state_id == selfDetails.state_id &&
                                 famDetails.city_id == selfDetails.city_id &&
                                 famDetails.pin_code == selfDetails.pin_code &&
                                 famDetails.address == selfDetails.address;
            
            ViewBag.IsSameAddress = isSameAddress;

            return View(famDetails);
            //return View(details.FirstOrDefault());

        }

        public JsonResult UpdateFamilyDetails(FamilyDetails famInfo)
        {
            return Json(familyDetails.UpdatefamilyDetails(famInfo));
            //return Json(famInfo);
        }

        public ActionResult DeactivateFamilyMember(int familyId, string employeecode, int reason_id, string remark)
        {

            return Json(familyDetails.DeactivatefamilyMember(familyId, employeecode, reason_id, remark));
        }


    }
}
