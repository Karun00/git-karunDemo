using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class DeploymentPlanController : IpmsBaseController
    {

        /// <summary>
        /// To return view for Deployment Plan screen
        /// </summary>
        /// <returns></returns>
        ///  
        [Authorize]
        [Route("DeploymentPlan")]
        public ActionResult DeploymentPlan()
        {
            //return View("DeploymentPlan", privilege);


            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("DeploymentPlan", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
    }
}
