using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class AutomatedSlotBlockingController : IpmsBaseController
    {
        [Authorize]
        [Route("AutomatedSlotBlocking")]
        public ActionResult AutomatedSlotBlocking() 
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("AutomatedSlotBlocking", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
      
    }
}