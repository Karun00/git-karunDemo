using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ModuleController : IpmsBaseController
    {
        //
        // GET: /Module/
        [Route("Modules")]
        public ActionResult ModuleMaster()
        {
            //return View("ModuleMaster",privilege);


            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ModuleMaster", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
       
	}
}