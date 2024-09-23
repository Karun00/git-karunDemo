using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace IPMS.Web.Controllers
{
    public class WelcomeController : IpmsBaseController
    {


        [Route("Welcome")]
        public ActionResult DashBoard()
        {
            return View("Welcome");
        }

       
	}
}