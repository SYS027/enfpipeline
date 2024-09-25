using Dapper;
using EDF.Web.Models;
using ENF_NEW.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EDF.Web.DataAccessLayer
{
    public class DALNominationReport
    {
        static string conString = CommonHelper.GetConnectionStringENF;

        ResponseModelENF res = new ResponseModelENF();

        public List<NominationReportModel> GetEmpNameDropdown()
        {
            List<NominationReportModel> res = new List<NominationReportModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("enf_fn_get_employee_name", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                NominationReportModel u = new NominationReportModel();
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

        public List<NominationReportModel> GetNominationCategoryDropdown()
        {
            List<NominationReportModel> res = new List<NominationReportModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();

                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("enf_fn_get_nomination_category", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@p_attribute_type", parent_key);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                NominationReportModel u = new NominationReportModel();
                                //  u.dropdown_key = reader["dropdown_key"] is DBNull ? 0 : Convert.ToInt32(reader["dropdown_key"]);
                                u.nomination_category_code = reader["nomination_category_code"] is DBNull ? string.Empty : reader["nomination_category_code"].ToString();
                                u.nomination_category_name = reader["nomination_category_name"] is DBNull ? string.Empty : reader["nomination_category_name"].ToString();
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

        public List<NominationReportModel> GetNominationReport(string employeeCodeSearch, DateTime? from_date, DateTime? to_date, string nominationTypeSearch, string familyMemberType)
        {
            List<NominationReportModel> nominationReports = new List<NominationReportModel>();

            using (var con = new NpgsqlConnection(conString))
                try
                {
                    con.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_from_date", from_date);
                    parameters.Add("@p_to_date", to_date);                   
                    parameters.Add("@p_employeecode", employeeCodeSearch);
                    parameters.Add("@p_nomination_category_code", nominationTypeSearch);
                    parameters.Add("@p_familyMemberType", familyMemberType);

                  
                    nominationReports = con.Query<NominationReportModel>("SELECT * FROM enf_fn_get_nomination_report(@p_from_date, @p_to_date, @p_employeecode, @p_nomination_category_code, @p_familyMemberType)", parameters).ToList();
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    CommonHelper.write_log($"Error retrieving Nomination Report : {ex.Message}");
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            return nominationReports;

        }

        public List<NominationReportModel> GetAuditReport()
        {
            List<NominationReportModel> nominationauditReports = new List<NominationReportModel>();

            using (var con = new NpgsqlConnection(conString))
                try
                {
                    con.Open();
                    var parameters = new DynamicParameters();
                    //parameters.Add("@p_from_date", from_date);
                    //parameters.Add("@p_to_date", to_date);
                    //parameters.Add("@p_employeecode", employeeCodeSearch);
                    //parameters.Add("@p_nomination_category_code", nominationTypeSearch);
                    //parameters.Add("@p_familyMemberType", familyMemberType);


                    nominationauditReports = con.Query<NominationReportModel>("SELECT * FROM enf_fn_get_audit_report()", parameters).ToList();
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    CommonHelper.write_log($"Error retrieving Nomination Audit Report : {ex.Message}");
                    // throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            return nominationauditReports;

        }

    }
}