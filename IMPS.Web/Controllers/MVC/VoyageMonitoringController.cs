using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VoyageMonitoringController : IpmsBaseController
    {
        public ActionResult ManageVoyageMonitoring()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageVoyageMonitoring", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Route("VoyageMonitoring/ManageVoyageMonitoring/{vcn?}")]
        public ActionResult ManageVoyageMonitoring(string vcn)
        {
            ViewBag.VCN = vcn;
            if (!string.IsNullOrWhiteSpace(privilege.Privileges))
            {
                return View("ManageVoyageMonitoring", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}