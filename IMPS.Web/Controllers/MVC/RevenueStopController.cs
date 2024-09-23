using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class RevenueStopController : Controller
    {
        /// <summary>
        /// To View Revenue Stop list screen
        /// </summary>
        /// <returns></returns>
        
        [Authorize]
        [Route("RevenueStop")]
        public ActionResult ManageRevenueStop()
        {
            return View();
        }
    }
}
