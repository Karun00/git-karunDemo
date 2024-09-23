using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class EntitiesController : IpmsBaseController
    {  
        //[Authorize]
        //public ActionResult Entities()
        //{
        //    return View();
        //}

        [Authorize]
        [Route("Entities")]
        public ActionResult Entities()
        {
            //return View("Entities", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("Entities", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

  
    }
}