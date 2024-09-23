using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class VesselSAPPostingController : IpmsBaseController
    {
        [Authorize]
        [Route("VesselSAPPosting")]
        public ActionResult VesselSAPPosting()
        {
            return View();
        }  
    }
}













