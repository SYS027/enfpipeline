using ENF_NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDF.Web.Models
{
    public class FamilyMemberDetails
    {

        public int FamilyId { get; set; }
        public long EmployeeId { get; set; }
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public decimal AnnualIncome { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string EmailSendStatus { get; set; }
        public string IsActive { get; set; }
        public DateTime? DeActiveDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string RelationshipCode { get; set; }
        public int Age { get; set; }
        public string Action { get; set; }
        public string nomination_category_code { get; set; }
        public string relationship_code { get; set; }
        public string relationship_name { get; set; }
        public int CountryId { get; set; }
        public int pecentage { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
        public string AddressIsActive { get; set; }
        public DateTime? AddressDeActiveDate { get; set; }
        public int AddressCreatedBy { get; set; }
        public DateTime AddressCreatedDate { get; set; }
        public DateTime? AddressUpdatedDate { get; set; }
        public string AddressAction { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }

    //public class EmployeeFullDetail
    //{
    //    public string NominationCategoryCode { get; set; }
    //    public string NominationCategoryName { get; set; }
    //    public List<FamilyDetail> FamilyDetails { get; set; }
    //}

    //public class FamilyDetail
    //{
    //    public string RelationshipCode { get; set; }
    //    public string RelationshipName { get; set; }
    //    public string FullName { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //    public int FamilyId { get; set; }
    //    public decimal? ShareOfPercentage { get; set; }
    //}

    public class EmployeeFullDetail
    {
        public string NominationCategoryCode { get; set; }
        public string NominationCategoryName { get; set; }
        public List<FamilyDetail> FamilyDetails { get; set; }
    }

    public class FamilyDetail
    {
        public string RelationshipCode { get; set; }
        public string RelationshipName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int FamilyId { get; set; }
        public decimal? ShareOfPercentage { get; set; }
        public string pd_is_active { get; set; }
        public string perc_is_active { get; set; }
        public string guardian_name { get; set; }
    }


    public class FamilyDetailsND
    {
        public string Relationship_Code { get; set; }
        public string Relationship_Name { get; set; }
        public string Full_Name { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public int Family_Id { get; set; }
        public decimal? Share_Of_Percentage { get; set; }
    }

    public class EmployeeFullDetails
    {
        public string NominationCategoryCode { get; set; }
        public string NominationCategoryName { get; set; }
        public List<FamilyDetailsND> FamilyDetails { get; set; }
    }

    public class FamilyPercentageSharingHistory
    {
        public int Family_Id { get; set; }
        public int Employee_Id { get; set; }
        public string Nomination_Category_Code { get; set; }
        public string Relationship_Code { get; set; }
        public decimal Share_Of_Percentage { get; set; }
        public char Is_Active { get; set; }
        public DateTime? De_Active_Date { get; set; }
        public int? Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public int? Deleted_By { get; set; }
        public DateTime? Deleted_Date { get; set; }
    }

    public class NomineeDetailsViewModel
    {
        public List<EmployeeFullDetail> EmployeeFamilyDetails { get; set; }
        public List<FamilyPercentageSharingHistory> DeletedRecords { get; set; }
    }


}