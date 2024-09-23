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
    public class AuditLogController : IpmsBaseController
    {
        public ActionResult ManageAuditLog()
        {
            return View();
        }
    }
}