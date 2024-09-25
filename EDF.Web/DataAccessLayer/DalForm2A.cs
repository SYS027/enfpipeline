using Dapper;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Wordprocessing;
using EDF.Web.Models.Form;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EDF.Web.DataAccessLayer
{
    public class DalForm2A
    {
        NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);


        #region
        public List<PropertyDetails_AutoFill> GetForm2_A_AutoFill(string employee_code)
        {
            ResponseModel res = new ResponseModel();
            List<PropertyDetails_AutoFill> result = new List<PropertyDetails_AutoFill>();

            try
            {
                con.Open();
                var cmd = new NpgsqlCommand();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("p_employee_code", employee_code);
                result = con.Query<PropertyDetails_AutoFill>("select * from fn_get_form2_a_property_details_auto_fill(:p_employee_code)", parameters).ToList();

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

            return result;
        }
        #endregion;



        #region Check eligibility Form2 A
        public List<EmployeeDetails> GetEmployeeElgPrd2A(string employeeid)
        {

            List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
            try
            {
                con.Open();
                var cmd = new NpgsqlCommand();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("employee_id", employeeid);

                employeeDetails = con.Query<EmployeeDetails>("select * from mdcl_fn_check_form2_a_eligibility(:employee_id)", parameters).ToList();

            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex);
            }
            finally
            {
                con.Close();
            }
            return employeeDetails;
        }


        #endregion


        #region Property Details
        public List<PropertyDetails> GetPropertyDetails(string employee_code)
        {
            ResponseModel res = new ResponseModel();
            List<PropertyDetails> result = new List<PropertyDetails>();

            {
                try
                {
                    con.Open();
                    var cmd = new NpgsqlCommand();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("p_employee_code", employee_code);
                    result = con.Query<PropertyDetails>("select * from fn_get_form2_a_employee_property_details(:p_employee_code)", parameters).ToList();

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


        #region Form 2 Autofill
        public EmployeeDetails GetEmployeeDetails(string employeeCode)
        {

            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {

                con.Open();
                var cmd = new NpgsqlCommand();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("employee_code", employeeCode);

                employeeDetails = con.QuerySingle<EmployeeDetails>("select * from fn_get_form_employee_details(:employee_code)", parameters);

            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex);
            }
            finally
            {
                con.Close();
                //con.Dispose();
            }
            return employeeDetails;
        }
        #endregion

        #region Form 2 Status Dropdown
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
                        cmd.Parameters.AddWithValue("@p_parent_key", parent_key);
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

                }
                finally
                {
                    con.Close();
                }
            }
            return res;
        }
        #endregion

        #region  Save Form2 A
        public ResponseModel AddFormtwoData(Form2AModel objModel)
        {
            ResponseModel objResponse = new ResponseModel();
            NpgsqlConnection con = new NpgsqlConnection(CommonHelper.GetConnectionString);
            NpgsqlTransaction transaction = null;

            try
            {
                int insertedId = 0;
                con.Open();
                transaction = con.BeginTransaction();

                using (var cmd = new NpgsqlCommand("fn_save_employee_form2_a_details", con, transaction))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_employeecode", string.IsNullOrWhiteSpace(objModel.employeecode) ? "" : objModel.employeecode);
                    cmd.Parameters.AddWithValue("p_not_applicable", objModel.not_applicable);
                    cmd.Parameters.AddWithValue("p_action_date", objModel.action_date);
                    insertedId = (int)cmd.ExecuteScalar();
                }
                if (objModel.not_applicable == false)
                {
                    using (var cmd2 = new NpgsqlCommand("fn_save_property_details", con, transaction))
                    {

                        cmd2.CommandType = CommandType.StoredProcedure;


                        foreach (var formTwoItem in objModel.formTwoModel)
                        {
                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("p_employeecode", string.IsNullOrWhiteSpace(objModel.employeecode) ? "" : objModel.employeecode);
                            cmd2.Parameters.AddWithValue("p_parent_id", insertedId);
                            cmd2.Parameters.AddWithValue("p_natureofproperty", formTwoItem.natureOfProperty != 0 ? formTwoItem.natureOfProperty : 0);
                            cmd2.Parameters.AddWithValue("p_presentmarketvalue", string.IsNullOrWhiteSpace(formTwoItem.presentMarketValue) ? "" : formTwoItem.presentMarketValue);
                            cmd2.Parameters.AddWithValue("p_propertyownedby", formTwoItem.propertyOwnedBy != 0 ? formTwoItem.propertyOwnedBy : 0);
                            cmd2.Parameters.AddWithValue("p_nameofspouse", string.IsNullOrWhiteSpace(formTwoItem.nameOfSpouse) ? "" : formTwoItem.nameOfSpouse);
                            cmd2.Parameters.AddWithValue("p_natureofacquisition", formTwoItem.natureOfAcuisition != 0 ? formTwoItem.natureOfAcuisition : 0);
                            cmd2.Parameters.AddWithValue("p_dateofacquisition", formTwoItem.dateOfAcuisition != null ? formTwoItem.dateOfAcuisition : null);
                            cmd2.Parameters.AddWithValue("p_country", string.IsNullOrWhiteSpace(formTwoItem.country) ? "" : formTwoItem.country);
                            cmd2.Parameters.AddWithValue("p_state", string.IsNullOrWhiteSpace(formTwoItem.state) ? "" : formTwoItem.state);
                            cmd2.Parameters.AddWithValue("p_city", string.IsNullOrWhiteSpace(formTwoItem.city) ? "" : formTwoItem.city);
                            cmd2.Parameters.AddWithValue("p_pincode", string.IsNullOrWhiteSpace(formTwoItem.pinCode) ? "" : formTwoItem.pinCode);
                            cmd2.Parameters.AddWithValue("p_streetaddress", string.IsNullOrWhiteSpace(formTwoItem.streetAddress) ? "" : formTwoItem.streetAddress);
                            cmd2.Parameters.AddWithValue("p_acquiredname", string.IsNullOrWhiteSpace(formTwoItem.acquiredName) ? "" : formTwoItem.acquiredName);
                            cmd2.Parameters.AddWithValue("p_acquiredaddress", string.IsNullOrWhiteSpace(formTwoItem.acquiredAddress) ? "" : formTwoItem.acquiredAddress);


                            cmd2.Parameters.AddWithValue("p_houseloan", string.IsNullOrWhiteSpace(formTwoItem.housingLoan) ? "" : formTwoItem.housingLoan);
                            cmd2.Parameters.AddWithValue("p_pfloan", string.IsNullOrWhiteSpace(formTwoItem.pfLoan) ? "" : formTwoItem.pfLoan);
                            cmd2.Parameters.AddWithValue("p_saving", string.IsNullOrWhiteSpace(formTwoItem.saving) ? "" : formTwoItem.saving);
                            cmd2.Parameters.AddWithValue("p_others", string.IsNullOrWhiteSpace(formTwoItem.others) ? "" : formTwoItem.others);
                            cmd2.Parameters.AddWithValue("p_total", string.IsNullOrWhiteSpace(formTwoItem.total) ? "" : formTwoItem.total);
                            cmd2.Parameters.AddWithValue("p_annualincome", string.IsNullOrWhiteSpace(formTwoItem.annualIncomeFromProperty) ? "" : formTwoItem.annualIncomeFromProperty);
                            cmd2.Parameters.AddWithValue("p_remarks", string.IsNullOrWhiteSpace(formTwoItem.remarks) ? "" : formTwoItem.remarks);

                            var resultIdParameter = new NpgsqlParameter("p_rows_affected", NpgsqlDbType.Integer);
                            resultIdParameter.Direction = ParameterDirection.Output;
                            cmd2.Parameters.Add(resultIdParameter);

                            cmd2.ExecuteNonQuery();
                            int rowsAffected = Convert.ToInt32(resultIdParameter.Value);

                            if (rowsAffected > 0)
                            {
                                objResponse.Status = true;
                                objResponse.Message = "Employee Property details Added successfully.";
                            }
                            else
                            {
                                objResponse.Status = false;
                                objResponse.Message = "Failed to insert employee Property details.";
                                break;
                            }
                        }

                    }
                    if (objModel.soldPropertyDetails.Count > 0)
                    {
                        using (var cmd3 = new NpgsqlCommand("fn_save_sold_property_details", con, transaction))
                        {
                            cmd3.CommandType = CommandType.StoredProcedure;

                            foreach (var soldProperty in objModel.soldPropertyDetails)
                            {
                                cmd3.Parameters.Clear();
                                cmd3.Parameters.AddWithValue("p_property_id", soldProperty.property_id);
                                cmd3.Parameters.AddWithValue("p_employeecode", string.IsNullOrWhiteSpace(soldProperty.employeecode) ? "" : soldProperty.employeecode);
                                cmd3.Parameters.AddWithValue("p_sold_to", string.IsNullOrWhiteSpace(soldProperty.sold_to) ? "" : soldProperty.sold_to);
                                cmd3.Parameters.AddWithValue("p_sold_amt", string.IsNullOrWhiteSpace(soldProperty.sold_amt) ? "" : soldProperty.sold_amt);
                                cmd3.Parameters.AddWithValue("p_sold_date", soldProperty.sold_date != null ? soldProperty.sold_date : null);
                                cmd3.Parameters.AddWithValue("p_period", objModel.action_date != null ? objModel.action_date : null);
                                cmd3.ExecuteNonQuery();
                            }
                        }
                    }
                }

                transaction.Commit();
                objResponse.Status = true;
                objResponse.Message = "Data added successfully.";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                objResponse.Status = false;
                objResponse.Message = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return objResponse;
        }
        #endregion
    }
}