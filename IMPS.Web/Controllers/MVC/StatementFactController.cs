using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class StatementFactController : IpmsBaseController
    {
        [Authorize]
        [Route("StatementFact")]
        public ActionResult StatementFact()
        {
           // return View("StatementFact", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("StatementFact", privilege);

            }
            else
            {
                return View("NotFound");
            }



        }
    }
}
        


