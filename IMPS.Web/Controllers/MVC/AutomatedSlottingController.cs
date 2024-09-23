using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class AutomatedSlottingController : IpmsBaseController
    {
        //
        // GET: /AutomatedSlotting/

        public ActionResult Index()
        {
            return View("AutomatedSlotting", privilege);
        }
        [Route("AutomatedSlotting")]
        public ActionResult AutomatedSlot()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View(privilege);
            }
            else
            {
                //return View("BerthMaster", privilege);
                return View("NotFound");
            }
        }
    }
}