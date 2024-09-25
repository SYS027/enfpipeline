using Dapper;
using DocumentFormat.OpenXml.Presentation;
using EDF.Web.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace EDF.Web.DataAccessLayer
{
    public class RelationDetails
    {
        public string employeecode { get; set; }
        public string employeename { get; set; }
        public string quarter { get; set; }
        public string relationshipEmployee { get; set; }
        public string relativeName { get; set; }
        public string natureOfEngagement { get; set; }
        public string firmsAssociated { get; set; }
        public string actionDate { get; set; }
        public string mutualfundName { get; set; }
        public string relativeDesignation { get; set; }
        public string placeOfPosting { get; set; }
        public string submission_period { get; set; }
        public string disp_period_flag { get; set; }
    }
    public class DropdownModel
    {
        public int dropdown_key { get; set; }
        public string dropdown_name { get; set; }
        public string attribute_type_unit_desc { get; set; }


    }
    public class EmployeeDetails
    {
        //public EmployeeDetails()
        //{

        //    RelationDetails = new List<FormFiveModel>();
        //}

        public string period_flag { get; set; }
        public List<FormFiveModel> RelationDetails { get; set; }
        public int employeeid { get; set; }
        public string elg_period { get; set; }
        public string elg_flag { get; set; }
        public string employeeCode { get; set; }
        public DateTime? action_date { get; set; }
        public DateTime? action_date_1 { get; set; }
        public bool not_applicable { get; set; }
        public string DOA { get; set; }
        public string employeename { get; set; }

        public string role { get; set; }

        public string groups { get; set; }

        public string band { get; set; }

        public string designation { get; set; }

        public DateTime declarationDate { get; set; }
        public string location { get; set; }
        public string as_on_date { get; set; }
    }
    public class FormFiveModel
    {

        public int Id { get; set; }
        public string elg_period { get; set; }
        public int employeeId { get; set; }
        public int employeecode { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public DateTime? actionDate { get; set; }
        public int status { get; set; }
        public int relationshipEmployee { get; set; }
        public string nameOfRelative { get; set; }
        public int natureEngagement { get; set; }
        public string institutionFirm { get; set; }
        public string namesOfMutualFund { get; set; }
        public string designation { get; set; }
        public string placeOfPosting { get; set; }
        public bool isSameCompany { get; set; }

    }

    public class RelationDetails_AutoFill
    {
        public string employeecode { get; set; }
        public string employeename { get; set; }
        public string quarter { get; set; }
        public int relationshipEmployee { get; set; }
        public string relativeName { get; set; }
        public int natureOfEngagement { get; set; }
        public string firmsAssociated { get; set; }
        public string mutualfundName { get; set; }
        public string relativeDesignation { get; set; }
        public string placeOfPosting { get; set; }
        public string actionDate { get; set; }
    }


    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int ID { get; set; }
    }

    internal class DalForm5
    {
        NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);

        #region Auto Populate of Form5
        public List<RelationDetails_AutoFill> GetRelativeList_AutoFill(string employee_code)
        {
            ResponseModel res = new ResponseModel();
            List<RelationDetails_AutoFill> result = new List<RelationDetails_AutoFill>();

            {
                try
                {
                    con.Open();
                    var cmd = new NpgsqlCommand();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", employee_code);
                    result = con.Query<RelationDetails_AutoFill>("select * from fn_get_form5_employee_relative_details_auto_fill(:p_employee_code)", parameters).ToList();

                }
                catch (Exception ex)
                {
                    res.Status = false;
                    res.Message = "Error!!!";
                    ExceptionLogging.LogException(ex);
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        #endregion;


        #region Relative Details

        public List<RelationDetails> GetRelativeList(string employee_code)
        {
            ResponseModel res = new ResponseModel();
            List<RelationDetails> result = new List<RelationDetails>();

            {
                try
                {
                    con.Open();
                    var cmd = new NpgsqlCommand();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", employee_code);
                    result = con.Query<RelationDetails>("select * from fn_get_form5_employee_relative_details(:p_employee_code)", parameters).ToList();

                }
                catch (Exception ex)
                {
                    res.Status = false;
                    res.Message = "Error!!!";
                    ExceptionLogging.LogException(ex);
                }
                finally
                {
                    con.Close();
                }
            }

            return result;
        }

        #endregion
        #region Form 5 
        public EmployeeDetails GetEmployeeDetails(string employeeid)
        {

            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                //using (NpgsqlConnection con = new NpgsqlConnection("host=103.228.83.115;port=5432; Username=postgres; Password=sqlpass; Database=Tour_Management"))

                con.Open();
                var cmd = new NpgsqlCommand();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("employee_id", employeeid);

                employeeDetails = con.QuerySingle<EmployeeDetails>("select * from fn_get_form_employee_details(:employee_id)", parameters);

            }
            catch (Exception ex)
            {
                CommonHelper.write_log("error in GetEmployeeDetails() :" + ex.Message);
                throw ex;
            }
            finally
            {
                con.Close();
                //con.Dispose();
            }
            return employeeDetails;
        }
        #endregion

        public List<EmployeeDetails> GetEmployeeElgPrd(string employeeid)
        {

            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            try
            {
                con.Open();
                var cmd = new NpgsqlCommand();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("employee_id", employeeid);

                employeeDetails = con.Query<EmployeeDetails>("select * from mdcl_fn_check_form5_eligibility(:employee_id)", parameters).ToList();

            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return employeeDetails;
        }

        #region Form 5 Save Method
        public ResponseModel AddRelative(EmployeeDetails objModel)
        {
            ResponseModel objResponse = new ResponseModel();
            NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);
            NpgsqlTransaction transaction = null;

            try
            {
                int insertedId = 0;
                con.Open();
                transaction = con.BeginTransaction();




                using (var cmd1 = new NpgsqlCommand("fn_save_mdcl_form5_emp_details", con, transaction))
                {

                    cmd1.CommandType = CommandType.StoredProcedure;

                    cmd1.Parameters.AddWithValue("p_employee_code", string.IsNullOrWhiteSpace(objModel.employeeCode) ? "" : objModel.employeeCode);
                    cmd1.Parameters.AddWithValue("p_elg_period", string.IsNullOrWhiteSpace(objModel.elg_period) ? "" : objModel.elg_period);
                    //cmd1.Parameters.AddWithValue("p_period_flag", string.IsNullOrWhiteSpace(objModel.period_flag) ? "" : objModel.period_flag);
                    cmd1.Parameters.AddWithValue("p_action_date", objModel.action_date != null ? objModel.action_date : DateTime.Now);
                    cmd1.Parameters.AddWithValue("p_not_applicable", objModel.not_applicable);

                    try
                    {
                        insertedId = (int)cmd1.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        CommonHelper.write_log("error in saving fn_save_mdcl_form5_emp_details() :" + ex.Message);
                    }
                }
                if (objModel.not_applicable == false)
                {
                    using (var cmd2 = new NpgsqlCommand("insert_employee_relationship", con, transaction))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;


                        foreach (var formFiveItem in objModel.RelationDetails)
                        {
                            if (formFiveItem.nameOfRelative != null && formFiveItem.placeOfPosting != null && formFiveItem.natureEngagement != 0 && formFiveItem.relationshipEmployee != 0)
                            {
                                cmd2.Parameters.Clear();

                                cmd2.Parameters.AddWithValue("p_parent_id", insertedId);
                                cmd2.Parameters.AddWithValue("p_employee_code", string.IsNullOrWhiteSpace(objModel.employeeCode) ? "" : objModel.employeeCode);
                                cmd2.Parameters.AddWithValue("p_relationship_with_employee", formFiveItem.relationshipEmployee != 0 ? formFiveItem.relationshipEmployee : 0);
                                cmd2.Parameters.AddWithValue("p_name_of_relative", string.IsNullOrWhiteSpace(formFiveItem.nameOfRelative) ? "" : formFiveItem.nameOfRelative);
                                cmd2.Parameters.AddWithValue("p_nature_of_engagement", formFiveItem.natureEngagement != 0 ? formFiveItem.natureEngagement : 0);
                                cmd2.Parameters.AddWithValue("p_firm_relative_associated", string.IsNullOrWhiteSpace(formFiveItem.institutionFirm) ? "" : formFiveItem.institutionFirm);
                                cmd2.Parameters.AddWithValue("p_names_of_mutual_fund", string.IsNullOrWhiteSpace(formFiveItem.namesOfMutualFund) ? "" : formFiveItem.namesOfMutualFund);
                                cmd2.Parameters.AddWithValue("p_designation", string.IsNullOrWhiteSpace(formFiveItem.designation) ? "" : formFiveItem.designation);
                                cmd2.Parameters.AddWithValue("p_place_of_posting", string.IsNullOrWhiteSpace(formFiveItem.placeOfPosting) ? "" : formFiveItem.placeOfPosting);

                                NpgsqlParameter rowsAffectedParam = new NpgsqlParameter("p_rows_affected", NpgsqlDbType.Integer);
                                rowsAffectedParam.Direction = ParameterDirection.Output;
                                cmd2.Parameters.Add(rowsAffectedParam);


                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("error in function insert_employee_relationship()" + ex.Message);
                                }
                                int intRetVal = Convert.ToInt32(rowsAffectedParam.Value);



                                if (intRetVal > 0)
                                {
                                    objResponse.Status = true;
                                    objResponse.Message = "Employee relationship inserted successfully.";
                                }
                                else
                                {
                                    objResponse.Status = false;
                                    objResponse.Message = "Failed to insert employee relationship.";
                                    transaction.Rollback();
                                    break;
                                }
                            }
                        }
                    }
                }
                transaction.Commit();
                objResponse.Status = true;
                objResponse.Message = "Employee relationships inserted successfully.";


            }
            catch (Exception ex)
            {
                objResponse.Status = false;
                objResponse.Message = "Failed to insert employee relationship";

                if (transaction != null)
                    transaction.Rollback();
                ExceptionLogging.LogException(ex);
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
                con.Close();
            }

            return objResponse;
        }
        #endregion

        //#region Form 5 Status Dropdown
        //public List<DropdownModel> GetFormStatusList()

        //{

        //    List<DropdownModel> res = new List<DropdownModel>();

        //    using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))

        //    {

        //        con.Open();

        //        try
        //        {

        //            using (NpgsqlCommand cmd = new NpgsqlCommand("get_dropdown_items", con))

        //            {

        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@p_parent_key", 1);

        //                NpgsqlDataReader reader = cmd.ExecuteReader();

        //                if (reader.HasRows)

        //                {

        //                    while (reader.Read())

        //                    {

        //                        DropdownModel u = new DropdownModel();

        //                        u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);

        //                        u.dropdown_name = reader["dropdown_name"] is DBNull ? string.Empty : reader["dropdown_name"].ToString();

        //                        res.Add(u);

        //                    }

        //                }

        //            }

        //        }

        //        catch (Exception ex)

        //        {

        //            Console.WriteLine(ex);

        //            // HelperClass.WriteLog("Error occurred while fetching data from the database: " + ex);

        //        }

        //        finally

        //        {

        //            con.Close();

        //        }

        //    }

        //    return res;

        //}
        //#endregion

        //#region Form 5 Relationship with Employee Dropdown
        //public List<DropdownModel> GetRelationshipList()

        //{

        //    List<DropdownModel> res = new List<DropdownModel>();

        //    using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))

        //    {

        //        con.Open();

        //        try

        //        {

        //            using (NpgsqlCommand cmd = new NpgsqlCommand("get_dropdown_items", con))

        //            {

        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@p_parent_key", 22);

        //                NpgsqlDataReader reader = cmd.ExecuteReader();

        //                if (reader.HasRows)

        //                {

        //                    while (reader.Read())

        //                    {

        //                        DropdownModel u = new DropdownModel();

        //                        u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);

        //                        u.dropdown_name = reader["dropdown_name"] is DBNull ? string.Empty : reader["dropdown_name"].ToString();

        //                        res.Add(u);

        //                    }

        //                }

        //            }

        //        }

        //        catch (Exception ex)

        //        {

        //            Console.WriteLine(ex);

        //            // HelperClass.WriteLog("Error occurred while fetching data from the database: " + ex);

        //        }

        //        finally

        //        {

        //            con.Close();

        //        }

        //    }

        //    return res;

        //}
        //#endregion

        //#region Form 5 Nature Of Engagement Dropdown
        //public List<DropdownModel> GetNatureList()

        //{

        //    List<DropdownModel> res = new List<DropdownModel>();

        //    using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))

        //    {

        //        con.Open();

        //        try

        //        {

        //            using (NpgsqlCommand cmd = new NpgsqlCommand("get_dropdown_items", con))

        //            {

        //                cmd.CommandType = CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@p_parent_key", 31);

        //                NpgsqlDataReader reader = cmd.ExecuteReader();

        //                if (reader.HasRows)

        //                {

        //                    while (reader.Read())

        //                    {

        //                        DropdownModel u = new DropdownModel();

        //                        u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);

        //                        u.dropdown_name = reader["dropdown_name"] is DBNull ? string.Empty : reader["dropdown_name"].ToString();

        //                        res.Add(u);

        //                    }

        //                }

        //            }

        //        }

        //        catch (Exception ex)

        //        {

        //            Console.WriteLine(ex);

        //            // HelperClass.WriteLog("Error occurred while fetching data from the database: " + ex);

        //        }

        //        finally

        //        {

        //            con.Close();

        //        }

        //    }

        //    return res;

        //}
        //#endregion

        public List<DropdownModel> GetFormDropdownList(int parent_key)
        {
            List<DropdownModel> res = new List<DropdownModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("get_dropdown_items", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_parent_key", parent_key);//1
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DropdownModel u = new DropdownModel();
                                u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                u.dropdown_name = reader["dropdown_name"] is DBNull ? string.Empty : reader["dropdown_name"].ToString();
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonHelper.write_log("error in GetFormDropdownList() :" + ex.Message);
                    ExceptionLogging.LogException(ex);
                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
    }


    public class ExceptionLogging
    {
        public static void LogException(Exception ex)
        {
            CommonHelper.write_log("error :" + ex.Message);
        }
        public static void LogException(string msg, Exception ex)
        {
            CommonHelper.write_log(msg + " |" + ex.Message);
        }
    }

    public class CommonHelper
    {
        public static string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["mycon"].ToString();
            }
        }

        public static string GetConnectionStringENF
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["myconn"].ToString();
            }
        }

        public static void write_log(string message)
        {
            try
            {
                string ErrorLogDir = ConfigurationManager.AppSettings["Error_log"].ToString();
                if (!Directory.Exists(ErrorLogDir))
                    Directory.CreateDirectory(ErrorLogDir);

                ErrorLogDir += "\\edf_error_log" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";

                using (StreamWriter sw = new StreamWriter(ErrorLogDir, true))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "\t" + message);
                }
            }
            catch (Exception exx)
            {


            }
        }
    }

}