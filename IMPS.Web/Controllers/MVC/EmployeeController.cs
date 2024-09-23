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
    public class EmployeeController : IpmsBaseController
    {
        //
        // GET: /Berth/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Route("Employees")]
        public ActionResult EmployeeMaster()
        {
            //return View("EmployeeMaster", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("EmployeeMaster", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
	}
}