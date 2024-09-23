using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class MaterialCodeMasterController : IpmsBaseController
    {
        [Route("MaterialCodeMaster/{id?}")]
        public ActionResult MaterialCodeMaster(string id)
        {
            ViewBag.ID = id;
            return View("MaterialCodeMaster", privilege);
        }
    }
}