using System.Web.Mvc;
namespace IPMS.Web.Controllers.MVC
{
    public class DredgingPriorityController : IpmsBaseController
    {
        /// <summary>
        /// To return view for Dredging Priority screen
        /// </summary>
        /// <returns></returns>
        ///  
        [Authorize]
        [Route("DredgingPriority/{id?}")]
        public ActionResult DredgingPriority(string id)
        {
            ViewBag.ID = id;
            return View("DredgingPriority", privilege);

            //if (privilege.Privileges != "")
            //{
            //    return View("DredgingPriority", privilege);

            //}
            //else
            //{
            //    return View("NotFound");
            //}

        }
    }
}