using System.Web.Optimization;

namespace SearchForKnowledge
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/site.css")
                .Include("~/content/styles/sweetalert.css"));


            bundles.Add(new ScriptBundle("~/SkriptsS4k")
                .Include("~/content/skriptss4k/jquery-2.2.2.js")
                .Include("~/content/skriptss4k/jquery.validate.js")
                .Include("~/content/skriptss4k/jquery.validate.unobtrusive.js")
                .Include("~/content/skriptss4k/bootstrap.js")
                .Include("~/content/skriptss4k/Logout.js")
                .Include("~/content/skriptss4k/bootstrap.min.js")
                .Include("~/content/skriptss4k/jquery-2.2.2.min.js")
                .Include("~/content/skriptss4k/jquery-2.2.3.min.js")
                .Include("~/content/skriptss4k/ImgZoom.js")
                .Include("~/content/skriptss4k/sweetalert.min.js")
                .Include("~/content/skriptss4k/Search.js"));
        }
    }
}