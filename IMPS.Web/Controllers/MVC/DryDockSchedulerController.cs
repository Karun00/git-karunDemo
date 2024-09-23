using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class DryDockSchedulerController : Controller
    {
        //
        // GET: /DryDockScheduler/
        [Route("DryDockScheduler")]
        public ActionResult DryDockScheduler()
        {
            return View();
        }
	}
}