using System.Web.Mvc;
using System;

namespace IPMS.Web.Controllers.MVC
{
    public class FuelRequisitionController : IpmsBaseController
    {
        [Authorize]
        [Route("FuelRequisition/{id?}")]
        public ActionResult FuelRequisition(string id)
        {
            ViewBag.ID = id;
           // return View("FuelRequisition", privilege);

            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("FuelRequisition", privilege);
                //return View("NotFound");

            }
            else
            {
                return View("NotFound");
                //return View("FuelRequisition", privilege);
            }

        }
    }
}