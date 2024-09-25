using EDF.Web.DataAccessLayer;
using EDF.Web.Models;
using EDF.Web.Models.Form;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDF.Web.Controllers
{
    [CustomAuthorize]
    public class ReportController : Controller
    {
        DALReport objDALReports = new DALReport();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Form5Rpt(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {
            List<RelationDetails> lstRelativeDetails = new List<RelationDetails>();
            List<RelationDetails> lstReport = objDALReports.GetFormFiveReport(employeeCodeSearch, from_date, to_date, is_search_not_applicable, search_period_flag, search_band, departmentSearch, designationSearch);
            ViewBag.EmpName = objDALReports.GetEmpNameDropdown();
            ViewBag.band = objDALReports.GetFormDropdownList("Band");
            ViewBag.department = objDALReports.GetFormDropdownList("Department");
            ViewBag.designation = objDALReports.GetFormDropdownList("Designation");
            ViewBag.lstnotapplicable = objDALReports.GetApplicableDropdown();
            ViewBag.lstperiodflagfor5 = objDALReports.GetPeriodFlagDropdownfor5();

            if (Request.IsAjaxRequest())
            {
                return Json(lstReport, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(lstReport);
            }
        }


        public ActionResult ExportToExcel(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {

            List<RelationDetails> lstReport = objDALReports.GetFormFiveReport(employeeCodeSearch, from_date, to_date, is_search_not_applicable, search_period_flag, search_band, departmentSearch, designationSearch);

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Form5Report");

                // Add heading
                ws.Cells["A1:K1"].Merge = true;
                ws.Cells["A1"].Value = "Form5 Report";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 16;
                ws.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center align heading

                // Add headers            
                ws.Cells["A2:K2"].Style.Font.Bold = true;
                ws.Cells["A2:K2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                ws.Cells["A2:K2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A2:K2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                ws.Cells["A2:K2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Cells["A2"].Value = "Employee Code";
                ws.Cells["B2"].Value = "Employee Name";
                ws.Cells["C2"].Value = "Period";
                ws.Cells["D2"].Value = "Relationship With Employee";
                ws.Cells["E2"].Value = "Name Of Relative";
                ws.Cells["F2"].Value = "Nature Of Engagement";
                ws.Cells["G2"].Value = "Institution/Firm with Relative is associated";
                ws.Cells["H2"].Value = "Names of Mutual Funds/AMCs";
                ws.Cells["I2"].Value = "Designation/Agency/ARN Code etc";
                ws.Cells["J2"].Value = "Place Of Posting/Operation";
                ws.Cells["K2"].Value = "Action Date";
                // Add data
                int row = 3;
                foreach (var item in lstReport)
                {
                    ws.Cells["A" + row].Value = item.employeecode;
                    ws.Cells["B" + row].Value = item.employeename;
                    ws.Cells["C" + row].Value = item.quarter;
                    ws.Cells["D" + row].Value = item.relationshipEmployee;
                    ws.Cells["E" + row].Value = item.relativeName;
                    ws.Cells["F" + row].Value = item.natureOfEngagement;
                    ws.Cells["G" + row].Value = item.firmsAssociated;
                    ws.Cells["H" + row].Value = item.mutualfundName;
                    ws.Cells["I" + row].Value = item.relativeDesignation;
                    ws.Cells["J" + row].Value = item.placeOfPosting;
                    ws.Cells["K" + row].Value = item.actionDate;
                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                byte[] excelBytes = excelPackage.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Form5Report.xlsx");
            }
        }


        public ActionResult Form2ARpt(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {
            List<PropertyDetails> lstpropReport = objDALReports.GetFormTwoReport(employeeCodeSearch, from_date, to_date, is_search_not_applicable, search_period_flag, search_band, departmentSearch, designationSearch);
            ViewBag.EmpName = objDALReports.GetEmpNamefor2ADropdown();
            ViewBag.band = objDALReports.GetFormDropdownList("Band");
            ViewBag.department = objDALReports.GetFormDropdownList("Department");
            ViewBag.designation = objDALReports.GetFormDropdownList("Designation");
            ViewBag.lstapplicable = objDALReports.GetApplicableDropdownfor2A();
            ViewBag.lstperiodflag = objDALReports.GetPeriodFlagDropdownfor2A();

            if (Request.IsAjaxRequest())
            {
                return Json(lstpropReport, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(lstpropReport);
            }
        }


        public ActionResult ExportToExcel2ARpt(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {

            List<PropertyDetails> lstpropReport = objDALReports.GetFormTwoReport(employeeCodeSearch, from_date, to_date, is_search_not_applicable, search_period_flag, search_band, departmentSearch, designationSearch);

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Form2AReport");

                // Add heading
                ws.Cells["A1:X1"].Merge = true;
                ws.Cells["A1"].Value = "Form2A Report";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Size = 16;
                ws.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Center align heading

                // Add headers            
                ws.Cells["A2:X2"].Style.Font.Bold = true;
                ws.Cells["A2:X2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                ws.Cells["A2:X2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A2:X2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                ws.Cells["A2:X2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ws.Cells["A2"].Value = "Employee Code";
                ws.Cells["B2"].Value = "Employee Name";
                ws.Cells["C2"].Value = "Nature of Property";
                ws.Cells["D2"].Value = "Present Market Value";
                ws.Cells["E2"].Value = "Property Owned By";
                ws.Cells["F2"].Value = "Name Of Spouse";
                ws.Cells["G2"].Value = "Nature of Acuisition";
                ws.Cells["H2"].Value = "Date of Acuisition";
                ws.Cells["I2"].Value = "Country";
                ws.Cells["J2"].Value = "State";
                ws.Cells["K2"].Value = "City";
                ws.Cells["L2"].Value = "Pin Code";
                ws.Cells["M2"].Value = "Street Address";
                ws.Cells["N2"].Value = "Acquired Name";
                ws.Cells["O2"].Value = "Acquired Address";
                ws.Cells["P2"].Value = "House Loan";
                ws.Cells["Q2"].Value = "PF Loan";
                ws.Cells["R2"].Value = "Saving";
                ws.Cells["S2"].Value = "Others";
                ws.Cells["T2"].Value = "Total";
                ws.Cells["U2"].Value = "Annual Income Property";
                ws.Cells["V2"].Value = "Remarks";
                ws.Cells["W2"].Value = "Action Date";
                ws.Cells["X2"].Value = "As On Date";
                // Add data
                int row = 3;
                foreach (var item in lstpropReport)
                {
                    ws.Cells["A" + row].Value = item.employeecode;
                    ws.Cells["B" + row].Value = item.employeename;
                    ws.Cells["C" + row].Value = item.natureOfProperty;
                    ws.Cells["D" + row].Value = item.presentMarketValue;
                    ws.Cells["E" + row].Value = item.propertyOwnedBy;
                    ws.Cells["F" + row].Value = item.nameOfSpouse;
                    ws.Cells["G" + row].Value = item.natureOfAcuisition;
                    ws.Cells["H" + row].Value = item.dateOfAcuisition;
                    ws.Cells["I" + row].Value = item.country;
                    ws.Cells["J" + row].Value = item.state;
                    ws.Cells["K" + row].Value = item.city;
                    ws.Cells["L" + row].Value = item.pinCode;
                    ws.Cells["M" + row].Value = item.streetAddress;
                    ws.Cells["N" + row].Value = item.acquiredName;
                    ws.Cells["O" + row].Value = item.acquiredAddress;
                    ws.Cells["P" + row].Value = item.housingLoan;
                    ws.Cells["Q" + row].Value = item.pfLoan;
                    ws.Cells["R" + row].Value = item.saving;
                    ws.Cells["S" + row].Value = item.others;
                    ws.Cells["T" + row].Value = item.total;
                    ws.Cells["U" + row].Value = item.annualIncomeFromProperty;
                    ws.Cells["V" + row].Value = item.remarks;
                    ws.Cells["W" + row].Value = item.created_date2;
                    ws.Cells["X" + row].Value = item.actionDate2;
                    row++;
                }

                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                byte[] excelBytes = excelPackage.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Form5Report.xlsx");
            }
        }


    }
}