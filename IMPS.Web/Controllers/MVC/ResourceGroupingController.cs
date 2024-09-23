using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ResourceGroupingController : IpmsBaseController
    {
        //
        // GET: // ResourceGrouping

        [Route("ResourceGrouping")]
        public ActionResult ManageResourceGrouping()
        {
            //To Check Page Privilege and Permission to access
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageResourceGrouping", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}