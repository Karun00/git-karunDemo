using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppDryDockExtensionController : IpmsBaseController
    {
        //
        // GET: /SuppDryDockExtension/
        [Authorize]
        [Route("SuppDryDockExtension/{id?}")]
        public ActionResult SuppDryDockExtension(string id)
        {
            ViewBag.ID = id;
            if (System.Configuration.ConfigurationManager.AppSettings["MyVersion"] != null)
            {
                ViewBag.VersionNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MyVersion"].ToString(), CultureInfo.InvariantCulture);
            }
            else
            {
                ViewBag.VersionNumber = 0;
            }
            return View();
        }

	}
}