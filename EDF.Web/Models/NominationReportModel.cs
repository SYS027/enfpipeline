using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDF.Web.Models
{
    public class NominationReportModel
    {
        public int family_id {  get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int employee_id {  get; set; }
        public string employeecode {  get; set; }
        public string employeename {  get; set; }
        public string nomination_category_code {  get; set; }
        public string nomination_category_name {  get; set; }
        public string nomination_type {  get; set; }
        public string relationship_code {  get; set; }
        public string relationship_name {  get; set; }
        public double share_of_percentage {  get; set; }
        public double old_share_of_percentage {  get; set; }
        public double new_share_of_percentage {  get; set; }



    }
}