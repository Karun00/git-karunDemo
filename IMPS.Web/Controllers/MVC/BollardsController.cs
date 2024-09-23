using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class BollardsController : IpmsBaseController
    {
        //
        // GET: /Bollards/      
        [Route("Bollards")]
        public ActionResult ManageBollards(int bollardId = 0, int isView = 0)
        {
            ViewBag.bollardId = bollardId;
            ViewBag.isView = isView;
           // return View("ManageBollards", privilege);

            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageBollards", privilege);

            }
            else
            {
                return View("NotFound");
            }
 


        }
	}
}