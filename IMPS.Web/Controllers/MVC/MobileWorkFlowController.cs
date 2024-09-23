using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class MobileWorkFlowController : Controller
    {
        public ActionResult PendingApprovals()
        {
            return View();
        }      
    }
}