using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class SubCategoriesController : IpmsBaseController
    {

        public ActionResult SubCategoriesMaster()
        {
            return View("SubCategories", privilege);
        }
              
        [Authorize]
        [Route("SubCategories")]
        public ActionResult ManageSubCategories(int Id = 0, int isView = 0)
        {
            ViewBag.quayId = Id;
            ViewBag.isView = isView;
            // return View("ManageSubCategories", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageSubCategories", privilege);

            }
            else
            {
                return View("NotFound");
            }





        }


    }
}