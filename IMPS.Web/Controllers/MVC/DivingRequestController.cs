using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class DivingRequestController : IpmsBaseController
    {
        //
        // GET: /DivingRequest/

        [Route("DivingRequests")]
        public ActionResult ManageDivingRequest()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageDivingRequest", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Route("DivingTaskExecutions")]
        public ActionResult DivingTaskExecution()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("DivingTaskExecution", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Route("DivingRequestOccupations")]
        public ActionResult DivingRequestOccupation()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("DivingRequestOccupation", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Route("DivingRequestOccupations/{DivingRequestID?}")]
        public ActionResult DivingRequestOccupation(string divingrequestid)
        {
            ViewBag.divingrequestid = divingrequestid;

            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("DivingRequestOccupation", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}