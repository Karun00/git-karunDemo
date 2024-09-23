using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ExternalDivingRegisterController : IpmsBaseController
    {
        //
        // GET: /ExternalDivingRegister/

        [Authorize]
        [Route("ExternalDivingRegisters")]
        public ActionResult ExternalDivingRegister()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ExternalDivingRegister", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}