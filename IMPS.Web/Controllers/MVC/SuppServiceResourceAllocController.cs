using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppServiceResourceAllocController : IpmsBaseController
    {
        //
        // GET: /SuppServiceResourceAlloc/
        //[Route("SuppServiceResourceAllocation")]
        [Authorize]
        [Route("ResourceAllocation")]
        public ActionResult SuppServiceResourceAlloc()
        {
            return View();
        }
	}
}