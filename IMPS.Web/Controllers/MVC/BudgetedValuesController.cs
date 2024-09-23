using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class BudgetedValuesController : IpmsBaseController
    {
        //
        // GET: /BudgetedValues/

        [Authorize]
        [Route("BudgetedValues")]
        public ActionResult BudgetedValues()
        {
            //return View("BudgetedValues", privilege);

            if (!String.IsNullOrEmpty(privilege.Privileges))
            {
                return View("BudgetedValues", privilege);

            }
            else
            {
                return View("NotFound");
            }
        }
    }
}