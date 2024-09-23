using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ServiceRequestController : IpmsBaseController
    {
        //
        // GET: /ServiceRequest/
        [Authorize]
        [Route("ServiceRequests/{id?}")]
        public ActionResult ManageServiceRequest(string id)
        {

            ViewBag.ID = id;

            //return View("ManageServiceRequest", privilege);
               return View("ManageServiceRequest", privilege);
        }
        [Authorize]
        [Route("ServiceRequests")]
        public ActionResult ManageServiceRequest()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageServiceRequest", privilege);
                
            }
            else
            {
                return View("NotFound"); 
            }
             
        }

        [Authorize]
        [Route("MServiceRequests")]
        public ActionResult MDepartureNotice()
        {
            if (!string.IsNullOrWhiteSpace(privilege.Privileges))
            {
                return View("MServiceRequest", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}