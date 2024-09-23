using System.Web.Mvc;


namespace IPMS.Web.Controllers
{
    public class NewsController : IpmsBaseController
    {
         [Route("News")]
        public ActionResult ManageNews()
        {
            //return View("ManageNews", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageNews", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }  
    }
}