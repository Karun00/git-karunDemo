using System.Web.Mvc;

namespace IPMS.Web.Controllers.MVC
{
    public class RolePrivilegeController : IpmsBaseController
    {
        //public ActionResult RolePrivilege()
        //{
        //    return View();
        //}
     
        [Authorize]
        [Route("RolePrivileges")]
        public ActionResult RolePrivilege()
        {
            //return View("RolePrivilege", privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("RolePrivilege", privilege);

            }
            else
            {
                return View("NotFound");
            }
 

        }
    }
}
