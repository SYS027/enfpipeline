using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENF_NEW.Models
{
    public class FamilyDetails
    {
        public int employee_id { get; set; }
        public string employeecode { get; set; }
        public int family_id { get; set; }
        public string relationship_code { get; set; }
        public string relationship_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public int age { get; set; }
        public string contact_no { get; set; }
        public string email_id { get; set; }
        public string qualification { get; set; }
        public decimal annual_income { get; set; }
        public char email_send_status { get; set; }    
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public string bank_ifsc { get; set; }
        public string bank_account_name { get; set; }
        public string bank_account_number { get; set; }       
        public int country_id { get; set; }
        public string cm_name { get; set; }
        public int state_id { get; set; }
        public string state_name { get; set; }
        public int city_id { get; set; }
        public string city_name { get; set; }
        public string pin_code { get; set; }
        public string address { get; set; }
        public char is_active { get; set; }
        public bool is_dependent {  get; set; } 
        public bool is_employeed {  get; set; } 
        //public DateTime de_active_date { get; set; }


        public class DeactiveModel
        {
            public int id { get; set; }
            public int reason_id { get; set; }
            public string reason_name { get; set; }
            public string remark { get; set; }
            public char is_active { get; set; }
            public DateTime de_active_date { get; set; }
        }

        public class ReasonList
        {
            public int reason_id { get; set; }
            public string reason_name { get; set; }
        }

        public class Relationtype
        {
            public string relationship_code { get; set; }
            public string relationship_name { get; set; }
        }

        public class BankList
        {
            public int bank_id { get; set; }
            public string bank_name { get; set; }
        }

        public class CountryList
        {
            public int cm_id { get; set; }
            public string cm_name { get; set; }
        }

        public class StateList
        {
            public int state_id { get; set; }
            public string state_name { get; set; }
        }

        public class CityList
        {
            public int city_id { get; set; }
            public string city_name { get; set; }
        }


    }
}