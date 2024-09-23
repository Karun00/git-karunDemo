using System.Web.Mvc;
using System;
namespace IPMS.Web.Controllers.MVC
{
    public class CraftReminderConfigController : IpmsBaseController
    {
        [Route("CraftsReminderConfig")]
        public ActionResult ManageCraftsReminderConfig()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageCraftsReminderConfig", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }

        [Authorize]
        [Route("CraftsReminderConfig/{craftreminderconfigID?}")]
        public ActionResult ManageCraftsReminderConfig(int craftreminderconfigID)
        {

            ViewBag.craftreminderconfigID = craftreminderconfigID;

            //return View("ManageServiceRequest", privilege);
            return View("ManageCraftsReminderConfig");
        }
    }
}