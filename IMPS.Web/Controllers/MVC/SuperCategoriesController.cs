using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class SuperCategoriesController : IpmsBaseController
    {
        public ActionResult SuperCategory()
        {
            return View("SuperCategories", privilege);
        }

        [Authorize]
        [Route("SuperCategories")]
        public ActionResult ManageSuperCategories()
        {
            //return View("ManageSuperCategories", privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageSuperCategories", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }



    }
}