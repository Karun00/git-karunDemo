using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class PlannedMovementsController : IpmsBaseController
    {
        [Route("PlannedMovements")]
        public ActionResult ManagePlannedMovements()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("PlannedMovements/{PortCode?}")]
        public ActionResult GetPlannedMovements(string portCode)
        {
            ViewBag.PortCode = portCode;
            return View("ManagePlannedMovements");
        }
    }
}