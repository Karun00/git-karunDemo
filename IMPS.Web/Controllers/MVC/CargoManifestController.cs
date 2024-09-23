using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class CargoManifestController : IpmsBaseController
    {
        //
        // GET: /CargoManifest/

        [Authorize]
        [Route("CargoManifest")]
        public ActionResult CargoManifest()
        {
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("CargoManifest", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}