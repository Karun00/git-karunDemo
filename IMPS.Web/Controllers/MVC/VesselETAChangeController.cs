using System.Globalization;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselETAChangeController : IpmsBaseController
    {
        [Route("ChangeETA/{vcn?}")]
        public ActionResult ChangeETA(string vcn, string id)
        {
            ViewBag.VCN = vcn;
            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.VesselETAChangeID = int.Parse(id, CultureInfo.InvariantCulture);
            }
            else
            {
                ViewBag.VesselETAChangeID = 0;
            }

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ChangeETA", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}