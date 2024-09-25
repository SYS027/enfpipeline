using System.Web;
using System.Web.Optimization;

namespace EDF.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                     "~/Scripts/angular.min.js",
                     "~/Scripts/angular-animate.min.js",
                     "~/Scripts/toaster.js",
                     "~/Assets/app.js",
                      "~/Scripts/ng/angular-validator.js",
                     "~/Scripts/ng/angular-validator-rules.js",
                     "~/Scripts/ng/ui-bootstrap-tpls-2.1.3.js",
                     "~/Assets/menu.js",
                     "~/Assets/common.js",
                     "~/Assets/fileUploadStatus.js",
                     "~/Assets/scheme.js",
                     "~/Assets/user.js",
                     "~/Assets/role.js",
                     "~/Assets/empSchmMapping.js",
                     "~/Assets/roleMenuMapping.js",
                     "~/Assets/report.js",
                      "~/Assets/karvyreport.js",
                       "~/Assets/hgsreport.js",
                     "~/Assets/FileUpload.js",
                     "~/Assets/AddNewItAsset.js",
                     "~/Assets/ReportDisposal.js",
                     "~/Assets/IncumbentEmployee.js",
                     "~/Assets/Employee.js",
                     "~/Assets/UploadITAssets.js",
                      "~/Assets/UploadAMCITAssets.js",
                     "~/Assets/EditITAsset.js",
                      "~/Assets/CertificationITAssets.js",
                       "~/Assets/ITAssets.js",
                       "~/Assets/ITAssetsAssign.js",
                       "~/Assets/ITAssetsReceipt.js",
                       "~/Assets/ITAssetsSurrender.js",
                       "~/Assets/LogReport.js",
                     "~/Assets/addFixedAssets.js",
                     "~/Assets/uploadFixedAssets.js",
                     "~/Assets/certificationOfFixedAssets.js",
                     "~/Assets/subsidiaryDeadStock.js",
                     "~/Assets/faQueryModule.js",
                     "~/Assets/CertifiedITAssetsReport.js",
                       "~/Assets/DisposalITAssets.js",
                       "~/Assets/acceptSurrenderItAssets.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

        }
    }
}
