using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace IPMS.Web.Controllers.MVC
{
    public class PortInformationController : IpmsBaseController
    {
        //
        // GET: /PortInformation/

        [Route("PortInformation")]
        public ActionResult PortInformationMaster()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("PortInformationMaster", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [AllowAnonymous]
        [Route("PortInformationMaster")]
        public ActionResult PortInformation()
        {
            return View(privilege);
        }
    }
}