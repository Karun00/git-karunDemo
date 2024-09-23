using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class PortEntryPassApplicationController : Controller
    {
        //
        [Route("PortEntryPassApplication")]
        public ActionResult ManagePortEntryPassApplication()
        {
            return View();
        }
           [Authorize]
        [Route("PortEntryPassApplicationList")]
        public ActionResult ManagePortEntryPassApplicationList()
        {
            return View();
        }
           [Authorize]
        [Route("PortEntryPassApplicationListforSSACheck")]
        public ActionResult ManagePortEntryPassApplicationListforSsacheck()
        {
            return View();
        }
           [Authorize]
        [Route("PortEntryPassApplicationListforSAPSCheck")]
        public ActionResult ManagePortEntryPassApplicationListforSapscheck()
        {
            return View();
        }
           [Authorize]
        [Route("InternalEmployeePermit")]
        public ActionResult ManageInternalEmployeePermit()
        {
            return View();
        }
           [Authorize]
        [Route("InternalEmployeePermitApprove")]
        public ActionResult ManageInternalEmployeePermitApprove()
        {
            return View();
        }

           [Authorize]
        [Route("PermitOffice")]
        public ActionResult ManagePermitOffice()
        {
            return View();
        }
	}
}