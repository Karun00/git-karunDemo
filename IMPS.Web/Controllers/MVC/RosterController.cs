using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class RosterController : IpmsBaseController
    {
        //
        // GET: /Roster/

        //[Route("Rosters")]     
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Route("Rosters")]     
        public ActionResult ManageRoster()
        {
            return View();
        }
	}
}