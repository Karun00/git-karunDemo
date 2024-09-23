using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class TerminalOperatorMasterController : IpmsBaseController
    {
        //
        // GET: /TerminalOperator/

        //For viewing all TerminalOperators details
        [Authorize]
        [Route("TerminalOperatorMaster")]
        public ActionResult TerminalOperatorMaster()
        {
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("TerminalOperatorMaster", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}