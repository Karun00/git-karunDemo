using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class DashBoardController : IpmsBaseController
    {
        //
        // GET: /DashBoard/
        public ActionResult Index()
        {
            return View();
        }
	}
}