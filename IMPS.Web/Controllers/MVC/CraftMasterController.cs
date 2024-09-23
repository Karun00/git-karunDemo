using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class CraftMasterController : IpmsBaseController
    {
        //
        // GET: /CraftMaster/

        [Route("Crafts")]
        public ActionResult ManageCraftmaster()
        {
            //To Check Page Privilege and Permission to access
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageCraftmaster", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}