using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDF.Web.Models.Form
{
    public class Form2AModel
    {
        public Form2AModel()
        {

            formTwoModel = new List<FormTwoModel>();
        }

        public List<SoldPropertyDetails> soldPropertyDetails { get; set; } = new List<SoldPropertyDetails>();
        public List<FormTwoModel> formTwoModel { get; set; }
        public int employeeid { get; set; }
        public string employeecode { get; set; }
        public string DOA { get; set; }
        public string employeename { get; set; }

        public string role { get; set; }

        public string groups { get; set; }

        public string band { get; set; }

        public bool not_applicable { get; set; }
        public string designation { get; set; }
        public string location { get; set; }
        public string period_flag { get; set; }
        public DateTime? action_date { get; set; }
    }
    public class DropdownModel
    {
        public int dropdown_key { get; set; }
        public string dropdown_name { get; set; }
    }
    public class FormTwoModel
    {
        // public int employeeid { get; set; }
        public string employeecode { get; set; }
        public DateTime onDateAction { get; set; }
        public string status { get; set; }
        public DateTime actionDate { get; set; }
        public int natureOfProperty { get; set; }
        public string presentMarketValue { get; set; }
        public int propertyOwnedBy { get; set; }
        public string nameOfSpouse { get; set; }
        public int natureOfAcuisition { get; set; }
        public DateTime? dateOfAcuisition { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public string streetAddress { get; set; }
        public string acquiredName { get; set; }
        public string acquiredAddress { get; set; }
        public string housingLoan { get; set; }
        public string pfLoan { get; set; }
        public string saving { get; set; }
        public string others { get; set; }
        public string total { get; set; }
        public string annualIncomeFromProperty { get; set; }
        public DateTime dateOfPurchaseProperty { get; set; }
        public string remarks { get; set; }

    }
    public class PropertyDetails
    {
        public string created_date2 { get; set; }
        public string employeecode { get; set; }
        public string employeename { get; set; }
        public DateTime onDateAction { get; set; }
        public string status { get; set; }
        public string actionDate2 { get; set; }
        public DateTime actionDate { get; set; }
        public String natureOfProperty { get; set; }
        public string presentMarketValue { get; set; }
        public String propertyOwnedBy { get; set; }
        public string nameOfSpouse { get; set; }
        public String natureOfAcuisition { get; set; }
        public DateTime dateOfAcuisition { get; set; }
        public string dateOfAcuisition2 { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public string streetAddress { get; set; }
        public string acquiredName { get; set; }
        public string acquiredAddress { get; set; }
        public string housingLoan { get; set; }
        public string pfLoan { get; set; }
        public string saving { get; set; }
        public string others { get; set; }
        public string total { get; set; }
        public string annualIncomeFromProperty { get; set; }
        public string dateOfPurchaseProperty { get; set; }
        public string remarks { get; set; }
        public string submission_period { get; set; }
        public string period_flag { get; set; }
        public string elg_period { get; set; }
        public DateTime created_date { get; set; }
    }

    public class PropertyDetails_AutoFill
    {
        // public int employeeid { get; set; }
        public int property_id { get; set; }
        public string employeecode { get; set; }
        public string employeename { get; set; }
        public DateTime onDateAction { get; set; }
        public string status { get; set; }
        public DateTime actionDate { get; set; }
        public int natureOfProperty { get; set; }
        public string presentMarketValue { get; set; }
        public int propertyOwnedBy { get; set; }
        public string nameOfSpouse { get; set; }
        public int natureOfAcuisition { get; set; }
        public DateTime dateOfAcuisition { get; set; }
        public string dateOfAcuisition2 { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public string streetAddress { get; set; }
        public string acquiredName { get; set; }
        public string acquiredAddress { get; set; }
        public string housingLoan { get; set; }
        public string pfLoan { get; set; }
        public string saving { get; set; }
        public string others { get; set; }
        public string total { get; set; }
        public string annualIncomeFromProperty { get; set; }
        public string dateOfPurchaseProperty { get; set; }
        public string remarks { get; set; }
        public string submission_period { get; set; }
        public string period_flag { get; set; }
        public string elg_period { get; set; }
        public DateTime created_date { get; set; }
    }

    public class SoldPropertyDetails
    {
        public int property_id { get; set; }
        public string employeecode { get; set; }
        public string sold_to { get; set; }
        public string sold_amt { get; set; }
        public DateTime? sold_date { get; set; }
        public string period { get; set; }
    }

}
