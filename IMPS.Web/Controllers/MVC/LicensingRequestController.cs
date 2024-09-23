using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class LicensingRequestController : IpmsBaseController
    {
        //
        //[Route("LicensingRequest")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("LicensingRequest")]
        public ActionResult ManageLicensingRequest()
        {
            return View();
        }

        [Route("LicensingRequestList/{LicenseRequestID?}")]
        public ActionResult GetLicensingRequestView(int licenseRequestid)
        {
            ViewBag.LicenseRequestID = licenseRequestid;
            return View("ManageLicensingRequest");
        }


        [Authorize]
        [Route("LicensingRequestList")]
        public ActionResult ManageLicensingRequestList()
        {
            return View("ManageLicensingRequestList");
        }

    }
}


