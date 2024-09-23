using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ManageQuaysController : IpmsBaseController
    {     
        [Authorize]
        [Route("Quays")]
        public ActionResult ManageQuays(int id = 0, int isView = 0)
        {
            ViewBag.quayId = id;
            ViewBag.isView = isView;
            //return View("ManageQuays", privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageQuays", privilege);
                //return View("NotFound");

            }
            else
            {
                return View("NotFound");
                //return View("ManageQuays", privilege);
            }
        }        
	}
}