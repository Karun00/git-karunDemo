using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class PortGeneralConfigsController : IpmsBaseController
    {
        //
        // GET: /PortGeneralConfigs/
        [Route("PortGeneralConfigurations")]
        public ActionResult PortGeneralConfigs()
        {
            //return View("PortGeneralConfigs", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("PortGeneralConfigs", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }
	}
}