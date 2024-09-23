using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class PilotExemptionRequestController : IpmsBaseController
    {
        //
          [AllowAnonymous]
        [Route("PilotExemptionRequest")]
        public ActionResult ManagePilotExemptionRequest()
        {
            return View("ManagePilotExemptionRequest",privilege);

        }

        [Route("PilotExemptionRequestList")]
        public ActionResult ListUpdatePilotExemptionRequest()
        {
            //return View("ListUpdatePilotExemptionRequest",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ListUpdatePilotExemptionRequest", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

        [Route("PilotExemptionRequestList/{PilotID?}")]
        public ActionResult GetPilotExemptionRequestView(int pilotid)
        {
            ViewBag.PilotID = pilotid;
            //return View("ManagePilotExemptionRequest",privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManagePilotExemptionRequest", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

    }
}