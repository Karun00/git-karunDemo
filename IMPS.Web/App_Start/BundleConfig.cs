using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace IPMS.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles != null)
            {
                //New bundles from here

                // IPMS Lay Out CSS
                bundles.Add(new StyleBundle("~/CSS/GlobalMandatory").Include(
                    "~/Content/Styles/font-awesome.min.css",
                    "~/Content/Styles/bootstrap.min.css",
                    "~/Content/Styles/uniform.default.css",
                    "~/Content/Styles/datepicker.css",
                    "~/Content/Styles/style-metronic.css",
                    "~/Content/Styles/style.css",
                    "~/Content/Styles/style-responsive.css",
                    "~/Content/Styles/grid-style.css",
                    "~/Content/Styles/plugins.css",
                    "~/Content/Styles/custom.css",
                    "~/Content/Styles/kendo.common.min.css",
                    "~/Content/Styles/kendo.default.min.css",
                    "~/Content/Styles/default.css",
                    "~/Content/Styles/toastr.css",
                    "~/Content/Styles/jquery-ui.css",
                    "~/Content/Styles/jquery.ui.chatbox.css",
                    "~/Content/Styles/jquery.confirm.css",
                    "~/Content/Styles/jquery.fileupload-ui.css"

                    ));

                bundles.Add(new StyleBundle("~/CSS/MobileStyle").Include(
                    "~/Content/Styles/mobile-style.css",
                    "~/Content/Styles/mobile_style.css",
                    "~/Content/Styles/mobile-zebra-dialog.css",
                    "~/Content/Styles/kendo.default.min.css",
                    "~/Content/Styles/kendo.common.min.css",
                    "~/Content/Styles/toastr.css",
                    "~/Content/Styles/toastr.min.css",
                    "~/Content/styles/kendo.common.min.css",
                    "~/Content/styles/kendo.rtl.min.css",
                    "~/Content/styles/kendo.default.min.css",
                    "~/Content/styles/kendo.mobile.all.min.css"
                    ));

                bundles.Add(new ScriptBundle("~/bundles/JqueryPlugin").Include("~/Scripts/Lib/jquery-2.0.3.min.js"));
                bundles.Add(new ScriptBundle("~/bundles/IpmsLayoutCorePlugins").Include(
                    "~/Scripts/Lib/jquery-2.0.3.min.js",
                    "~/Scripts/Lib/jquery-ui.js",
                    "~/Scripts/Lib/jquery-migrate-1.2.1.min.js",
                    "~/Scripts/Lib/bootstrap.min.js",
                    "~/Scripts/Lib/jquery.slimscroll.min.js",
                    "~/Scripts/Lib/jquery.cokie.min.js",
                    "~/Scripts/Lib/jquery.validate.min.js",
                    "~/Scripts/Lib/jquery.bootstrap.wizard.min.js",
                    "~/Scripts/Lib/spinner.min.js",
                    "~/Scripts/Lib/bootstrap-datepicker.js",
                    "~/Scripts/Lib/jquery.dataTables.js",
                    "~/Scripts/Lib/DT_bootstrap.js",
                    "~/Scripts/Lib/bootstrap-maxlength.min.js",
                    "~/Scripts/Lib/bootstrap.touchspin.js",
                    "~/Scripts/Lib/select2.min.js",
                    "~/Scripts/Lib/app.js",
                    "~/Scripts/Lib/form-validation.js",
                    "~/Scripts/Lib/form-wizard.js",
                    "~/Scripts/Lib/form-components.js",
                    "~/Scripts/Lib/grid-script.js",
                    "~/Scripts/Lib/newsTicker.js",
                    "~/Scripts/Lib/kendo.web.min.js",
                    "~/Scripts/Lib/kendo.all.min.js",
                    "~/Scripts/Lib/kendo.sortable.min.js",
                    "~/Scripts/Lib/toastr.js",
                    "~/Scripts/Lib/jquery.confirm.js"));


                bundles.Add(new ScriptBundle("~/bundles/IpmsLayoutPageLevelPlugins").Include(
                    "~/Scripts/Lib/app.js",
                    "~/Scripts/Lib/form-components.js",
                    "~/Scripts/Lib/grid-script.js"));

                bundles.Add(new ScriptBundle("~/bundles/MobilePlugins").Include(
                    "~/Scripts/Lib/jquery-2.0.3.min.js",
                    "~/Scripts/Lib/jquery-migrate-1.2.1.min.js",
                    "~/Scripts/Lib/bootstrap.min.js",
                    "~/Scripts/Lib/jquery.slimscroll.min.js",
                    "~/Scripts/Lib/jquery.cokie.min.js",
                    "~/Scripts/Lib/jquery.validate.min.js",
                    "~/Scripts/Lib/jquery.bootstrap.wizard.min.js",
                    "~/Scripts/Lib/spinner.min.js",
                    "~/Scripts/Lib/bootstrap-datepicker.js",
                    "~/Scripts/Lib/jquery.dataTables.js",
                    "~/Scripts/Lib/DT_bootstrap.js",
                    "~/Scripts/Lib/bootstrap-maxlength.min.js",
                    "~/Scripts/Lib/bootstrap.touchspin.js",
                    "~/Scripts/Lib/select2.min.js",
                    "~/Scripts/Lib/app.js",
                    "~/Scripts/Lib/form-validation.js",
                    "~/Scripts/Lib/form-wizard.js",
                    "~/Scripts/Lib/form-components.js",
                    "~/Scripts/Lib/grid-script.js",
                    "~/Scripts/Lib/newsTicker.js",
                    "~/Scripts/Lib/kendo.web.min.js",
                    "~/Scripts/Lib/kendo.all.min.js",
                    "~/Scripts/Lib/kendo.sortable.min.js",
                    "~/Scripts/Lib/kendo.mobile.min.js",
                    "~/Scripts/Lib/toastr.js",
                    "~/Scripts/Lib/toastr.min.js",
                    "~/Scripts/Lib/mobile-zebra-dialog.js"));



                bundles.Add(new ScriptBundle("~/bundles/CkEditorPlugins").Include(
                    "~/Scripts/Lib/ckeditor.js",
                    "~/Scripts/Lib/config.js",
                    "~/Scripts/Lib/en.js", "~/Scripts/Lib/styles.js"));

                bundles.Add(new ScriptBundle("~/bundles/LoginScripts").Include("~/Scripts/Lib/jquery.blockui.min.js",
                    "~/Scripts/Lib/jquery.uniform.min.js", "~/Scripts/Lib/jquery.backstretch.min.js",
                    "~/Scripts/Lib/login-soft.js"));

                bundles.Add(
                    new ScriptBundle("~/bundles/KnockoutScripts").Include("~/Scripts/Lib/knockout-3.0.0.debug.js",
                        "~/Scripts/Lib/knockout-sortable.min.js",
                        "~/Scripts/Lib/knockout.validation.js",
                        "~/Scripts/Lib/knockout.mapping-latest.js",
                        "~/Scripts/Lib/knockout-kendo.min.js"

                        ));

                //bundles.Add(new ScriptBundle("~/bundles/KnockoutScripts1").Include(
                //   "~/Scripts/Lib/knockout-sortable.min.js",
                //   "~/Scripts/Lib/knockout.validation.js",
                // "~/Scripts/Lib/knockout.mapping-latest.js",
                // "~/Scripts/Lib/knockout-kendo.min.js"

                //   ));



                bundles.Add(new StyleBundle("~/CSS/PageLevel").Include(
                    "~/Content/Styles/jquery.fileupload-ui.css"));
                bundles.Add(new StyleBundle("~/CSS/Theme").Include(
                    "~/Content/Styles/style-metronic.css",
                    "~/Content/Styles/style.css", "~/Content/Styles/grid-style.css",
                    "~/Content/Styles/style-responsive.css", "~/Content/Styles/plugins.css",
                    "~/Content/Styles/default.css", "~/Content/Styles/custom.css",
                    "~/Scripts/Lib/skins/moono/editor.css"));

                bundles.Add(new StyleBundle("~/CSS/ResourceAllocation").Include(
                    "~/Content/Styles/ResourceAllocation.css"));

            }
        }
    }
}