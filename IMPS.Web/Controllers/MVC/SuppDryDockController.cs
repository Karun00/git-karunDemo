using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppDryDockController : IpmsBaseController
    {
        //
        // GET: /SuppDryDock/

        [Authorize]
        [Route("SuppDryDockApplication/{id?}")]
        public ActionResult SuppDryDockApplication(string id)
        {
            ViewBag.ID = id;
            //return View("SuppDryDockApplication", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("SuppDryDockApplication", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
	}
}