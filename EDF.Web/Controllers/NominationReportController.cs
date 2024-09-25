using EDF.Web.DataAccessLayer;
using EDF.Web.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EDF.Web.Controllers
{
    public class NominationReportController : Controller
    {
        DALNominationReport dalNomination = new DALNominationReport();
        // GET: NominationReport
       
        public ActionResult Index(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, string nominationTypeSearch, string familyMemberType)
        {
            ViewBag.EmpName = dalNomination.GetEmpNameDropdown();
            ViewBag.NominationCategory = dalNomination.GetNominationCategoryDropdown();
          
            List<NominationReportModel> nominationReports = dalNomination.GetNominationReport(employeeCodeSearch, from_date, to_date, nominationTypeSearch, familyMemberType);

            if (Request.IsAjaxRequest())
            {                
                return Json(nominationReports, JsonRequestBehavior.AllowGet);
            }
            else
            {               
                return View(nominationReports);
            }
        }

        public ActionResult ExportToExcel(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, string nominationTypeSearch, string familyMemberType)
        {

            List<NominationReportModel> nominationReports = dalNomination.GetNominationReport(employeeCodeSearch, from_date, to_date, nominationTypeSearch, familyMemberType);

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Nomination Report");

                // Add heading
                ws.Cells["A1:F1"].Merge = true;
                ws.Cells["A1"].Value = "Nomination Report";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 16;
                ws.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center align heading

                // Add headers            
                ws.Cells["A2:F2"].Style.Font.Bold = true;
                ws.Cells["A2:F2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                ws.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                ws.Cells["A2:F2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Cells["A2"].Value = "Employee Code";
                ws.Cells["B2"].Value = "Employee Name";
                ws.Cells["C2"].Value = "Family Member Name";
                ws.Cells["D2"].Value = "Nomination Type";
                ws.Cells["E2"].Value = "Relation With Employee";
                ws.Cells["F2"].Value = "Share of Percentage";
                           
                // Add data
                int row = 3;
                foreach (var item in nominationReports)
                {
                    ws.Cells["A" + row].Value = item.employeecode;
                    ws.Cells["B" + row].Value = item.employeename;
                    ws.Cells["C" + row].Value = item.first_name +" "+ item.last_name;
                    ws.Cells["D" + row].Value = item.nomination_category_name;
                    ws.Cells["E" + row].Value = item.relationship_name;
                    ws.Cells["F" + row].Value = item.share_of_percentage;                    
                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                byte[] excelBytes = excelPackage.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NominationReport.xlsx");
            }
        }


        public ActionResult AuditReport()
        {
            List<NominationReportModel> nominationauditReports = dalNomination.GetAuditReport();

            return View(nominationauditReports);
        }

    }



}