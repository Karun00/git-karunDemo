using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class DockingPlanController : IpmsBaseController
    {
        [Authorize]
        [Route("DockingPlan/{id?}")]
        public ActionResult DockingPlan(string id)
        {
            ViewBag.ID = id;
            return View("DockingPlan", privilege);

            if (privilege.Privileges != "")
            {
                return View("DockingPlan", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

    }
}