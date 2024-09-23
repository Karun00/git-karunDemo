using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class FuelConsumptionDailyLogController : IpmsBaseController
    {
        //
        // GET: /FuelConsumptionDailyLog/
        [Route("FuelConsumptionDailyLog")]
        public ActionResult FuelConsumptionDailyLog()
        {
            return View();
        }
	}
}