using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace IPMS.Web.Controllers
{
    public class PortsController : IpmsBaseController
    {
        public ActionResult ManagePorts(int portId = 0, int isView = 0)
        {
            ViewBag.portId = portId;
            ViewBag.isView = isView;
            //ViewBag.IsEx = "True";
            //return View("ManagePorts",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("ManagePorts", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }

        [Route("Dashboard")]
        public ActionResult DashBoard()
        {
           // ViewBag.IsEx = "False";
            return View("DashBoard");
        }
        public ActionResult PortMaster()
        {
            //return View("PortMaster",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("PortMaster", privilege);

            }
            else
            {
                return View("NotFound");
            }


        }
        [Route("AllPorts")]
        public ActionResult AllPorts()
        {
            // ViewBag.IsEx = "False";
            return View();
        }
        [Route("ShipsinPorts")]
        public ActionResult ShipsinPorts()
        {
            return View();
        }
       
	}
}