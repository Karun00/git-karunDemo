using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItextSharpDoc = iTextSharp.text;
using IPMS.Domain;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppDockUnDockTimeController : IpmsBaseController
    {
        //
        // GET: /SuppDockUnDockTime/
       

        [Route("SuppDockUnDockTimes")]
        public ActionResult SuppDockUnDockTime()
        {
           // return View("SuppDockUnDockTime", privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("SuppDockUnDockTime", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
	}
}