using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class WegoDashBoardController : IpmsBaseController
    {
        [Authorize]
        [Route("WegoDashBoard")]
        public ActionResult WegoDashBoard()
        {

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("WegoDashBoard", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
        //Total Movements Dashboard
        [Authorize]
        [Route("Report/TotalMovementsDashboard")]
        public ActionResult TotalMovementsDashboard()
        {

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("TotalMovementsDashboard", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }



    }
}