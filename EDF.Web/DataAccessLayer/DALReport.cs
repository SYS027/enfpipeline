using Dapper;
using EDF.Web.Models.Form;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EDF.Web.DataAccessLayer
{
    public class DALReport
    {
        NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);


        #region Attribute Dropdown
        public List<DropdownModel> GetFormDropdownList(string parent_key)
        {
            List<DropdownModel> res = new List<DropdownModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_attribute_units", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DropdownModel u = new DropdownModel();
                                //  u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                u.attribute_type_unit_desc = reader["attribute_type_unit_desc"] is DBNull ? string.Empty : reader["attribute_type_unit_desc"].ToString();
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Employee Name for Form5 Dropdown
        public List<RelationDetails> GetEmpNameDropdown()
        {
            List<RelationDetails> res = new List<RelationDetails>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_employee_name", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RelationDetails u = new RelationDetails();
                                //  u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                u.employeecode = reader["employee_code"] is DBNull ? string.Empty : reader["employee_code"].ToString();
                                u.employeename = reader["employeename"] is DBNull ? string.Empty : reader["employeename"].ToString();
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Applicable Dropdown
        public List<EmployeeDetails> GetApplicableDropdown()
        {
            List<EmployeeDetails> res = new List<EmployeeDetails>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_ApplicableDropdown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //   cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                EmployeeDetails u = new EmployeeDetails();
                                //  u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                //  u.not_applicable = reader["employee_code"] is DBNull ? string.Empty : reader["employee_code"].ToString();
                                //  u.employeename = reader["employeename"] is DBNull ? string.Empty : reader["employeename"].ToString();
                                //  u.not_applicable = reader["employee_code"] == DBNull.Value ? string.Empty : reader["employee_code"].ToString();
                                //  u.not_applicable = reader["not_applicable"] is DBNull ? false : true;
                                u.not_applicable = reader["not_applicable"] != DBNull.Value && (bool)reader["not_applicable"];

                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Period Flag for form 5 Dropdown
        public List<EmployeeDetails> GetPeriodFlagDropdownfor5()
        {
            List<EmployeeDetails> emp = new List<EmployeeDetails>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_periodflagdropdownfor5", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //   cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                EmployeeDetails u = new EmployeeDetails();
                                u.period_flag = reader["period_flag"] is DBNull ? string.Empty : reader["period_flag"].ToString();
                                //u.period_flag = reader["not_applicable"] != DBNull.Value && (bool)reader["not_applicable"];
                                emp.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return emp;
        }
        #endregion

        #region Form 5 Report

        public List<RelationDetails> GetFormFiveReport(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {
            List<RelationDetails> lstRelativeDetails = new List<RelationDetails>();

            int search_not_applicable;

            if (is_search_not_applicable == null)
            {
                search_not_applicable = -1;
            }
            else if (Convert.ToBoolean(is_search_not_applicable))
            {
                search_not_applicable = 1;
            }
            else
            {
                search_not_applicable = 0;
            }

            // SqlConnection con = new SqlConnection(CommonHelper.GetConnectionString);
            using (var con = new NpgsqlConnection(CommonHelper.GetConnectionString))
                try
                {

                    con.Open();
                    var parameters = new DynamicParameters();

                    parameters.Add("@employeeCodeSearch", employeeCodeSearch);
                    // parameters.Add("@employeeNameSearch", employeeNameSearch);
                    parameters.Add("@from_date", from_date);
                    parameters.Add("@to_date", to_date);
                    parameters.Add("@search_not_applicable", search_not_applicable);
                    parameters.Add("@search_period_flag", search_period_flag);
                    parameters.Add("@search_band", search_band);
                    parameters.Add("@departmentSearch", departmentSearch);
                    parameters.Add("@designationSearch", designationSearch);

                    lstRelativeDetails = con.Query<RelationDetails>("SELECT * FROM get_form5_report_details(@employeeCodeSearch,@from_date, @to_date, @search_not_applicable, @search_period_flag, @search_band, @departmentSearch, @designationSearch)", parameters).ToList();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.LogException(ex);
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            return lstRelativeDetails;
            // return LoginData/*.OrderByDescending(x => x.ID).ToList()*/;
        }
        #endregion



        #region Employee Name for Form2A Dropdown
        public List<Form2AModel> GetEmpNamefor2ADropdown()
        {
            List<Form2AModel> res = new List<Form2AModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_employee_name_for2a", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Form2AModel u = new Form2AModel();
                                //  u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                u.employeecode = reader["employeecode"] is DBNull ? string.Empty : reader["employeecode"].ToString();
                                u.employeename = reader["employeename"] is DBNull ? string.Empty : reader["employeename"].ToString();
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Applicable Dropdown
        public List<Form2AModel> GetApplicableDropdownfor2A()
        {
            List<Form2AModel> res = new List<Form2AModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_applicabledropdownfor2a", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //   cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Form2AModel u = new Form2AModel();
                                u.not_applicable = reader["not_applicable"] != DBNull.Value && (bool)reader["not_applicable"];
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Period Flag Dropdown
        public List<Form2AModel> GetPeriodFlagDropdownfor2A()
        {
            List<Form2AModel> res = new List<Form2AModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("fn_get_periodflagdropdownfor2a", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //   cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Form2AModel u = new Form2AModel();
                                u.period_flag = reader["period_flag"] is DBNull ? string.Empty : reader["period_flag"].ToString();
                                //u.period_flag = reader["not_applicable"] != DBNull.Value && (bool)reader["not_applicable"];
                                res.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region Form 2A Report

        public List<PropertyDetails> GetFormTwoReport(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, bool? is_search_not_applicable, string search_period_flag, string search_band, string departmentSearch, string designationSearch)
        {
            List<PropertyDetails> lstpropertyDetails = new List<PropertyDetails>();
            int search_not_applicable;

            if (is_search_not_applicable == null)
            {
                search_not_applicable = -1;
            }
            else if (Convert.ToBoolean(is_search_not_applicable))
            {
                search_not_applicable = 1;
            }
            else
            {
                search_not_applicable = 0;
            }
            using (var con = new NpgsqlConnection(CommonHelper.GetConnectionString))
                try
                {

                    con.Open();
                    var parameters = new DynamicParameters();

                    parameters.Add("@employeeCodeSearch", employeeCodeSearch);
                    parameters.Add("@from_date", from_date);
                    parameters.Add("@to_date", to_date);
                    parameters.Add("@search_not_applicable", search_not_applicable);
                    parameters.Add("@search_period_flag", search_period_flag);
                    parameters.Add("@search_band", search_band);
                    parameters.Add("@designationsearch", designationSearch);
                    parameters.Add("@departmentsearch", departmentSearch);

                    lstpropertyDetails = con.Query<PropertyDetails>("SELECT * FROM get_form2A_report_details(@employeeCodeSearch,@from_date,@to_date,@search_not_applicable,@search_period_flag,@search_band,@designationsearch,@departmentsearch)", parameters).ToList();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.LogException(ex);
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            return lstpropertyDetails;
            // return LoginData/*.OrderByDescending(x => x.ID).ToList()*/;
        }
        #endregion



    }
}