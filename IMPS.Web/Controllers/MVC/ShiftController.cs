using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class ShiftController : IpmsBaseController
    {
        //
        // GET: /Shift/
          [Route("Shift")]
        public ActionResult ManageShift()
        {
            //return View("ManageShift",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageShift", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }
	}
}