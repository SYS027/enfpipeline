using EDF.Web.DataAccessLayer;
using ENF.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using static ENF.Models.EmpDetails;



namespace ENF.Data_Access_Layer.DAL
{

    public class DALEmpDetails
    {
        static string con = ConfigurationManager.ConnectionStrings["myconn"].ToString();


        public List<EmployeeDetailsNC> GetEmployeeDetails(EmpDetailsSearch e)
        {
            var employeeDetailsList = new List<EmployeeDetailsNC>();
            var employeeDetailsDict = new Dictionary<string, EmployeeDetailsNC>();

            try
            {
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();

                    using (var command = new NpgsqlCommand("SELECT * FROM piyush_get_active_nomination_categories()", conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var nominationId = reader["nomination_id"].ToString();
                                EmployeeDetailsNC employeeDetails;
                                if (employeeDetailsDict.TryGetValue(nominationId, out employeeDetails))
                                {
                                    if (reader["relationship_name"] != DBNull.Value)
                                    {
                                        employeeDetails.relationship_names.Add(reader["relationship_name"].ToString());
                                    }

                                    if (reader["relationship_code"] != DBNull.Value)
                                    {
                                        employeeDetails.relationship_codes.Add(reader["relationship_code"].ToString());
                                    }
                                }
                                else
                                {
                                    employeeDetails = new EmployeeDetailsNC
                                    {
                                        nomination_id = nominationId,
                                        nomination_category_code = reader["nomination_category_code"].ToString(),
                                        nomination_category_name = reader["nomination_category_name"].ToString(),
                                        is_active = reader["is_active"].ToString(),
                                        created_by = reader["created_by"].ToString(),
                                        created_date = reader["created_date"].ToString(),
                                        de_active_date = reader["de_active_date"] != DBNull.Value ? reader["de_active_date"].ToString() : null,
                                        active_date = reader["active_date"] != DBNull.Value ? reader["active_date"].ToString() : null,
                                        relationship_codes = new List<string>(),
                                        relationship_names = new List<string>()
                                    };

                                    if (reader["relationship_name"] != DBNull.Value)
                                    {
                                        employeeDetails.relationship_names.Add(reader["relationship_name"].ToString());
                                    }

                                    if (reader["relationship_code"] != DBNull.Value)
                                    {
                                        employeeDetails.relationship_codes.Add(reader["relationship_code"].ToString());
                                    }

                                    employeeDetailsDict[nominationId] = employeeDetails;
                                    employeeDetailsList.Add(employeeDetails);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log(ex.Message);
            }

            return employeeDetailsList;
        }

        public List<EmployeeRelationship> GetEmployeeRelationshipDetails(EmpDetailsSearch e)
        {
            var RelationshipDetailsDetailsList = new List<EmployeeRelationship>();

            try
            {
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();

                    using (var command = new NpgsqlCommand("SELECT * FROM get_emp_relationship_data()", conn))
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var employeeDetails = new EmployeeRelationship
                                {
                                    nomination_id = reader["relationship_id"].ToString(),
                                    relationship_code = reader["relationship_code"].ToString(),
                                    relationship_name = reader["relationship_name"].ToString(),
                                    is_active = reader["is_active"].ToString(),
                                    created_by = reader["created_by"].ToString(),
                                    created_date = reader["created_date"].ToString(),
                                    de_active_date = reader["de_active_date"].ToString(),
                                    active_date = reader["active_date"].ToString(),
                                };

                                RelationshipDetailsDetailsList.Add(employeeDetails);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log(ex.Message);
            }

            return RelationshipDetailsDetailsList;
        }

        public Responce InsertCategoryMaster(EmployeeDetailsNC e)
        {
            Responce res = new Responce();

            try
            {
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = new NpgsqlCommand("SELECT insert_nomination_category(@p_nomination_category_code, @p_nomination_category_name, @p_is_active, @p_created_by, @p_created_date, @p_active_date)", conn))
                            {
                                var nominationCategoryCode = GenerateCategoryCode(e.nomination_category_name);
                                cmd.Parameters.AddWithValue("@p_nomination_category_code", nominationCategoryCode);
                                cmd.Parameters.AddWithValue("@p_nomination_category_name", e.nomination_category_name);
                                cmd.Parameters.AddWithValue("@p_is_active", "Y");
                                cmd.Parameters.AddWithValue("@p_created_by", int.Parse(e.employeecode));
                                cmd.Parameters.AddWithValue("@p_created_date", DateTime.Now);
                                cmd.Parameters.AddWithValue("@p_active_date", DateTime.Now);

                                // Expecting the function to return a string, not an integer
                                var result = (string)cmd.ExecuteScalar();

                                if (!string.IsNullOrEmpty(result))
                                {
                                    foreach (var item in e.Eligble)
                                    {
                                        string relationshipCode = item.Substring(0, 3).ToUpper();

                                        using (var secondCmd = new NpgsqlCommand("SELECT inserttbl_nomination_category_eligibilty_with_relationship(@p_nomination_category_code, @p_nomination_category_name, @p_eligible, @p_is_active, @p_created_by, @p_created_date, @p_active_date)", conn))
                                        {
                                            secondCmd.Parameters.AddWithValue("@p_nomination_category_code", result);
                                            secondCmd.Parameters.AddWithValue("@p_nomination_category_name", relationshipCode);
                                            secondCmd.Parameters.AddWithValue("@p_eligible", "Y");
                                            secondCmd.Parameters.AddWithValue("@p_is_active", "Y");
                                            secondCmd.Parameters.AddWithValue("@p_created_by", int.Parse(e.employeecode));
                                            secondCmd.Parameters.AddWithValue("@p_created_date", DateTime.Now);
                                            secondCmd.Parameters.AddWithValue("@p_active_date", DateTime.Now);
                                            secondCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                else
                                {
                                    res.Message = "First insertion failed or no code returned.";
                                }
                            }

                            transaction.Commit();
                            res.Status = true;
                            res.Message = "Insertion successful.";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            res.Message = "Data Already Exists or another error occurred.";
                            res.Status = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log($"Database connection failed: {ex.Message}");
                res.Message = "Database connection failed.";
            }

            return res;
        }
        public List<CategoryEligibility> GetActiveEligibleCategoriesFromDb()
        {
            List<CategoryEligibility> categories = new List<CategoryEligibility>();

            try
            {
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM piyush_get_active_eligible_categories()", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CategoryEligibility category = new CategoryEligibility
                                {
                                    NominationCategoryCode = reader["nomination_category_code"].ToString(),
                                    RelationshipCode = reader["relationship_code"].ToString()
                                };
                                categories.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.write_log($"Database query failed: {ex.Message}");
            }

            return categories;
        }

        private string GenerateCategoryCode(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return string.Empty;

            var words = categoryName.Split(' ');
            if (words.Length == 1)
            {
                char firstChar = categoryName.Trim()[0];
                return $"{char.ToUpper(firstChar)}N";
            }

            if (words.Length < 2)
            {
                return string.Empty;
            }

            var firstCharFirstWord = words[0][0];
            var firstCharSecondWord = words[1][0];
            return $"{char.ToUpper(firstCharFirstWord)}{char.ToUpper(firstCharSecondWord)}";
        }



        public Responce UpdateRec(int nominationId, char status, string nominationCategoryCode)
        {
            Responce res = new Responce();

            try
            {
                using (var connection = new NpgsqlConnection(con))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("SELECT update_rec_nomination_and_category(@p_nominationid, @p_nominationCategoryCode)", connection))
                    {
                        command.Parameters.AddWithValue("@p_nominationid", nominationId);
                        command.Parameters.AddWithValue("@p_nominationCategoryCode", nominationCategoryCode);

                        var rowsAffected = (int)command.ExecuteNonQuery();

                        if (rowsAffected >= -1)
                        {
                            res.Status = true;
                            res.Message = "Update successful.";
                        }
                        else
                        {
                            res.Status = false;
                            res.Message = "Something went wrong";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Status = false;
                res.Message = "Something went wrong";
                CommonHelper.write_log($"{ex.Message}");
            }

            return res;
        }

        //public Responce Updatestatus(List<CategoryUpdateModel> data)
        //{
        //    Responce res = new Responce();
        //    try
        //    {
        //        using (var connection = new NpgsqlConnection(con))
        //        {
        //            connection.Open();

        //            foreach (var item in data)
        //            {
        //                using (var cmd = new NpgsqlCommand("update_or_insert_eligibility_status", connection))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;

        //                    foreach (var relationshipCode in item.SelectedValues)
        //                    {
        //                        cmd.Parameters.Clear();
        //                        cmd.Parameters.AddWithValue("p_status_id", DBNull.Value); 
        //                        cmd.Parameters.AddWithValue("p_nomination_category_code", item.NominationCategoryCode);
        //                        cmd.Parameters.AddWithValue("p_relationship_code", relationshipCode);
        //                        cmd.Parameters.AddWithValue("p_status", item.IsActive ? "Y" : "N");
        //                        cmd.Parameters.AddWithValue("p_emp_id", int.Parse(item.employeecode));

        //                        cmd.ExecuteNonQuery();
        //                    }
        //                }

        //                if (item.UnselectedValues.Any())
        //                {
        //                    foreach (var relationshipCode in item.UnselectedValues)
        //                    {
        //                        using (var cmd = new NpgsqlCommand("update_unselected_eligibility_status", connection))
        //                        {
        //                            cmd.CommandType = CommandType.StoredProcedure;

        //                            cmd.Parameters.Clear();
        //                            cmd.Parameters.AddWithValue("p_nomination_category_code", item.NominationCategoryCode);
        //                            cmd.Parameters.AddWithValue("p_relationship_code", relationshipCode);
        //                            cmd.Parameters.AddWithValue("p_emp_id", int.Parse(item.employeecode));

        //                            cmd.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Message = ex.Message;
        //        res.Status = false;
        //        return res;
        //    }

        //    res.Status = true;
        //    res.Message = "Status updated successfully";
        //    return res;
        //}

        public Responce Updatestatus(List<CategoryUpdateModel> data)
        {
            Responce res = new Responce();
            try
            {
                using (var connection = new NpgsqlConnection(con))
                {
                    connection.Open();

                    foreach (var item in data)
                    {
                        using (var cmd = new NpgsqlCommand("CALL update_or_insert_eligibility_status(@p_status_id, @p_nomination_category_code, @p_relationship_code, @p_status, @p_emp_id)", connection))
                        {
                            cmd.CommandType = CommandType.Text; 

                            foreach (var relationshipCode in item.SelectedValues)
                            {
                                cmd.Parameters.Clear();
                               
                                cmd.Parameters.AddWithValue("@p_status_id", DBNull.Value);
                                cmd.Parameters.AddWithValue("@p_nomination_category_code", item.NominationCategoryCode);
                                cmd.Parameters.AddWithValue("@p_relationship_code", relationshipCode);
                                cmd.Parameters.AddWithValue("@p_status", item.IsActive ? "Y" : "N");
                                cmd.Parameters.AddWithValue("@p_emp_id", int.Parse(item.employeecode));

                                cmd.ExecuteNonQuery();
                            }
                        }

                        if (item.UnselectedValues.Any())
                        {
                            foreach (var relationshipCode in item.UnselectedValues)
                            {
                                using (var cmd = new NpgsqlCommand("CALL update_unselected_eligibility_status(@p_nomination_category_code, @p_relationship_code, @p_emp_id)", connection))
                                {
                                    cmd.CommandType = CommandType.Text; // Using CommandType.Text for CALL statements

                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@p_nomination_category_code", item.NominationCategoryCode);
                                    cmd.Parameters.AddWithValue("@p_relationship_code", relationshipCode);
                                    cmd.Parameters.AddWithValue("@p_emp_id", int.Parse(item.employeecode));

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Status = false;
                return res;
            }

            res.Status = true;
            res.Message = "Status updated successfully";
            return res;
        }




    }
}
