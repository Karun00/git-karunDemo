using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class UserController : IpmsBaseController
    {
        //
        // GET: /User/
        [Route("Users")]
        public ActionResult UserMaster()
        {
            //return View("UserMaster",privilege);


            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("UserMaster", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }



        [Route("UserRegistration/{UserID?}")]
        public ActionResult UserMaster(string UserID)
        {
            ViewBag.UserID = UserID;
            //return View("UserMaster", privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("UserMaster", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }


	}
}