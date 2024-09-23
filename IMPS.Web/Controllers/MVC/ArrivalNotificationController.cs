using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class ArrivalNotificationController : IpmsBaseController
	{
		//
		// GET: /ArrivalNotification/
        [Authorize]
        [Route("ArrivalNotifications")]
		public ActionResult Index()
		{
			return View("ArrivalNotification", privilege);
		}
        [Authorize]
        [Route("ArrivalNotifications/{vcn?}/{WorkflowInstanceId?}")]
        public ActionResult GetArrivalNotificationView(string vcn, int WorkflowInstanceId)
        {
            ViewBag.VCN = vcn;
            ViewBag.WorkflowInstanceId = WorkflowInstanceId;
            return View("ArrivalNotification", privilege);
        }

       
	}
}