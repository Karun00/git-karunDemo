using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace IPMS.Web.Controllers
{
    public class ElectronicNotificationsController : IpmsBaseController
    {
        [Route("ElectronicNotifications")]
        public ActionResult ManageElectronicNotifications()
        {
            //return View("ManageElectronicNotifications", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageElectronicNotifications", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }
    }
}