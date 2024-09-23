using System.Web.Mvc;



namespace IPMS.Web.Controllers
{
    public class MobileConversationController : Controller
    {
        public ActionResult Conversations()
        {
            ViewBag.LoginUserName = User.Identity.Name;         
            return View();
        }
    }
}