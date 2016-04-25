using System.Web.Optimization;

namespace SearchForKnowledge
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/site.css"));


            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-2.2.2.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/Logout.js"));
        }
    }
}