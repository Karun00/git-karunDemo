using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class BerthController : IpmsBaseController
    {
        //
        // GET: /Berth/
        public ActionResult Index()
        {
            return View("BerthMaster", privilege);
        }
        
        //For viewing all berths details
       
        [Route("Berths")]
        public ActionResult BerthMaster()
        {
            //return View("BerthMaster", privilege);
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
               return View("BerthMaster", privilege);
                //return View("NotFound");
            }
            else
            {
                //return View("BerthMaster", privilege);
                return View("NotFound");
            } 
        }     
	}
}