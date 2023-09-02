using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LMSPricing.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/admin/bundles/script").Include(
                        "~/Areas/Content/script/jquery.js",
                        "~/Areas/Content/script/bootstrap.min.js",
                        "~/Areas/Content/script/calendar.js",
                        "~/Areas/Content/script/core.js",
                        "~/Areas/Content/script/jquery.Bootstrap-PersianDateTimePicker.js"

                        ));

            bundles.Add(new StyleBundle("~/Areas/Content/css").Include(
                      "~/Areas/Content/style/css/bootstrap.min.css",
                      "~/Areas/Content/style/css/bootstrap-rtl.min.css",
                      "~/Areas/Content/style/css/sb-admin.css",
                      "~/Areas/Content/style/css/sb-admin-rtl.css",
                      "~/Areas/Content/style/css/common.css",
                      "~/Areas/Content/style/css/jquery.Bootstrap-PersianDateTimePicker.css",
                      "~/Areas/Content/style/font-awesome/css/font-awesome.min.css"

                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}