using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class DredgingOperationController : IpmsBaseController
    {
        //
        // GET: /DredgingOperation/
        /// <summary>
        /// To return view for Dredging Volume screen
        /// </summary>
        /// <returns></returns>
        ///  
        [Route("BerthOccupation")]
        public ActionResult BerthOccupation()
        {
            return View("BerthOccupation", privilege);
        }

        [Route("DredgingVolume")]
        public ActionResult DredgingVolume()
        {
            return View("DredgingVolume", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("DredgingVolume", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}


        }
        /// <summary>
        /// To return view for Berth occupation screen
        /// </summary>
        /// <returns></returns>
        ///  
        [Route("BerthOccupation/{dredgingoperationid?}")]
        public ActionResult BerthOccupation(string dredgingOperationId)
        {
            ViewBag.dredgingoperationid = dredgingOperationId;
            return View("BerthOccupation", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("BerthOccupation", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}


        }

        /// <summary>
        /// To return view for Dredging Volume screen By DredgingOperation ID
        /// </summary>
        /// <returns></returns>
        ///  
        [Route("DredgingVolume/{dredgingoperationid?}")]
        public ActionResult DredgingVolume(string dredgingOperationId)
        {
            ViewBag.dredgingoperationid = dredgingOperationId;
            return View("DredgingVolume", privilege);


            //if (privilege.Privileges != "")
            //{
            //    return View("DredgingVolume", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}


        }
    }
}