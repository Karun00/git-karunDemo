using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ServiceTypeController : IpmsBaseController
    {
        //
        // GET: /ServiceType/

        [Authorize]
        [Route("ServiceType")]
        public ActionResult ServiceType()
        {
            //return View("ServiceType", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ServiceType", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
    }
}