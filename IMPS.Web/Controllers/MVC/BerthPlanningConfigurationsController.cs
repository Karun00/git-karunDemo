using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class BerthPlanningConfigurationsController : IpmsBaseController
    {
        //
        // GET: /BerthPlanningConfigurations/
        [Authorize]
        [Route("BerthPlanningConfigurations")]
        public ActionResult Index()
        {
            return View("BerthPlanningConfigurations", privilege);
        }
	}
}