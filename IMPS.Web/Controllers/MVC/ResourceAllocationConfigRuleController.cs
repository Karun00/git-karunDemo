using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ResourceAllocationConfigRuleController : IpmsBaseController
    {
        //
        // GET: /ResourceAllocationConfigRule/

        [Route("ResourceAllocationConfigRule")]
        public ActionResult ResourceAllocationConfigRule()
        {
            //To Check Page Privilege and Permission to access
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ResourceAllocationConfigRule", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}