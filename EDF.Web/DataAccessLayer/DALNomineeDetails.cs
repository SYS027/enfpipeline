using Dapper;
using EDF.Web.Models;
using ENF.Models;
using ENF_NEW.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;



namespace ENF.Data_Access_Layer.DAL
{
    public class DALNomineeDetails
    {
        static string con = ConfigurationManager.ConnectionStrings["myconn"].ToString();



        public List<FamilyMemberDetails> GetFamilyMemberDetails(long employeeId)
        {
            var familyMemberDetailsList = new List<FamilyMemberDetails>();

            try
            {
                using (var conn = new NpgsqlConnection(con))
                {
                    conn.Open();

                    using (var command = new NpgsqlCommand("SELECT * FROM get_family_member_details_test(@EmployeeId)", conn))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", NpgsqlTypes.NpgsqlDbType.Bigint, employeeId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var familyMemberDetails = new FamilyMemberDetails
                                {
                                    FamilyId = reader["family_id"] != DBNull.Value ? Convert.ToInt32(reader["family_id"]) : 0,
                                    EmployeeId = reader["employee_id"] != DBNull.Value ? Convert.ToInt64(reader["employee_id"]) : 0,
                                    FirstName = reader["first_name"] != DBNull.Value ? reader["first_name"].ToString() : string.Empty,
                                    LastName = reader["last_name"] != DBNull.Value ? reader["last_name"].ToString() : string.Empty,
                                    DateOfBirth = reader["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(reader["date_of_birth"]) : (DateTime?)null,
                                    Gender = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : string.Empty,
                                    Qualification = reader["qualification"] != DBNull.Value ? reader["qualification"].ToString() : string.Empty,
                                    AnnualIncome = reader["annual_income"] != DBNull.Value ? Convert.ToDecimal(reader["annual_income"]) : 0,
                                    ContactNo = reader["contact_no"] != DBNull.Value ? reader["contact_no"].ToString() : string.Empty,
                                    EmailId = reader["email_id"] != DBNull.Value ? reader["email_id"].ToString() : string.Empty,
                                    EmailSendStatus = reader["email_send_status"] != DBNull.Value ? reader["email_send_status"].ToString() : string.Empty,
                                    IsActive = reader["is_active"] != DBNull.Value ? reader["is_active"].ToString() : string.Empty,
                                    DeActiveDate = reader["de_active_date"] != DBNull.Value ? Convert.ToDateTime(reader["de_active_date"]) : (DateTime?)null,
                                    CreatedBy = reader["created_by"] != DBNull.Value ? Convert.ToInt32(reader["created_by"]) : 0,
                                    CreatedDate = reader["created_date"] != DBNull.Value ? Convert.ToDateTime(reader["created_date"]) : DateTime.Now,
                                    UpdatedDate = reader["updated_date"] != DBNull.Value ? Convert.ToDateTime(reader["updated_date"]) : (DateTime?)null,
                                    RelationshipCode = reader["relationship_code"] != DBNull.Value ? reader["relationship_code"].ToString() : string.Empty,
                                    Age = reader["age"] != DBNull.Value ? Convert.ToInt32(reader["age"]) : 0,
                                    Action = reader["action"] != DBNull.Value ? reader["action"].ToString() : string.Empty,
                                    //pecentage = reader["share_of_percentage"] != DBNull.Value ? reader["share_of_percentage"].ToString() : string.Empty,
                                    pecentage = reader["share_of_percentage"] != DBNull.Value ? Convert.ToInt32(reader["share_of_percentage"]) : 0,
                                    nomination_category_code = reader["nomination_category_code"] != DBNull.Value ? reader["nomination_category_code"].ToString() : string.Empty,
                                    relationship_code = reader["relationship_code"] != DBNull.Value ? reader["relationship_code"].ToString() : string.Empty,
                                    relationship_name = reader["s_relationship_name"] != DBNull.Value ? reader["s_relationship_name"].ToString() : string.Empty,
                                };

                                familyMemberDetailsList.Add(familyMemberDetails);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

            return familyMemberDetailsList;
        }

        public Responce InsertFamilyPercentageSharing(NominationCategory nominationCategories, int employeeId)
        {
            Responce responce = new Responce();

            try
            {
                using (var connection = new NpgsqlConnection(con))
                {
                    connection.Open();

                    foreach (var entry in nominationCategories.Entries)
                    {
                        using (var command = new NpgsqlCommand("CALL insert_or_update_family_percentage_sharing(@p_family_id, @p_employee_id, @p_nomination_category_code, @p_relationship_code, @p_share_of_percentage, @p_is_active, @p_flag, @p_guardian_name);", connection))
                        {
                            command.CommandType = CommandType.Text;

                            command.Parameters.AddWithValue("@p_family_id", NpgsqlTypes.NpgsqlDbType.Integer, entry.FamilyId);
                            command.Parameters.AddWithValue("@p_employee_id", NpgsqlTypes.NpgsqlDbType.Integer, employeeId);
                            command.Parameters.AddWithValue("@p_nomination_category_code", NpgsqlTypes.NpgsqlDbType.Varchar, nominationCategories.NominationId.ToString());
                            command.Parameters.AddWithValue("@p_relationship_code", NpgsqlTypes.NpgsqlDbType.Varchar, entry.RelationshipCode.ToString());
                            command.Parameters.AddWithValue("@p_share_of_percentage", NpgsqlTypes.NpgsqlDbType.Numeric, entry.Percentage);
                            command.Parameters.AddWithValue("@p_is_active", NpgsqlTypes.NpgsqlDbType.Char, entry.flag);
                            command.Parameters.AddWithValue("@p_flag", entry.flag);

                            command.Parameters.AddWithValue("@p_guardian_name", entry.guardianName ?? (object)DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }

                    responce.Status = true;
                    responce.Message = "Inserted successfully.";
                }
            }
            catch (Exception ex)
            {
                responce.Status = false;
                responce.Message = "Error: " + ex.Message;
            }

            return responce;
        }

        public List<EmployeeFullDetails> GetEmployeeFullDetails(int empId)
        {
            using (var connection = new NpgsqlConnection(con))
            {
                var query = "SELECT * FROM public.get_employee_full_details(@EmpId);";
                var parameters = new { EmpId = empId };

                var results = connection.Query(query, parameters);

                var groupedResults = results
                    .GroupBy(row => new
                    {
                        NominationCategoryCode = (string)row.nomination_category_code,
                        NominationCategoryName = (string)row.nomination_category_name
                    })
                    .Select(group => new EmployeeFullDetails
                    {
                        NominationCategoryCode = group.Key.NominationCategoryCode,
                        NominationCategoryName = group.Key.NominationCategoryName,
                        FamilyDetails = group.Select(row => new FamilyDetailsND
                        {
                            Relationship_Code = (string)row.relationship_code,
                            Relationship_Name = (string)row.relationship_name,
                            Full_Name = (string)row.full_name,
                            Date_Of_Birth = (DateTime)row.date_of_birth,
                            Family_Id = (int)row.family_id,
                            Share_Of_Percentage = row.share_of_percentage != null ? (decimal?)row.share_of_percentage : null
                        }).ToList()
                    })
                    .ToList();

                return groupedResults;
            }
        }

        public List<EmployeeFullDetail> GetEmployeeFalilyDetails(int empId, string nomination_cat_code)
        {
            using (var connection = new NpgsqlConnection(con))
            {
                var query = "SELECT * FROM public.get_employee_family_details(@EmpId, @nom_cate_code);";
                DynamicParameters para = new DynamicParameters();
                para.Add("@empid", empId);
                para.Add("@nom_cate_code", nomination_cat_code);
                //var parameters = new { EmpId = empId, nom_cate_code : nomination_cat_code};

                var results = connection.Query(query, para).ToList();



                var groupedResults = results
                    .GroupBy(row => new
                    {
                        NominationCategoryCode = (string)row.nomination_category_code,
                        NominationCategoryName = (string)row.nomination_category_name
                    })
                    .Select(group => new EmployeeFullDetail
                    {
                        NominationCategoryCode = group.Key.NominationCategoryCode,
                        NominationCategoryName = group.Key.NominationCategoryName,
                        FamilyDetails = group.Select(row => new FamilyDetail
                        {
                            RelationshipCode = (string)row.relationship_code,
                            RelationshipName = (string)row.relationship_name,
                            FullName = (string)row.full_name,
                            DateOfBirth = (DateTime)row.date_of_birth,
                            FamilyId = (int)row.family_id,
                            ShareOfPercentage = row.share_of_percentage != null ? (decimal?)row.share_of_percentage : null,
                            pd_is_active = (string)row.pd_is_active,
                            perc_is_active = (string)row.perc_is_active,
                            guardian_name = (string)row.guardian_name
                        }).ToList().OrderByDescending(x => x.pd_is_active).ToList()
                    })
                    .ToList();

                return groupedResults;
            }
        }

        public List<FamilyPercentageSharingHistory> GetFamilyPercentageSharingHistory(string nominationCategoryCode, int employeeId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(con))
                {
                    var query = "SELECT * FROM public.get_family_percentage_sharing_history(@NominationCategoryCode, @EmployeeId);";

                    connection.Open();

                    var parameters = new
                    {
                        NominationCategoryCode = nominationCategoryCode,
                        EmployeeId = employeeId
                    };

                    var result = connection.Query<FamilyPercentageSharingHistory>(query, parameters).ToList();

                    if (result == null || !result.Any())
                    {
                        Console.WriteLine("No data returned from the query.");
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (for example, use a logging framework)
                Console.WriteLine("Error executing query: " + ex.Message);
                return new List<FamilyPercentageSharingHistory>();
            }
        }



    }
}