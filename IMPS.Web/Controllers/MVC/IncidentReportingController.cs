using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class IncidentReportingController : IpmsBaseController
    {
        [Authorize]
        [Route("IncidentReporting")]
        public ActionResult IncidentReporting()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {

                return View("IncidentReporting", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}