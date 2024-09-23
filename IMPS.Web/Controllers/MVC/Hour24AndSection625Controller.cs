using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class Hour24AndSection625Controller : IpmsBaseController
    {
        //
        // GET: /Hour24AndSection625/
          [AllowAnonymous]
            [Route("Hour24AndSection625")]
        public ActionResult Hour24AndSection625()
        {
            return View();
        }
           [Authorize]
            [Route("Hour24AndSection625List")]
            public ActionResult Hour24AndSection625List()
            {
                //if (privilege.Privileges != "")
                if (!string.IsNullOrEmpty(privilege.Privileges))
                {
                    return View("Hour24AndSection625List", privilege);
                }
                else
                {
                    //return View("BerthMaster", privilege);
                    return View("NotFound");
                } 
            }
	}
}