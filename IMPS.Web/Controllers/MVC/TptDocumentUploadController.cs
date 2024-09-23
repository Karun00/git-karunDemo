using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class TptDocumentUploadController : IpmsBaseController
    {
        //
        // GET: /TptDocumentUpload/

        [Authorize]
        [Route("TptDocumentUpload")]
        public ActionResult TptDocumentUpload()
        {
            return View("TptDocumentUpload", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("TptDocumentUpload", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}
        }
    }

    public class TfrDocumentUploadController : IpmsBaseController
    {
        //
        // GET: /TptDocumentUpload/

        [Authorize]
        [Route("TfrDocumentUpload")]
        public ActionResult TfrDocumentUpload()
        {
            return View("TfrDocumentUpload", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("TptDocumentUpload", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}
        }

        [Authorize]
        [Route("TrainMonitoring")]
        public ActionResult TrainMonitoring()
        {
            return View("TrainMonitoring", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("TptDocumentUpload", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}
        }

    }
}