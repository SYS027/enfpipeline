using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDF.Web.DataAccessLayer
{
    public class GetPGQuery
    {
        public static string KCMS_SP_ADD_UPDATE_SCHEME_MASTER
        {
            get
            {
                return @"select * from kcms_sp_add_update_scheme_master
                                                  ( 
                                                 :p_id,:p_schm_code,:p_schm_name,:p_schm_category_id,:p_schm_category_plan_id,:p_option_id,:p_scheme_type_id,
                                                 :p_equity_percentage,:p_debt_percentage,:p_login_user,:p_hybride_flag,:p_product_id
                                                  )";
            }
        }
        public static string KCMS_SP_ADD_UPDATE_EMPLOYEE_MASTER
        {
            get
            {
                return @"select * from kcms_sp_add_update_employee_master
                                                (
                                                :p_id,:p_emp_code,:p_emp_name,:p_designation_id,:p_department_id,:p_folio_no,:p_date_of_appointment
                                                ,:p_date_of_superannuation,:p_pan_no,:p_login_user
                                                )";
            }
        }
        public static string QUERY_SP_UPLOAD_ITASSET_DATA
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_data
                                                (
                                                 :p_id
                                                ,:p_asset_number
                                                ,:p_alternate_number
                                                ,:p_asset_location
                                                ,:p_asset_type
                                                ,:p_asset_model
                                                ,:p_make
                                                ,:p_vendor_id
                                                ,:p_description
                                                ,:p_accessories_name
                                                ,:p_po_number
                                                ,:p_procedurement_year
                                                ,:p_po_date
                                                ,:p_department_id
                                                ,:p_warr_expiry_date
                                                ,:p_installation_date
                                                ,:p_delivery_date
                                                ,:p_movable
                                                ,:p_company_cd
                                                ,:p_cost_of_asset
                                                ,:p_status
                                                ,:p_amc_ems_warr
                                                ,:p_from_date
                                                ,:p_from_to
                                                ,:p_warr_vendor_name
                                                ,:p_empid
                                                ,:p_emp_name
                                                ,:p_date_of_receive
                                                ,:p_date_of_return
                                                ,:p_issue_status
                                                ,:p_from_date_warr
                                                ,:p_to_date_warr
                                                ,:p_warr_vendor_id
                                                ,:p_from_date_amc
                                                ,:p_to_date_amc
                                                ,:p_amc_vendor_id
                                                ,:p_buy_back_cost
                                                ,:p_file_name
                                                )";
            }
        }
        public static string ITASSET_SP_ADD_UPDATE_ITASSET_MASTER
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_master
                                                (
                                                 :p_id
                                                ,:p_asset_number
                                                ,:p_alternate_number
                                                ,:p_asset_location
                                                ,:p_asset_type
                                                ,:p_asset_model
                                                ,:p_make
                                                ,:p_vendor_id
                                                ,:p_description
                                                ,:p_accessories_name
                                                ,:p_po_number
                                                ,:p_procedurement_year
                                                ,:p_po_date
                                                ,:p_department_id
                                                ,:p_warr_expiry_date
                                                ,:p_installation_date
                                                ,:p_delivery_date
                                                ,:p_movable
                                                ,:p_company_cd
                                                ,:p_cost_of_asset
                                                ,:p_status
                                                ,:p_amc_ems_warr
                                                ,:p_from_date
                                                ,:p_from_to
                                                ,:p_warr_vendor_name
                                                ,:p_empid
                                                ,:p_emp_name
                                                ,:p_date_of_receive
                                                ,:p_date_of_return
                                                ,:p_issue_status
                                                ,:p_from_date_warr
                                                ,:p_to_date_warr
                                                ,:p_warr_vendor_id
                                                ,:p_from_date_amc
                                                ,:p_to_date_amc
                                                ,:p_amc_vendor_id
                                                ,:p_buy_back_cost
                                                )";
            }
        }

        public static string ITASSET_SP_ADD_UPDATE_ITASSET_AMC_Warr
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_AMC_Warr
                                                (:p_asset_id,:p_amc_vendor_id,:p_from_date_amc,:p_to_date_amc
                                                ,:p_warr_vendor_id,:p_from_date_warr,:p_to_date_warr,:p_modify_by,:P_file_name)";
            }
        }
        public static string ITASSET_SP_ADD_UPDATE_ITASSET_Assign_to_Emp
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_Assign_to_Emp
                                                (
                                                :p_id,:p_asset_id,:p_asset_no,:p_empid,:p_emp_name,:p_date_of_allotment
                                                ,:p_issue_status,:p_created_by,:p_modify_by,:p_utc_code,:p_company_id,:p_location_code
                                                )";
            }
        }
        public static string ITASSET_SP_DATA_FOR_ACCEPTANCE_AND_SURRENDER
        {
            get
            {
                return @"select * from ia_sp_get_data_for_accept_and_surrender(:p_login_user,:p_role_code,:p_location_code)";
            }
        }
        public static string ITASSET_SP_ADD_UPDATE_ITASSET_Assign_to_Emp_receipt
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_Assign_to_Emp_receipt
                                                (
                                                :p_id,:p_asset_id,:p_empid,:p_emp_name,:p_date_of_receive
                                                ,:p_issue_status,:p_created_by,:p_modify_by,:p_utc_code,:p_company_id
                                                )";
            }
        }
        public static string ITASSET_SP_ADD_UPDATE_ITASSET_Assign_to_Emp_surrender
        {
            get
            {
                return @"select * from itasset_sp_add_update_itasset_Assign_to_Emp_surrender
                                                (
                                                :p_id,:p_asset_id,:p_empid,:p_emp_name,:p_date_of_return
                                                ,:p_issue_status,:p_created_by,:p_modify_by,:p_utc_code,:p_company_id
                                                )";
            }
        }
        public static string KCMS_SP_ADD_UPDATE_EMPLOYEE_SCHEME_MAPPING
        {
            get
            {
                return @"select * from kcms_sp_add_update_employee_scheme_mapping
                                                   (
                                                     :p_id,:p_schm_emp_mapp_audit_id,:p_employee_id,:p_scheme_id,:p_schm_substitute_id,:p_schm_category_id,:p_weightage,:p_role_id,:p_login_user
                                                   )";
            }
        }
        public static string SCHEME_DATA_ID
        {
            get
            {
                return @"select * from kcms_tbl_scheme_master where id=@p_id";
            }
        }
        public static string EMPLOYEE_DATA_ID
        {
            get
            {
                return @"select * from kcms_tbl_employee_master where id=@p_id";
            }
        }
        public static string ITASSET_DATA_BY_ID
        {
            get
            {
                return @"select 
                            	a1.*,a2.*
								,ed.email as emp_email
                            	,'prakash.dewoolkar@uti.co.in,Ruchita.Kokate@uti.co.in,Hareshbhai.Surti@uti.co.in,shiva.sethi@uti.co.in' as additional_email_ids
                            from itasset_tbl_itasset_master a1
                            left join itasset_tbl_itasset_assign_to_emp a2 on a2.asset_id=a1.id
                            left join uti_employee_details ed on ed.employeecode=a2.employee_code
                            where a1.id=@p_id";
            }
        }
        public static string ITASSET_AMC_Warr_DATA_BY_AssetID
        {
            get
            {
                return @"select * from itasset_tbl_itasset_AMC_Warr where asset_id=@p_id";
            }
        }
        public static string ITASSET_Allot_DATA_BY_AssetID
        {
            get
            {
                return @"select * from itasset_tbl_itasset_Assign_to_Emp where asset_id=@p_id";
            }
        }
        public static string ITASSET_receipt_DATA_BY_AssetID
        {
            get
            {
                return @"select * from itasset_tbl_itasset_Assign_to_Emp_receipt where asset_id=@p_id";
            }
        }
        public static string ITASSET_surrender_DATA_BY_AssetID
        {
            get
            {
                return @"select * from itasset_tbl_itasset_Assign_to_Emp_surrender where asset_id=@p_id";
            }
        }
        public static string ITASSET_DATA_BY_SearchText
        {
            //get
            //{
            //    return @"select * from itasset_tbl_itasset_master where asset_location like '@p_serachtext%' or employee_name like '@p_serachtext%'";
            //}
            get
            {
                return @"select * from itasset_tbl_itasset_master where employee_name like '@p_serachtext%' or asset_location like '@p_serachtext%'";
            }
        }
        public static string EMPLOYEE_DATA_EMPLOYEE_CODE
        {
            get
            {
                return @"select * from kcms_tbl_employee_master where emp_code=@p_emp_code";
            }
        }
        public static string SCHEME_DATA_SCHEME_CODE
        {
            get
            {
                return @"select * from kcms_tbl_scheme_master where schm_code=@p_schm_code";
            }
        }
        public static string EMPLOYEE_SCHEME_MAPPING_DATA_TOCHECK
        {
            get
            {
                return @"select * from kcms_tbl_schm_emp_mapping where employee_id=@p_employee_id and schm_category_id=@p_scheme_category_id and scheme_id=@p_scheme_id";
            }
        }
        public static string DELETE_EMPLOYEE_SCHEME_MAPPING_DATA
        {
            get
            {
                return @"delete from kcms_tbl_schm_emp_mapping where employee_id=@p_employee_id and schm_category_id=@p_scheme_category_id";
            }
        }
        public static string ADD_UPDATE_SCHM_EMP_MAPPING_AUDIT
        {
            get
            {
                return "select * from kcms_sp_add_update_employee_scheme_mapping_audit(:p_id,:p_employee_id,:p_login_user)";
            }
        }
        public static string DELETE_SCHM_EMP_MAPPING_AUDIT
        {
            get
            {
                return "select * from kcms_sp_delete_employee_scheme_mapping_audit(:p_id)";
            }
        }
        public static string EMPLOYEE_SCHEME_MAPPING_DATA_ID
        {
            get
            {
                return @"select * from kcms_sp_get_emp_schm_mapping_data_byid(:p_id)";
            }
        }
        public static string GET_SCHEME_ID_FROM_SCHM_MASTER
        {
            get
            {
                return @"select * from kcms_sp_get_all_schmid_by_categoryid(:p_schm_category_id)";
            }
        }
        public static string GET_department_MASTER
        {
            get
            {
                return @"select * from itasset_sp_get_department()";
            }
        }
        public static string GET_department_bylocation_MASTER
        {
            get
            {
                return @"select * from itasset_sp_get_department_by_locationname(:p_location_name)";
            }
        }
        public static string ITASSET_SP_DELETE_ITASSET
        {
            get
            {
                return @"select * from itasset_sp_delete_itasset(:p_id)";
            }
        }
        public static string IA_SP_GET_DATA_FOR_ALLOTMENT
        {
            get
            {
                return @"select 
                            m.id
	                    	, e.employee_code
	                    	, e.employee_name
	                    	, e.utc_code
	                    	, asset_type
	                    	, asset_number
	                    	, alternate_number
	                    	, asset_model
	                    	,(
								select distinct attribute_type_unit_desc from uti_emp_tbl_attributes_info 
								where attribute_type_code='Location Code' 
								and attribute_type_unit_code=asset_location
							) as asset_location
							,m.asset_location as location_code
	                    	, e.date_of_allotment
	                    	,e.company_id as company_cd

                        from itasset_tbl_itasset_master m
                        left join itasset_tbl_itasset_assign_to_emp e on m.id = e.asset_id
                        where e.is_accepted is null or e.is_accepted = false";
            }
        }
        public static string GET_Movable_ITAssetsList
        {
            get
            {
                return @"select * from itasset_sp_get_ITAssets_MovableList(:p_search_txt,:p_movable_txt)";
            }
        }
        #region ITAssets Offered start
        public static string ITASSET_Issued_Details_BY_EMPID
        {
            get
            {
                return @"select * from itasset_sp_issued_details_by_empid(:p_emp_code)";
            }
        }
        public static string ITASSET_offered_Details_BY_EMPID
        {
            get
            {
                return @"select * from itasset_sp_offered_details_by_empid(:p_emp_code)";
            }
        }
        public static string ITASSET_offered_Details_BY_EMPID_CHECK
        {
            get
            {
                return @"select * from itasset_sp_offered_details_by_empid_check(:p_emp_code)";
            }
        }
        public static string ITASSET_SP_OFFERING_TO_EMP
        {
            get
            {
                return @"select * from itasset_sp_offering_to_emp(:p_asset_id,:p_emp_code,:p_payment_date,:p_remarks,:p_created_by)";
            }
        }
        public static string ITASSET_offered_Details_BY_EMPID_AssetNo_emplogin
        {
            get
            {
                return @"select * from itasset_sp_offered_details_by_empid_assetno_on_emplogin(:p_EmpId,:p_role_code,:p_user_location,:p_is_employee)";
            }
        }
        public static string Offered_ITASSET_DATA_BY_ID_ON_EMPLOGIN
        {
            get
            {
                return @"select * from itasset_sp_offered_details_by_empid_assetid_on_emplogin(:p_asset_id,:p_empid,:p_role_code,:p_user_location)";
            }
        }
        public static string ITASSET_SP_OFFERED_RESPONSE_BY_EMP
        {
            get
            {
                return @"select * from itasset_sp_offered_response_by_emp(:p_asset_id,:p_asset_no,:p_emp_id,:p_response,:p_modified_by,:p_date_of_deposit,:p_employeeid_iv,:p_date_of_deposit_iv,:p_iv_remark,:p_payment_slip)";
            }
        }

        public static string ITASSET_DATA_BY_ASSET_NUMBWE
        {
            get
            {
                return @"select * from itasset_tbl_itasset_master where asset_number=@p_asset_number";
            }
        }

        public static string ITASSET_offered_no_response_list
        {
            get
            {
                return @"select * from itasset_sp_offered_no_response_list(:p_search_txt)";
            }
        }
        public static string ITASSET_offered_no_response_ewaste_dealer_list
        {
            get
            {
                return @"select * from itasset_sp_offered_no_response_ewaste_dealer_list()";
            }
        }
        public static string ITASSET_offered_no_response_donate_list
        {
            get
            {
                return @"select * from itasset_sp_offered_no_response_donate_list(:p_search_txt)";
            }
        }
        public static string ITASSET_SP_OFFERING_TO_OTHERS
        {
            get
            {
                return @"select * from itasset_sp_offering_to_other(:p_offer_to,:p_asset_id,:p_emp_code,:p_payment_date,:p_remarks,:p_response,:p_created_by)";
            }
        }
        #endregion ITAssets Offered end
    }
}