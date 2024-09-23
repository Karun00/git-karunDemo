using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class WegoBerthDashBoardController : IpmsBaseController
    {
        [Authorize]
        [Route("WegoBerthDashBoard")]
        public ActionResult WegoBerthDashBoard()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("WegoBerthDashBoard", privilege);
            }
            else
            {
                return View("NotFound");
            }            
        }
    }
}