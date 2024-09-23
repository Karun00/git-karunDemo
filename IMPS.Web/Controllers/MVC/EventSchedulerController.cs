using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class EventSchedulerController : IpmsBaseController
    {
        //
        // GET: /EventScheduler/
        [Authorize]
        [Route("EventSchedulers")]
        public ActionResult ManageEventScheduler()
        {
            //return View("ManageEventScheduler", privilege);

            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageEventScheduler", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
	}
}