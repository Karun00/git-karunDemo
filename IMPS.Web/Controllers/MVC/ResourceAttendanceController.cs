using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ResourceAttendanceController : IpmsBaseController
    {
        /// <summary>
        /// To View Resource attendance screen
        /// </summary>
        /// <returns></returns>
        [Route("ResourceAttendance")]
        public ActionResult ManageResourceAttendance()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageResourceAttendance");
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}