using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselAgentChangeController : IpmsBaseController
    {
        //
        // GET: /VesselAgentChange/
        [Route("VesselAgentChange")]
        public ActionResult Index()
        {
           // return View("VesselAgentChange",privilege);


            if (!string.IsNullOrWhiteSpace( privilege.Privileges))
            {
                return View("VesselAgentChange", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }

        /// <summary>
        /// To view change agent request screen
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Route("VesselAgentChanges/{vcn?}")]
        [HttpGet]
        public ActionResult ManageVesselAgentChangeRequest(string vcn)
        {
            ViewBag.VCN = vcn;
           // return View("ManageVesselAgentChangeRequest",privilege);


            if (!string.IsNullOrWhiteSpace(privilege.Privileges))
            {
                return View("ManageVesselAgentChangeRequest", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
	}
}