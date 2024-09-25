using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENF.Models
{
    public class EmpDetails
    {
        //public class EmployeeDetailsNC
        //{
        //    public List<Perc> perc { get; set; } = new List<Perc>();
        //    public List<string> Names  { get; set; }
        //    public List<int> FamilyId { get; set; }
        //    public List<int> per { get; set; }

        //    public string nomination_id { get; set; }
        //    public string nomination_category_code { get; set; }
        //    public string nomination_category_name { get; set; }
        //    public string is_active { get; set; }
        //    public string created_by { get; set; }
        //    public string created_date { get; set; }
        //    public string de_active_date { get; set; }
        //    public string active_date { get; set; }
        //    public string Empflag { get; set; }
        //    public string relationship_code { get; set; }
        //    public string relationship_name { get; set; }
        //    public string[] Eligble { get; set; }
        //    public List<string> relationship_codes { get; set; } // Holds multiple codes
        //    public List<string> relationship_names { get; set; } // Holds multiple relationship names

        //    //added by anisha
        //    public int createdby { get; set; }
        //    public string employeecode { get; set; }
        //}

        //public class EmployeeRelationship
        //{
        //    public string nomination_id { get; set; }
        //    public string relationship_code { get; set; }
        //    public string relationship_name { get; set; }
        //    public string is_active { get; set; }
        //    public string created_by { get; set; }
        //    public string created_date { get; set; }
        //    public string de_active_date { get; set; }
        //    public string active_date { get; set; }
        //    public string Empflag { get; set; }
        //}
        //public class EmpDetailsSearch
        //{         
        //    public List<string> Nmae { get; set; }
        //    public string search_text { get; set; }
        //    public string category_code { get; set; }
        //    public char status { get; }
        //    //added by anisha
        //    public int createdby { get; set; }
        //    public string employeecode { get; set; }

        //}
        //public class CategoryEligibility
        //{
        //    public string NominationCategoryCode { get; set; }
        //    public string RelationshipNames { get; set; }
        //    public string RelationshipCode { get; set; }
        //}
        //public class CategoryUpdateModel
        //{
        //    public int NominationId { get; set; } // ID for the nomination
        //    public string NominationCategoryCode { get; set; } // Code for the nomination category
        //    public List<string> SelectedValues { get; set; } = new List<string>(); // List of selected values (initialized to avoid null reference)
        //    public bool IsActive { get; set; } // Indicates whether the category is active
        //    public List<string> RelationshipNames { get; set; } = new List<string>(); // List of relationship names (initialized to avoid null reference)
        //    public List<string> UnselectedValues { get; set; } = new List<string>();
        //    //added by anisha
        //    public int createdby { get; set; }
        //    public string employeecode { get; set; }
        //}

        //public class Perc
        //{
        //    public string Names { get; set; }
        //    public int FamilyId { get; set; }
        //    public int per { get; set; }

        //    public string relationship { get; set; }
        //}

        public class EmployeeDetailsNC
        {

            public List<Perc> perc { get; set; } = new List<Perc>();

            public string nomination_id { get; set; }
            public string nomination_category_code { get; set; }
            public string nomination_category_name { get; set; }
            public string is_active { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string de_active_date { get; set; }
            public string active_date { get; set; }
            public string Empflag { get; set; }
            public string relationship_code { get; set; }
            public string relationship_name { get; set; }
            public string[] Eligble { get; set; }
            public List<string> relationship_codes { get; set; } // Holds multiple codes
            public List<string> relationship_names { get; set; } // Holds multiple relationship names
            
            public string employeecode {  get; set; }   
        }

        public class EmployeeRelationship
        {
            public string nomination_id { get; set; }
            public string relationship_code { get; set; }
            public string relationship_name { get; set; }
            public string is_active { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string de_active_date { get; set; }
            public string active_date { get; set; }
            public string Empflag { get; set; }
        }
        public class EmpDetailsSearch
        {
            public List<string> Nmae { get; set; }
            public string search_text { get; set; }
            public string category_code { get; set; }
            public char status { get; }

        }
        public class CategoryEligibility
        {
            public string NominationCategoryCode { get; set; }
            public string RelationshipNames { get; set; }
            public string RelationshipCode { get; set; }
        }
        public class CategoryUpdateModel
        {
            public int NominationId { get; set; } // ID for the nomination
            public string NominationCategoryCode { get; set; } // Code for the nomination category
            public List<string> SelectedValues { get; set; } = new List<string>(); // List of selected values (initialized to avoid null reference)
            public bool IsActive { get; set; } // Indicates whether the category is active
            public List<string> RelationshipNames { get; set; } = new List<string>(); // List of relationship names (initialized to avoid null reference)
            public List<string> UnselectedValues { get; set; } = new List<string>();

            public string employeecode { get; set; }
        }

        public class Perc
        {
            public string Names { get; set; }
            public int FamilyId { get; set; }
            public int per { get; set; }

            public string relationship { get; set; }
        }

    }

}