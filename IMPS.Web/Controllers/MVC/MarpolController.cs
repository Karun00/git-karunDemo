using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class MarpolController : IpmsBaseController
    {
        [Authorize]
        [Route("Marpol")]
        public ActionResult Marpol()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("Marpol", privilege);

            }
            else
            {                
                return View("NotFound");
            }
        }

    }
}