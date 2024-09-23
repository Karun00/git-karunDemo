using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselArrestImmobilizationSAMSAStopController : IpmsBaseController
    {
        //
        // GET: /VesselArrestImmobilizationSAMSAStop/

        [Route("VesselArrestImmobilizationSAMSAStop")]
        public ActionResult VesselArrestImmobilizationSAMSAStop()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("VesselArrestImmobilizationSAMSAStop", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}