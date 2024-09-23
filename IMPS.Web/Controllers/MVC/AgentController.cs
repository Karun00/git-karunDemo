using System;
using System.Globalization;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    //[Authorize]
    public class AgentController : IpmsBaseController
    {        
        /// <summary>
        /// To return view for Anonymous New Agent registration screen
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        ///  
        [AllowAnonymous]
        [Route("Agent/NewAgentRegistration/{vcn?}")]
        public ActionResult NewAgentRegistration(string vcn)
        {
            if (!string.IsNullOrEmpty(vcn))
            {
                if (Convert.ToInt32(vcn,CultureInfo.InvariantCulture) > 0)
                {
                    //Response.Redirect="Agent/AgentMaster/{vcn?}";
                    ViewBag.VCN = vcn;
                    return RedirectToAction("AgentMaster", "Agent", new { vcn = vcn });
                }
            }

            return View("NewAgentRegistration",privilege    );
        }
        /// <summary>
        /// To return view for Agent registration screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
           // return View("Registration",privilege);
            if (!string.IsNullOrEmpty(privilege.Privileges))
           // if (privilege.Privileges != "")
            {
                return View("Registration", privilege);
                //return View("NotFound");
            }
            else
            {
                //return View("BerthMaster", privilege);
                return View("NotFound");
            } 
        }
        /// <summary>
        /// To return view for Agent registration screen
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [Route("Agent/AgentMaster/{vcn?}")]
        public ActionResult AgentMaster(string vcn)
        {
            ViewBag.VCN = vcn;
            //return View("AgentMaster",privilege);
            //if (privilege.Privileges != "")
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("AgentMaster", privilege);
                //return View("NotFound");
            }
            else
            {
                //return View("BerthMaster", privilege);
                return View("NotFound");
            } 
        }
        /// <summary>
        /// to Check whether the Document files existed or not in Agent Registration
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public bool CheckFileExists(string fileName, int agentId)
        {
            var result = false;
            fileName = "Agent_" + agentId + "_" + fileName;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            if (System.IO.File.Exists(path))
            {
                result = true;
            }
            return result;
        }
	}
}

