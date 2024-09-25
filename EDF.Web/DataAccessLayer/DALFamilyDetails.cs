using EDF.Web.DataAccessLayer;
using ENF_NEW.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using static ENF_NEW.Models.FamilyDetails;

namespace ENF_NEW.Data_Access_Layer.DAL
{
    public class DALFamilyDetails
    {
        // static string con = ConfigurationManager.ConnectionStrings["mycon"].ToString();
        //static string conString = ConfigurationManager.ConnectionStrings["myconn"].ToString();     
        static string conString = CommonHelper.GetConnectionStringENF;

        ResponseModelENF res = new ResponseModelENF();

        public List<ReasonList> GetDeactiveReason()
        {
            List<ReasonList> reasonlst = new List<ReasonList>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select reason_id, reason_name from tbl_family_deactive_reason_master", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReasonList u = new ReasonList();
                                    u.reason_id = string.IsNullOrEmpty(reader["reason_id"].ToString()) ? 0 : Convert.ToInt32(reader["reason_id"]);
                                    u.reason_name = string.IsNullOrWhiteSpace(reader["reason_name"].ToString()) ? "" : reader["reason_name"].ToString();
                                    reasonlst.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting Deactive Reason type List is:" + ex);
                }
            }
            return reasonlst;
        }

        public List<Relationtype> GetRelationtype()
        {
            List<Relationtype> relation = new List<Relationtype>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select relationship_code, relationship_name from tbl_family_relationship_master", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Relationtype u = new Relationtype();
                                    u.relationship_code = string.IsNullOrWhiteSpace(reader["relationship_code"].ToString()) ? "" : reader["relationship_code"].ToString();
                                    u.relationship_name = string.IsNullOrWhiteSpace(reader["relationship_name"].ToString()) ? "" : reader["relationship_name"].ToString();
                                    //u.relationship_name = string.IsNullOrEmpty(reader["country_id"].ToString()) ? 0 : Convert.ToInt32(reader["country_id"]);
                                    relation.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting Relationship type List is:" + ex);
                }
            }
            return relation;
        }

        public List<BankList> GetBankList()
        {
            List<BankList> bank = new List<BankList>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select bank_id, bank_name from tbl_family_bank_master", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    BankList u = new BankList();
                                    u.bank_id = string.IsNullOrEmpty(reader["bank_id"].ToString()) ? 0 : Convert.ToInt32(reader["bank_id"]);
                                    u.bank_name = string.IsNullOrWhiteSpace(reader["bank_name"].ToString()) ? "" : reader["bank_name"].ToString();
                                    bank.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting Bank name List is:" + ex);
                }
            }
            return bank;
        }

        public List<CountryList> GetCountryList()
        {
            List<CountryList> country = new List<CountryList>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select cm_id, cm_name from tbl_countries_master", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    CountryList u = new CountryList();
                                    u.cm_id = string.IsNullOrEmpty(reader["cm_id"].ToString()) ? 0 : Convert.ToInt32(reader["cm_id"]);
                                    u.cm_name = string.IsNullOrWhiteSpace(reader["cm_name"].ToString()) ? "" : reader["cm_name"].ToString();
                                    //u.relationship_name = string.IsNullOrEmpty(reader["country_id"].ToString()) ? 0 : Convert.ToInt32(reader["country_id"]);
                                    country.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting Country List is:" + ex);
                }
            }
            return country;
        }

        public List<StateList> GetStateList(int? id)
        {
            List<StateList> states = new List<StateList>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    string query = "SELECT state_id, state_name FROM tbl_state_master WHERE country_id = @p_country_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        // cmd.Parameters.AddWithValue("@p_contry_id", id);
                        cmd.Parameters.AddWithValue("@p_country_id", id.HasValue ? (object)id.Value : DBNull.Value);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    StateList u = new StateList();
                                    u.state_id = string.IsNullOrEmpty(reader["state_id"].ToString()) ? 0 : Convert.ToInt32(reader["state_id"]);
                                    u.state_name = string.IsNullOrWhiteSpace(reader["state_name"].ToString()) ? "" : reader["state_name"].ToString();
                                    //u.relationship_name = string.IsNullOrEmpty(reader["country_id"].ToString()) ? 0 : Convert.ToInt32(reader["country_id"]);
                                    states.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting State List is:" + ex);
                }
            }
            return states;
        }

        public List<CityList> GetCityList(int? id)
        {
            List<CityList> city = new List<CityList>();
            //ResponseModel res = new ResponseModel();

            using (NpgsqlConnection con = new NpgsqlConnection(conString))
            {
                con.Open();
                try
                {
                    string query = "SELECT city_id, city_name FROM tbl_cities_master WHERE state_id = @p_state_id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        // cmd.Parameters.AddWithValue("@p_contry_id", id);
                        cmd.Parameters.AddWithValue("@p_state_id", id.HasValue ? (object)id.Value : DBNull.Value);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    CityList u = new CityList();
                                    u.city_id = string.IsNullOrEmpty(reader["city_id"].ToString()) ? 0 : Convert.ToInt32(reader["city_id"]);
                                    u.city_name = string.IsNullOrWhiteSpace(reader["city_name"].ToString()) ? "" : reader["city_name"].ToString();
                                    //u.relationship_name = string.IsNullOrEmpty(reader["country_id"].ToString()) ? 0 : Convert.ToInt32(reader["country_id"]);
                                    city.Add(u);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!!";
                    CommonHelper.write_log("The error while getting City List is:" + ex);
                }
            }
            return city;
        }

        public ResponseModelENF AddfamilyDetails(FamilyDetails famInfo)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conString))
                {
                    con.Open();
                    using (var transaction = con.BeginTransaction())
                    {
                        try
                        {

                            //using (var cmd = new NpgsqlCommand("INSERT INTO tbl_family_member_personal_details (relationship_code, first_name, last_name, gender, date_of_birth, contact_no, email_id, qualification, annual_income, is_active, created_by) VALUES (@relationship_code, @first_name, @last_name, @gender, @date_of_birth, @contact_no, @email_id, @qualification, @annual_income, @is_active, @created_by)", con))
                            using (var cmd = new NpgsqlCommand("INSERT INTO tbl_family_member_personal_details (employee_id, relationship_code, first_name, last_name, gender, date_of_birth, age, contact_no, email_id, qualification, annual_income, is_active, created_by, action, is_dependent, is_employeed) VALUES (@employee_id, @relationship_code, @first_name, @last_name, @gender, @date_of_birth, @age, @contact_no, @email_id, @qualification, @annual_income, @is_active, @created_by, @action, @is_dependent, @is_employeed) RETURNING family_id", con))

                            {
                                cmd.Parameters.AddWithValue("@employee_id", famInfo.employee_id);
                                cmd.Parameters.AddWithValue("@relationship_code", famInfo.relationship_code);
                                cmd.Parameters.AddWithValue("@first_name", famInfo.first_name);
                                cmd.Parameters.AddWithValue("@last_name", famInfo.last_name);
                                cmd.Parameters.AddWithValue("@gender", famInfo.gender);
                                cmd.Parameters.AddWithValue("@date_of_birth", famInfo.date_of_birth);
                                cmd.Parameters.AddWithValue("@age", famInfo.age);
                                cmd.Parameters.AddWithValue("@contact_no", famInfo.contact_no);
                                cmd.Parameters.AddWithValue("@email_id", famInfo.email_id);
                                cmd.Parameters.AddWithValue("@qualification", famInfo.qualification);
                                cmd.Parameters.AddWithValue("@annual_income", famInfo.annual_income);
                                cmd.Parameters.AddWithValue("@is_active", "Y");
                                //cmd.Parameters.AddWithValue("@created_by", famInfo.employeecode);
                                cmd.Parameters.AddWithValue("@created_by", int.Parse(famInfo.employeecode));
                                cmd.Parameters.AddWithValue("@action", "I");
                                cmd.Parameters.AddWithValue("@is_dependent", famInfo.is_dependent);
                                cmd.Parameters.AddWithValue("@is_employeed", famInfo.is_employeed);

                                var result = cmd.ExecuteScalar();

                                int family_id;
                                if (result != null && int.TryParse(result.ToString(), out family_id))
                                //if (result != null && int.TryParse(result.ToString(), out int family_id))
                                {
                                    //using (var secondCmd = new NpgsqlCommand("INSERT INTO tbl_family_bank_details (family_id, bank_ifsc, bank_account_name, bank_account_number, is_active, created_by, action) VALUES (@family_id, @bank_ifsc, @bank_account_name, @bank_account_number, @is_active, @created_by, @action)", con))
                                    //{
                                    //    secondCmd.Parameters.AddWithValue("@family_id", family_id);
                                    //    secondCmd.Parameters.AddWithValue("@bank_ifsc", famInfo.bank_ifsc);
                                    //    secondCmd.Parameters.AddWithValue("@bank_account_name", famInfo.bank_account_name);
                                    //    secondCmd.Parameters.AddWithValue("@bank_account_number", famInfo.bank_account_number);
                                    //    secondCmd.Parameters.AddWithValue("@is_active", "Y");
                                    //    // secondCmd.Parameters.AddWithValue("@created_by", famInfo.employeecode);
                                    //    secondCmd.Parameters.AddWithValue("@created_by", int.Parse(famInfo.employeecode));
                                    //    secondCmd.Parameters.AddWithValue("@action", "I");
                                    //    secondCmd.ExecuteNonQuery();
                                    //}

                                    using (var thirdCmd = new NpgsqlCommand("INSERT INTO tbl_family_member_address_details (family_id, country_id, state_id, city_id, pin_code, address, is_active, created_by, action) VALUES (@family_id, @country_id, @state_id, @city_id, @pin_code, @address, @is_active, @created_by, @action)", con))
                                    {
                                        thirdCmd.Parameters.AddWithValue("@family_id", family_id);
                                        thirdCmd.Parameters.AddWithValue("@country_id", famInfo.country_id);
                                        thirdCmd.Parameters.AddWithValue("@state_id", famInfo.state_id);
                                        thirdCmd.Parameters.AddWithValue("@city_id", famInfo.city_id);
                                        thirdCmd.Parameters.AddWithValue("@pin_code", famInfo.pin_code);
                                        thirdCmd.Parameters.AddWithValue("@address", famInfo.address);
                                        thirdCmd.Parameters.AddWithValue("@is_active", "Y");
                                        //thirdCmd.Parameters.AddWithValue("@created_by", famInfo.employeecode);
                                        thirdCmd.Parameters.AddWithValue("@created_by", int.Parse(famInfo.employeecode));
                                        thirdCmd.Parameters.AddWithValue("@action", "I");

                                        thirdCmd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    res.message = "Family Details inserted successfully.";
                                    res.status = true;
                                }
                                else
                                {
                                    res.message = "Insertion failed or no ID returned.";
                                    res.status = false;
                                    return res;
                                }
                            }

                            //transaction.Commit();
                            //res.message = "Family Details inserted successfully.";
                            //res.status = true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            CommonHelper.write_log($"Error during insert family details: {ex.Message}");
                            res.message = "Error during insert family details.";
                            res.status = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log($"Database connection failed: {ex.Message}");
                res.message = "Database connection failed.";
                res.status = false;
            }

            return res;
        }

        public List<FamilyDetails> GetFamilyDetailsList(int employeeID)
        {
            List<FamilyDetails> result = new List<FamilyDetails>();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conString))
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM enf_fn_get_family_member_details(@p_employee_id)", con))
                    {
                        cmd.Parameters.AddWithValue("@p_employee_id", employeeID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FamilyDetails u = new FamilyDetails
                                {
                                    family_id = reader.IsDBNull(reader.GetOrdinal("family_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("family_id")),
                                    relationship_name = reader.IsDBNull(reader.GetOrdinal("relationship_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("relationship_name")),
                                    first_name = reader.IsDBNull(reader.GetOrdinal("first_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("first_name")),
                                    last_name = reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("last_name")),
                                    gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? string.Empty : reader.GetString(reader.GetOrdinal("gender")),
                                    date_of_birth = reader.IsDBNull(reader.GetOrdinal("date_of_birth")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                                    age = reader.IsDBNull(reader.GetOrdinal("age")) ? 0 : reader.GetInt32(reader.GetOrdinal("age")),
                                    contact_no = reader.IsDBNull(reader.GetOrdinal("contact_no")) ? string.Empty : reader.GetString(reader.GetOrdinal("contact_no")),
                                    email_id = reader.IsDBNull(reader.GetOrdinal("email_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("email_id")),
                                    qualification = reader.IsDBNull(reader.GetOrdinal("qualification")) ? string.Empty : reader.GetString(reader.GetOrdinal("qualification")),
                                    annual_income = reader.IsDBNull(reader.GetOrdinal("annual_income")) ? 0.0m : reader.GetDecimal(reader.GetOrdinal("annual_income")),
                                    //bank_ifsc = reader.IsDBNull(reader.GetOrdinal("bank_ifsc")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_ifsc")),
                                    //bank_account_name = reader.IsDBNull(reader.GetOrdinal("bank_account_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_account_name")),
                                    //bank_account_number = reader.IsDBNull(reader.GetOrdinal("bank_account_number")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_account_number")),
                                    cm_name = reader.IsDBNull(reader.GetOrdinal("cm_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("cm_name")),
                                    state_name = reader.IsDBNull(reader.GetOrdinal("state_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("state_name")),
                                    city_name = reader.IsDBNull(reader.GetOrdinal("city_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("city_name")),
                                    pin_code = reader.IsDBNull(reader.GetOrdinal("pin_code")) ? string.Empty : reader.GetString(reader.GetOrdinal("pin_code")),
                                    address = reader.IsDBNull(reader.GetOrdinal("address")) ? string.Empty : reader.GetString(reader.GetOrdinal("address")),
                                    is_dependent = reader.IsDBNull(reader.GetOrdinal("is_dependent")) ? false : reader.GetBoolean(reader.GetOrdinal("is_dependent")),
                                    is_employeed = reader.IsDBNull(reader.GetOrdinal("is_employeed")) ? false : reader.GetBoolean(reader.GetOrdinal("is_employeed")),


                                    // Handling int
                                    //family_id = reader.IsDBNull(reader.GetOrdinal("family_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("family_id")),

                                    // Handling string
                                    //relationship_name = reader.IsDBNull(reader.GetOrdinal("relationship_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("relationship_name")),

                                    // Handling char
                                    //gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? '\0' : reader.GetChar(reader.GetOrdinal("gender")),

                                    // Handling bool
                                    //is_active = reader.IsDBNull(reader.GetOrdinal("is_active")) ? false : reader.GetBoolean(reader.GetOrdinal("is_active")),

                                    // Handling nullable DateTime (example)
                                    //date_of_birth = reader.IsDBNull(reader.GetOrdinal("date_of_birth")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("date_of_birth")),

                                    // Handling decimal (example)
                                    //salary = reader.IsDBNull(reader.GetOrdinal("salary")) ? 0.0m : reader.GetDecimal(reader.GetOrdinal("salary"))
                                };

                                result.Add(u);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false;
                res.message = "Errorr!!!";
                CommonHelper.write_log("The error getting while fetching Family detail List:" + ex);
            }

            return result;
        }

        public FamilyDetails GetSelfDetails(int employeeID)
        {
            FamilyDetails result = new FamilyDetails();
            try
            {
                using (var con = new NpgsqlConnection(conString))
                {
                    con.Open();

                    using (var cmd = new NpgsqlCommand("SELECT * FROM enf_fn_get_self_details(@employeeId)", con))
                    {
                        cmd.Parameters.AddWithValue("employeeId", employeeID);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = new FamilyDetails
                                {
                                    address = reader.IsDBNull(reader.GetOrdinal("address")) ? string.Empty : reader.GetString(reader.GetOrdinal("address")),
                                    cm_name = reader.IsDBNull(reader.GetOrdinal("cm_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("cm_name")),
                                    country_id = reader.IsDBNull(reader.GetOrdinal("cm_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("cm_id")),
                                    state_id = reader.IsDBNull(reader.GetOrdinal("state_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("state_id")),
                                    state_name = reader.IsDBNull(reader.GetOrdinal("state_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("state_name")),
                                    city_id = reader.IsDBNull(reader.GetOrdinal("city_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("city_id")),
                                    city_name = reader.IsDBNull(reader.GetOrdinal("city_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("city_name")),
                                    pin_code = reader.IsDBNull(reader.GetOrdinal("pin_code")) ? string.Empty : reader.GetString(reader.GetOrdinal("pin_code"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false;
                res.message = "Errorr!!!";
                CommonHelper.write_log($"Error retrieving self details: {ex.Message}");
            }

            return result;
        }

        public List<FamilyDetails> GetFamilyDetailsById(int? id)
        {
            List<FamilyDetails> result = new List<FamilyDetails>();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conString))
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM enf_fn_get_family_member_details_by_family_id(@p_family_id)", con))
                    {
                        // Add the parameter for family_id
                        cmd.Parameters.AddWithValue("p_family_id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FamilyDetails u = new FamilyDetails
                                {
                                    family_id = reader.IsDBNull(reader.GetOrdinal("family_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("family_id")),
                                    relationship_code = reader.IsDBNull(reader.GetOrdinal("relationship_code")) ? string.Empty : reader.GetString(reader.GetOrdinal("relationship_code")),
                                    relationship_name = reader.IsDBNull(reader.GetOrdinal("relationship_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("relationship_name")),
                                    first_name = reader.IsDBNull(reader.GetOrdinal("first_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("first_name")),
                                    last_name = reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("last_name")),
                                    gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? string.Empty : reader.GetString(reader.GetOrdinal("gender")),
                                    date_of_birth = reader.IsDBNull(reader.GetOrdinal("date_of_birth")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                                    age = reader.IsDBNull(reader.GetOrdinal("age")) ? 0 : reader.GetInt32(reader.GetOrdinal("age")),
                                    contact_no = reader.IsDBNull(reader.GetOrdinal("contact_no")) ? string.Empty : reader.GetString(reader.GetOrdinal("contact_no")),
                                    email_id = reader.IsDBNull(reader.GetOrdinal("email_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("email_id")),
                                    qualification = reader.IsDBNull(reader.GetOrdinal("qualification")) ? string.Empty : reader.GetString(reader.GetOrdinal("qualification")),
                                    annual_income = reader.IsDBNull(reader.GetOrdinal("annual_income")) ? 0.0m : reader.GetDecimal(reader.GetOrdinal("annual_income")),
                                    //bank_ifsc = reader.IsDBNull(reader.GetOrdinal("bank_ifsc")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_ifsc")),
                                    //bank_account_name = reader.IsDBNull(reader.GetOrdinal("bank_account_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_account_name")),
                                    //bank_account_number = reader.IsDBNull(reader.GetOrdinal("bank_account_number")) ? string.Empty : reader.GetString(reader.GetOrdinal("bank_account_number")),
                                    country_id = reader.IsDBNull(reader.GetOrdinal("country_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("country_id")),
                                    cm_name = reader.IsDBNull(reader.GetOrdinal("cm_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("cm_name")),
                                    state_id = reader.IsDBNull(reader.GetOrdinal("state_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("state_id")),
                                    state_name = reader.IsDBNull(reader.GetOrdinal("state_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("state_name")),
                                    city_id = reader.IsDBNull(reader.GetOrdinal("city_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("city_id")),
                                    city_name = reader.IsDBNull(reader.GetOrdinal("city_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("city_name")),
                                    pin_code = reader.IsDBNull(reader.GetOrdinal("pin_code")) ? string.Empty : reader.GetString(reader.GetOrdinal("pin_code")),
                                    address = reader.IsDBNull(reader.GetOrdinal("address")) ? string.Empty : reader.GetString(reader.GetOrdinal("address")),
                                    is_dependent = reader.IsDBNull(reader.GetOrdinal("is_dependent")) ? false : reader.GetBoolean(reader.GetOrdinal("is_dependent")),
                                    is_employeed = reader.IsDBNull(reader.GetOrdinal("is_employeed")) ? false : reader.GetBoolean(reader.GetOrdinal("is_employeed")),

                                };

                                result.Add(u);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false;
                res.message = "Errorr!!!";
                CommonHelper.write_log("Error while fetching Family detail List by Family ID: " + ex);
            }

            return result;
        }

        public ResponseModelENF UpdatefamilyDetails(FamilyDetails famInfo)
        {
            ResponseModelENF res = new ResponseModelENF();

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conString))
                {
                    con.Open();
                    using (var transaction = con.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new NpgsqlCommand("SELECT enf_fn_update_familydetails(@p_family_id, @p_employee_id, @p_first_name, @p_last_name, @p_date_of_birth, @p_age, @p_gender, @p_qualification, @p_annual_income, @p_contact_no, @p_email_id, @p_relationship_code, @p_country_id, @p_city_id, @p_state_id, @p_pin_code, @p_address, @p_created_by, @p_is_dependent)", con))
                            {
                                cmd.Parameters.AddWithValue("@p_family_id", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.family_id);
                                cmd.Parameters.AddWithValue("@p_employee_id", NpgsqlTypes.NpgsqlDbType.Bigint, famInfo.employee_id);
                                cmd.Parameters.AddWithValue("@p_first_name", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.first_name);
                                cmd.Parameters.AddWithValue("@p_last_name", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.last_name);
                                cmd.Parameters.AddWithValue("@p_date_of_birth", NpgsqlTypes.NpgsqlDbType.Date, famInfo.date_of_birth);
                                cmd.Parameters.AddWithValue("@p_age", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.age);
                                cmd.Parameters.AddWithValue("@p_gender", NpgsqlTypes.NpgsqlDbType.Char, famInfo.gender);
                                cmd.Parameters.AddWithValue("@p_qualification", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.qualification);
                                cmd.Parameters.AddWithValue("@p_annual_income", NpgsqlTypes.NpgsqlDbType.Numeric, famInfo.annual_income);
                                cmd.Parameters.AddWithValue("@p_contact_no", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.contact_no);
                                cmd.Parameters.AddWithValue("@p_email_id", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.email_id);
                                cmd.Parameters.AddWithValue("@p_relationship_code", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.relationship_code);
                                cmd.Parameters.AddWithValue("@p_country_id", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.country_id);
                                cmd.Parameters.AddWithValue("@p_city_id", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.city_id);
                                cmd.Parameters.AddWithValue("@p_state_id", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.state_id);
                                cmd.Parameters.AddWithValue("@p_pin_code", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.pin_code);
                                cmd.Parameters.AddWithValue("@p_address", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.address);
                                //cmd.Parameters.AddWithValue("@p_bank_account_number", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.bank_account_number);
                                //cmd.Parameters.AddWithValue("@p_bank_ifsc", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.bank_ifsc);
                                //cmd.Parameters.AddWithValue("@p_bank_account_name", NpgsqlTypes.NpgsqlDbType.Varchar, famInfo.bank_account_name);
                                //cmd.Parameters.AddWithValue("@p_created_by", NpgsqlTypes.NpgsqlDbType.Integer, famInfo.employeecode);  // Adjust as necessary
                                cmd.Parameters.AddWithValue("@p_created_by", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(famInfo.employeecode));
                                cmd.Parameters.AddWithValue("@p_is_dependent", NpgsqlTypes.NpgsqlDbType.Boolean, famInfo.is_dependent);
                                cmd.Parameters.AddWithValue("@p_is_employeed", NpgsqlTypes.NpgsqlDbType.Boolean, famInfo.is_employeed);



                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                                res.message = "Family details updated successfully.";
                                res.status = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            res.message = "Error during Update Family Details.";
                            res.status = false;
                            CommonHelper.write_log($"Error during update family details with new Family ID: {ex.Message}");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log($"Database connection failed: {ex.Message}");
                res.message = "Database connection failed.";
                res.status = false;
            }

            return res;
        }

        public ResponseModelENF DeactivatefamilyMember(int familyId, string employeecode, int reason_id, string remark)
        {
            using (var connection = new NpgsqlConnection(conString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertQuery = @"
                    INSERT INTO tbl_deactive_family_member (family_id, reason_id, remark, isactive, de_active_date, created_by, action)
                    VALUES (@family_id, @reason_id, @remark, @isactive, @de_active_date, @created_by, @action)";

                        using (var cmd = new NpgsqlCommand(insertQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@family_id", familyId);
                            cmd.Parameters.AddWithValue("@reason_id", reason_id);
                            cmd.Parameters.AddWithValue("@remark", remark);
                            cmd.Parameters.AddWithValue("@isactive", "Y");
                            cmd.Parameters.AddWithValue("@de_active_date", DateTime.Now);
                            //cmd.Parameters.AddWithValue("@created_by", "2020");
                            cmd.Parameters.AddWithValue("@created_by", int.Parse(employeecode));
                            cmd.Parameters.AddWithValue("@action", "I");
                            cmd.ExecuteNonQuery();
                        }

                        string updatePersonalDetailsQuery = @"
                    UPDATE tbl_family_member_personal_details
                    SET de_active_date = @de_active_date, is_active = @is_active, action = @action
                    WHERE family_id = @family_id";

                        using (var cmd = new NpgsqlCommand(updatePersonalDetailsQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@de_active_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@is_active", "N");
                            cmd.Parameters.AddWithValue("@action", "D");
                            cmd.Parameters.AddWithValue("@family_id", familyId);

                            cmd.ExecuteNonQuery();
                        }

                        //    string updateBankDetailsQuery = @"
                        //UPDATE tbl_family_bank_details
                        //SET de_active_date = @de_active_date, is_active = @is_active, action = @action
                        //WHERE family_id = @family_id";

                        //    using (var cmd = new NpgsqlCommand(updateBankDetailsQuery, connection, transaction))
                        //    {
                        //        cmd.Parameters.AddWithValue("@de_active_date", DateTime.Now);
                        //        cmd.Parameters.AddWithValue("@is_active", "N");
                        //        cmd.Parameters.AddWithValue("@action", "D");
                        //        cmd.Parameters.AddWithValue("@family_id", familyId);
                        //        cmd.ExecuteNonQuery();
                        //    }

                        string updateAddressDetailsQuery = @"
                    UPDATE tbl_family_member_address_details
                    SET de_active_date = @de_active_date, is_active = @is_active, action = @action
                    WHERE family_id = @family_id";

                        using (var cmd = new NpgsqlCommand(updateAddressDetailsQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@de_active_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@is_active", "N");
                            cmd.Parameters.AddWithValue("@action", "D");
                            cmd.Parameters.AddWithValue("@family_id", familyId);
                            cmd.ExecuteNonQuery();
                        }


                        string updateFamilyPercentageSharingQuery = @"
                    UPDATE tbl_family_percentage_sharing
                    SET de_active_date = @de_active_date, is_active = @is_active
                    WHERE family_id = @family_id";

                        using (var cmd = new NpgsqlCommand(updateFamilyPercentageSharingQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@de_active_date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@is_active", "N");                            
                            cmd.Parameters.AddWithValue("@family_id", familyId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        res.message = "Family details Deactivated successfully.";
                        res.status = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        CommonHelper.write_log($"Error in deactivating family member: {ex.Message}");
                        res.message = "Error in deactivating family member.";
                        res.status = false;
                        //throw new Exception("Error in deactivating family member", ex);
                    }
                }

            }
            return res;
        }


    }
}