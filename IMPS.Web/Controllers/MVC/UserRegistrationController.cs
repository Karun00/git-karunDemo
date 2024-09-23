using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class UserRegistrationController : IpmsBaseController
    {
        //
        // GET: /UserRegistration/
          [AllowAnonymous]
        [Route("UserRegistration")]
        public ActionResult UserRegistration(int applicantId = 0)
        {
            ViewBag.ApplicantId = applicantId;
            return View();
        }

    }
}