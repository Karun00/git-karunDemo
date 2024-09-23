using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class DepartureNoticeController : IpmsBaseController
    {
        //
        // GET: /DepartureNotice/

        [Authorize]
        [Route("DepartureNotice")]
        public ActionResult DepartureNotice()
        {
            if (!string.IsNullOrWhiteSpace(privilege.Privileges))
            {
                return View("DepartureNotice", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Authorize]
        [Route("DepartureNotice/{DepartureID?}")]
        public ActionResult DepartureNotice(string DepartureID)
        {
            if (DepartureID == "MDepartureNotice")
            {
                if (!string.IsNullOrWhiteSpace(privilege.Privileges))
                {
                    return View("MDepartureNotice", privilege);
                }
                else
                {
                    return View("NotFound");
                }
            }
            else {
                ViewBag.DepartureID = DepartureID;

                if (!string.IsNullOrWhiteSpace(privilege.Privileges))
                {
                    return View("DepartureNotice", privilege);
                }
                else
                {
                    return View("NotFound");
                }
            
            }
        }

        [Authorize]
        [Route("MDepartureNotice")]
        public ActionResult MDepartureNotice()
        {
            if (!string.IsNullOrWhiteSpace(privilege.Privileges))
            {
                return View("MDepartureNotice", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}