using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class BerthMaintenanceCompletionController : IpmsBaseController
    {
    
        [Route("BerthMaintenanceCompletion/{id?}")]
        public ActionResult BerthMaintenanceCompletion(string id)
        {
            ViewBag.ID = id;
            return View("BerthMaintenanceCompletion", privilege);
        }

    }
}