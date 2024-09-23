using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ServiceTypeDesignationController : IpmsBaseController
    {
        //
        // GET: /ServiceTypeDesignation/

        [Authorize]
        [Route("ServiceTypeDesignation")]
        public ActionResult ServiceTypeDesignation()
        {
            //return View("ServiceTypeDesignation", privilege);
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ServiceTypeDesignation", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
    }
}