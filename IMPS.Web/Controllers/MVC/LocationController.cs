using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class LocationController : IpmsBaseController
    {
       

        [Authorize]
        [Route("Location")]
        public ActionResult Location()
        {
            //return View("Location", privilege);
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("Location", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
    }
}