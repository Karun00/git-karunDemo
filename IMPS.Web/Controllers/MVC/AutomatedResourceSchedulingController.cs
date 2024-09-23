using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class AutomatedResourceSchedulingController : IpmsBaseController
    {
        //
        // GET: /AutomatedResourceScheduling/
        [Route("AutomatedResourceAllocation")]
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("AutomatedResourceScheduling", privilege);
            }
            else
            {
                //return View("BerthMaster", privilege);
                return View("NotFound");
            }
        }
    }
}