using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppMiscServiceController : Controller
    {
        //
        // GET: /SuppDryDock/

        [Route("SuppMiscService")]
        public ActionResult SuppMiscService()
        {
            return View();
        }
    }
}