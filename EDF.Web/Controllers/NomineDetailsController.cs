using EDF.Web.Models;
using ENF.Data_Access_Layer.DAL;
using ENF.Models;
using System.Web.Mvc;
using static ENF.Models.EmpDetails;

namespace ENF.Controllers
{
    public class NomineDetailsController : Controller
    {

        DALNomineeDetails dAL = new DALNomineeDetails();
        DALEmpDetails iDALEmp = new DALEmpDetails();


        public ActionResult Index(EmpDetailsSearch e)
        {
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            var employeeDetails = dAL.GetEmployeeFullDetails(employeeID);
            return View(employeeDetails);
        }
        public JsonResult Submit(NominationCategory model)//33964
        {
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            var a = dAL.InsertFamilyPercentageSharing(model, employeeID);
            return Json(new { success = false, message = "Validation failed." });
        }

        public JsonResult GetFamilyDropdown(string nomination_cat_code)
        {
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            var a = dAL.GetEmployeeFalilyDetails(employeeID, nomination_cat_code);
            return Json(a);
        }

        public ActionResult _NomineeDetails(string nomination_cat_code)
        {
            int employeeID = (int)UserManager.User.employeeid;
            ViewBag.EmployeeID = employeeID;
            var employeeFamilyDetails = dAL.GetEmployeeFalilyDetails(employeeID, nomination_cat_code);

            //var employeeId = 34320;

            //var familyPercentageHistory = dAL.GetFamilyPercentageSharingHistory(nomination_cat_code, employeeId);

            //var familyIds = employeeFamilyDetails
            //    .SelectMany(detail => detail.FamilyDetails)
            //    .Select(family => family.FamilyId)
            //    .Distinct()
            //    .ToList();

            //var deletedRecords = familyPercentageHistory
            //    .Where(record => familyIds.Contains(record.Family_Id) && record.Deleted_Date != null)
            //    .GroupBy(record => record.Family_Id)
            //    .Select(group => group.First()) 
            //    .ToList();

            //var deletedRecordsDict = deletedRecords
            //    .ToDictionary(record => record.Family_Id);

            //ViewBag.DeletedRecords = deletedRecordsDict;
            //var ListData = employeeFamilyDetails.OrderBy(x => x.FamilyDetails.OrderBy(y => y.pd_is_active)).ToList();

            return PartialView("_NomineeDetails", employeeFamilyDetails);
        }




    }
}
