using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDF.Web.Models
{
    public class CustomModel
    {
    }
    //public class LoginViewModel
    //{
    //    [Required]
    //    [Display(Name = "UserID")]
    //    [EmailAddress]
    //    public string UserID { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //}


    public class LoginViewModel
    {
        public string EmpCode { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class MenuHeaderModel
    {
        public string parentmenu_name { get; set; }
        public int id { get; set; }
        public List<MenusModel> child_menu_list { get; set; }
    }

    public class MenusModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public int parent_id { get; set; }
        public bool select_status { get; set; }
        public long menu_order { get; set; }
        public Boolean is_parent { get; set; }
        public int role_id { get; set; }
    }
    public class EmailModel
    {
        public int id { get; set; }
        public string attached_file_location { get; set; }
        public string attached_file { get; set; }
        public List<string> to_emailids { get; set; }
        public List<string> cc_emailids { get; set; }
        public List<string> bcc_emailids { get; set; }
        public List<string> attached_files { get; set; }
        public string file_name { get; set; }
        public string email_subject { get; set; }
        public string email_body { get; set; }
        public string module_code { get; set; }
        public byte[] eml_attachemet { get; set; }
        public string attachment_name { get; set; }
        //public string yearfrom { get; set; }
        //public string yearto { get; set; }

    }
}