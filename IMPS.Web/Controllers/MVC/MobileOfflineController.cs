using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class MobileOfflineController : Controller
    {
        //
        // GET: /MobileOffline/
        public ActionResult PendingTasks()
        {
            return View();
        }
        public ActionResult Manifest()
        {
            Response.ContentType = "text/cache-manifest";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Cache.SetCacheability(
                System.Web.HttpCacheability.NoCache);
            return View();
        }
	}
}