using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SAPPostingController : IpmsBaseController
    {
        [Authorize]
        [Route("SAPPosting")]
        public ActionResult SAPPosting()
        {
            return View();
        }

        //[Authorize]
        //[Route("Entities")]
        //public ActionResult Entities()
        //{          

        //    if (privilege.Privileges != "")
        //    {
        //        return View("Entities", privilege);

        //    }
        //    else
        //    {
        //        return View("NotFound");
        //    }

        //}

  
    }
}













