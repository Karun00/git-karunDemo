using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class AutomatedSlotConfigurationController : IpmsBaseController
    {
      [Route("AutomatedSlotConfiguration")]
        public ActionResult AutomatedSlotConfigurationMaster()
        {
            return View("AutomatedSlotConfigurationMaster",privilege);
        }
	}
}