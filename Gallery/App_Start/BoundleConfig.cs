using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Gallery
{
    public class BoundleConfig
    {
        public static void RegisterBoundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundless/bootstrap").Include(
                "~/Scripts/boostrap.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"));
        }
    }
}