using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselRegistrationController : IpmsBaseController
    {
        //
        // GET: /VesselRegistration/
        /// <summary>
        /// To View Vessel Registration Screen
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("VesselRegistration")]
        [HttpGet]
        public ActionResult Index()
        {
            //return View("VesselRegistration", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("VesselRegistration", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

        /// <summary>
        /// To View vessel registration screen
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Route("VesselRegistration/{vcn?}")]
        public ActionResult VesselRegistration(string vcn)
        {
            ViewBag.IMONo = vcn;
           // return View("VesselRegistration",privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("VesselRegistration", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

	}
}