using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselCallAnchorageController : IpmsBaseController
    {
        //
        // GET: /VesselCallAnchorage/


        [Route("VesselCallAnchorages")]
        public ActionResult VesselCallAnchorage()
        {
            //return View("VesselCallAnchorage",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("VesselCallAnchorage", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

        [Route("VesselCallAnchorage/VesselCallAnchorage/{vcn?}")]
        public ActionResult VesselCallAnchorage(string vcn)
        {
            ViewBag.VCN = vcn;
            //return View("VesselCallAnchorage",privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("VesselCallAnchorage", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
	}
}