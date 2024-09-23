using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ResourceAllocationController : IpmsBaseController
    {
        [Authorize]
        [Route("ServiceRecordings")]
        public ActionResult ManageResourceAllocation()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageResourceAllocation", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}