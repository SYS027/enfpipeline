using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using Dapper;
using EDF.Web.Models;
using Npgsql;

namespace EDF.Web.DataAccessLayer
{
    public class DalLogin
    {
        NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);

        //public LoginModelDetails GetLoginEmpDetails(string employeeCode)
        //{

        //    LoginModelDetails employeeDetails = new LoginModelDetails();
        //    try
        //    {


        //        con.Open();
        //        var cmd = new NpgsqlCommand();
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("employee_code", employeeCode);

        //        employeeDetails = con.QuerySingle<LoginModelDetails>("select * from fn_get_login_employee_details(:employee_code)", parameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLogging.LogException(ex);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }
        //    return employeeDetails;
        //}


        public LoginDetailModel ValidateUser(LoginViewModel objModel)
        {
            LoginDetailModel UserDetail = new LoginDetailModel();
            NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);
            DataTable dt = new DataTable();
            // bool status = true;
            bool ad_status = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AdStatus"));
            try
            {

                using (con = new NpgsqlConnection(CommonHelper.GetConnectionString))
                {

                    con.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from  mdcl_sp_get_validateemp(:pemplid)", con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("pemplid", objModel.EmpCode);

                        using (NpgsqlDataAdapter SqDA = new NpgsqlDataAdapter(cmd))
                        {
                            SqDA.Fill(dt);
                        }
                    }

                    if (dt.Rows.Count == 0)
                    {
                        UserDetail = new LoginDetailModel();
                    }
                    else
                    {
                        foreach (DataRow drow in dt.Rows)
                        {

                            //UserDetail.UserID = string.IsNullOrWhiteSpace(drow["employeeid"].ToString()) ? 0 : Convert.ToInt32(drow["employeeid"].ToString());
                            UserDetail.employeeid = string.IsNullOrWhiteSpace(drow["employeeid"].ToString()) ? 0 : Convert.ToInt32(drow["employeeid"].ToString());
                            UserDetail.UserName = string.IsNullOrWhiteSpace(drow["employeename"].ToString()) ? "" : drow["employeename"].ToString();
                            UserDetail.EmailID = string.IsNullOrWhiteSpace(drow["email"].ToString()) ? "" : drow["email"].ToString();
                            //UserDetail.ApplnNo = string.IsNullOrWhiteSpace(drow["name"].ToString()) ? "" : drow["name"].ToString();
                            //UserDetail.Band = string.IsNullOrWhiteSpace(drow["name"].ToString()) ? "" : drow["name"].ToString();
                            //UserDetail.Department = string.IsNullOrWhiteSpace(drow["name"].ToString()) ? "" : drow["name"].ToString();
                            //UserDetail.Location = string.IsNullOrWhiteSpace(drow["name"].ToString()) ? "" : drow["name"].ToString();
                            //UserDetail.Designation = string.IsNullOrWhiteSpace(drow["name"].ToString()) ? "" : drow["name"].ToString();
                            UserDetail.Employeecode = string.IsNullOrWhiteSpace(drow["employeecode"].ToString()) ? "" : drow["employeecode"].ToString();
                            UserDetail.Salutation = string.IsNullOrWhiteSpace(drow["salutation"].ToString()) ? "" : drow["salutation"].ToString();
                            UserDetail.Mobile = string.IsNullOrWhiteSpace(drow["mobile"].ToString()) ? "" : drow["mobile"].ToString();
                            UserDetail.DateofBirth = string.IsNullOrWhiteSpace(drow["dateofbirth"].ToString()) ? "" : drow["dateofbirth"].ToString();
                            UserDetail.DateofJoin = string.IsNullOrWhiteSpace(drow["dateofjoining"].ToString()) ? "" : drow["dateofjoining"].ToString();
                            UserDetail.Employeestatus = string.IsNullOrWhiteSpace(drow["employeestatus"].ToString()) ? "" : drow["employeestatus"].ToString();
                            UserDetail.ReportingMname = string.IsNullOrWhiteSpace(drow["reportingmanagername"].ToString()) ? "" : drow["reportingmanagername"].ToString();
                            UserDetail.ReportingMcode = string.IsNullOrWhiteSpace(drow["reportingmanagercode"].ToString()) ? "" : drow["reportingmanagercode"].ToString();
                            UserDetail.DateofResignation = string.IsNullOrWhiteSpace(drow["dateofresignation"].ToString()) ? "" : drow["dateofresignation"].ToString();
                            UserDetail.Employementtype = string.IsNullOrWhiteSpace(drow["employmenttype"].ToString()) ? "" : drow["employmenttype"].ToString();
                            UserDetail.Nationality = string.IsNullOrWhiteSpace(drow["nationality"].ToString()) ? "" : drow["nationality"].ToString();
                            UserDetail.MaritalStatus = string.IsNullOrWhiteSpace(drow["maritalstatus"].ToString()) ? "" : drow["maritalstatus"].ToString();
                            UserDetail.UserName = UserDetail.Salutation + UserDetail.UserName;
                            UserDetail.is_full_access = string.IsNullOrWhiteSpace(drow["is_full_access"].ToString()) ? false : Convert.ToBoolean(drow["is_full_access"]);
                            UserDetail.gender = string.IsNullOrWhiteSpace(drow["gender"].ToString()) ? "" : drow["gender"].ToString();
                            UserDetail.is_operator = string.IsNullOrWhiteSpace(drow["is_operator"].ToString()) ? false : Convert.ToBoolean(drow["is_operator"]);
                            UserDetail.role_code = string.IsNullOrWhiteSpace(drow["role_code"].ToString()) ? "" : drow["role_code"].ToString();
                            UserDetail.Band = string.IsNullOrWhiteSpace(drow["band"].ToString()) ? "" : drow["band"].ToString();
                            //UserDetail.UserID = 1001;
                            //UserDetail.CompanyID = 1001;
                            //UserDetail.UserName = "Setti Sir";

                            //if (UserDetail.gender == "Female")
                            //{
                            DateTime date_of_joining = DateTime.ParseExact(UserDetail.DateofJoin, "dd MMM yyyy", CultureInfo.InvariantCulture);
                            if (date_of_joining.Year > 2003)
                            {
                                UserDetail.is_maternity = true;
                            }
                            //}
                        }
                      
                    }

                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log("error :ValidateUser()" + ex.Message);
                ExceptionLogging.LogException(ex);
                UserDetail = null;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return UserDetail;
        }

    }
}