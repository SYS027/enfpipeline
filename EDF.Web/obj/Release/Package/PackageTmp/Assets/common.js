app.factory("CommonFactory", ["$rootScope", "$http", "$q", function ($rootScope, $http, $q) {
    this.loadDropDownList = function (category_name, module_type) {
        return $http.post(('/Common/LoadDropDownList/'), { category_name: category_name, module_type: module_type });
    },
        this.getVendors = function () {
            return $http.get('/Common/GetVendors/');
        },
        this.getSchemeTypes = function () {
            return $http.get('/Common/GetSchemeTypes/');
        },
        this.getSchemeTypes = function () {
            return $http.get('/Common/GetSchemeTypes/');
        },
        this.getSchemeCategories = function () {
            return $http.get('/Common/GetSchemeCategories/');
        },
        this.getSchemeCategoriesPlan = function () {
            return $http.get('/Common/GetSchemeCategoriesPlan/');
        },
        this.getOptions = function () {
            return $http.get('/Common/GetOptions/');
        },
        this.getYearList = function () {
            return $http.get('/Common/GetYearList/');
        },
        this.getMonthList = function () {
            return $http.get('/Common/GetMonthList/');
        },
        this.getEmployees = function () {
            return $http.get('/Common/GetEmployees/');
        },
        this.getallSchemes = function () {
            return $http.post('/Common/GetallSchemes/');
        },
        this.getSchemes = function (scheme_category_id) {
            return $http.post(('/Common/GetSchemes/'), { scheme_category_id: scheme_category_id });
        },
        this.getMenus = function () {
            return $http.get('/Common/GetMenus/');
        },
        this.getSchemeRoles = function () {
            return $http.get('/Common/GetSchemeRoles/');
        },
        this.getRoles = function () {
            return $http.get('/Common/GetRoles/');
        },
        this.getReportAccess = function () {
            return $http.get('/Common/GetReportAccess/');
        },
        this.getDesignations = function () {
            return $http.get('/Common/GetDesignations/');
        },
        this.getDepartments = function () {
            return $http.get('/Common/GetDepartments/');
        },
        this.getfiles = function (from_date, to_date) {
            return $http.post(('/Common/GetEmailFiles/'), { from_date: from_date.toJSON(), to_date: to_date.toJSON() });
        },
        // binding for fixed assets
        this.asset_types = function () {
            return $http.get("/Common/GetFixedAssetTypes")
        },
        this.get_vendors = function () {
            return $http.get("/Common/GetFixedAssetVendors")
        },
        this.get_departments = function () {
            return $http.get("/Common/GetFixedAssetDepartments")
        },
        this.get_status = function () {
            return $http.get("/Common/GetFixedAssetStatus")
        },
        this.get_locationByRole = function () {
            return $http.post("/Common/GetLocationsByRole")
        },
        this.get_employeeByLocation = function () {
            return $http.post("/Common/GetLocationWiseEmployee")
        },
        this.get_FA_Remark = function () {
            return $http.post(("/Common/LoadDropDownList"), { category_name: '#FA_CRTREMK#', module_type: 'FA' })
        },
        this.get_iv_employees = function () {
            return $http.post("/Common/GetClassIV_Employees")
        },
        this.getITStatusList = function (asset_id) {
            return $http.post(("/Common/GetITStatusList"), { asset_id: asset_id })
        }
    // end of fixed assets binding
    return this;
}]);