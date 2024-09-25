using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENF.Data_Access_Layer.DAL;
using static ENF.Models.EmpDetails;
using EDF.Web.Models;

namespace ENF.Controllers
{
    public class EmpDetailsController : Controller
    {
        DALEmpDetails iDALEmp = new DALEmpDetails();
        public ActionResult Index(EmpDetailsSearch e)
        {
            ViewBag.employeecode = UserManager.User.employeecode;
            var employeeDetails = iDALEmp.GetEmployeeDetails(e);
            var employeeRelationships = iDALEmp.GetEmployeeRelationshipDetails(e);

            var relationships = employeeRelationships
                .GroupBy(x => x.relationship_code)
                .Select(g => new EmployeeRelationship
                {
                    relationship_code = g.Key,
                    relationship_name = g.First().relationship_name
                })
                .ToList();
            ViewBag.eligible = relationships;
            ViewBag.reg_cod = employeeRelationships;

            return View(employeeDetails);
        }



        public ActionResult AddRecord(EmpDetailsSearch e)
        {
            var employeeRelationships = iDALEmp.GetEmployeeRelationshipDetails(e);

            var Region = employeeRelationships.GroupBy(x => new { x.relationship_code, x.relationship_name }).Select(x => new EmployeeRelationship()
            {
                relationship_code = x.FirstOrDefault().relationship_code,
                relationship_name = x.FirstOrDefault().relationship_name
            });

            ViewBag.reg_cod = Region;

            return View();
        }

        public JsonResult CreateRecord(EmployeeDetailsNC e)
        {
            var result = iDALEmp.InsertCategoryMaster(e);
            return Json(new { status = result.Status, message = result.Message });
        }
        public ActionResult UpdateStatus(int nominationId, char status, String nominationCategoryCode)
        {
            var response = iDALEmp.UpdateRec(nominationId, status, nominationCategoryCode);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateEligibleCategories(List<CategoryUpdateModel> data)
        {
            var res = iDALEmp.Updatestatus(data);
            return Json(new { status = res.Status, message = res.Message });
        }


    }
}