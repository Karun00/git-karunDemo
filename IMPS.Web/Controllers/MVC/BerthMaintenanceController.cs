using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class BerthMaintenanceController : IpmsBaseController
    {      

        [Route("BerthMaintenances/{id?}")]
        public ActionResult BerthMaintenance(string id)
        {
            ViewBag.ID = id;
            return View("BerthMaintenance", privilege);
        }

	}
}