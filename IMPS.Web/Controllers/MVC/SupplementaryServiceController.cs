using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SupplementaryServiceController : IpmsBaseController
    {
        //
        // GET: /SupplementaryServiceRequest/
        [Route("SupplementaryServiceRequests")]
        public ActionResult SupplementaryServiceRequest()
        {
           // return View("SupplementaryServiceRequest",privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("SupplementaryServiceRequest", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
        [Route("SupplementaryServiceRequests/{SuppServiceRequestID?}")]
       // [Route("SupplementaryServiceRequest/SupplementaryServiceRequests/{SuppServiceRequestID?}")]
        public ActionResult GetSupplementaryServiceRequestView(string suppServiceRequestID)
        {
            ViewBag.SuppServiceRequestID = suppServiceRequestID;
           // return View("SupplementaryServiceRequest",privilege);
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("SupplementaryServiceRequest", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }


        [Route("SupplementaryServiceExtensions")]
        public ActionResult SupplementaryServiceExtension()
        {
            return View();
        }
	}
}