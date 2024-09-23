using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class CraftOutOfCommissionController : IpmsBaseController
    {
        //
        // GET: /CraftOutOfCommission/

        [Authorize]
        [Route("CraftOutOfCommission")]
        public ActionResult Index()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("CraftOutOfCommission", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Authorize]
        [Route("CraftInCommission")]
        public ActionResult CraftInCommission()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("CraftInCommission", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}