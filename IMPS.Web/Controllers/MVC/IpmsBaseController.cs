using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using IPMS.Domain.ValueObjects;
using IPMS.Web.Controllers.API;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;

namespace IPMS.Web.Controllers
{
    [Authorize]
    public class IpmsBaseController : Controller
    {
        public PrivilegeVO privilege;
        public string referenceId;
        public IpmsBaseController()
        {
            privilege = new PrivilegeVO();
            string username = Thread.CurrentPrincipal.Identity.Name;
            string controllername = this.GetType().Name;
            controllername = controllername.Replace("Controller", "");

            IAccountService _accountService = new AccountClient();
            try
            {
                privilege.Privileges = _accountService.GetUserPrivilegesWithControllerName(controllername, username);
            }
            finally
            {
                if (_accountService != null) 
                {
                    _accountService.Dispose();
                    _accountService = null;
                }
            }

        }


    }
}
