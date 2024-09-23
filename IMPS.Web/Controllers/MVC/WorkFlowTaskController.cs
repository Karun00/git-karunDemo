using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.ComponentModel.Composition;

using IPMS.Domain.Models;
using System.Web.Security;

namespace IPMS.Web.Controllers
{

    public class WorkFlowTaskController : IpmsBaseController
    {
        [Route("WorkFlowTasks")]
        public ActionResult ManageWorkFlows()
        {
           // return View("ManageWorkFlows", privilege);

          if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManageWorkFlows", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }


       
    }
}