using System;
using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class FuelReceiptController : IpmsBaseController
    {
        [Authorize]
        [Route("FuelReceipt/{id?}")]
        public ActionResult FuelReceipt(string id)
        {
            ViewBag.ID = id;
           // return View("FuelReceipt", privilege);


            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("FuelReceipt", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }
    }
}
       