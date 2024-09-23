using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class MarineRevenueController : IpmsBaseController
    {
        //
        // GET: /MarineRevenue/
        [Route("MarineRevenue")]
        public ActionResult MarineRevenue()
        {
            //return View();

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("MarineRevenue", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
    }
}