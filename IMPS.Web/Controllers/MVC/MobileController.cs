using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IPMS.Web.Controllers
{
    public class MobileController : IpmsBaseController
    {
        string _portcode = string.Empty;

        public ActionResult DashBoard()
        {
            #region Port Code
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies["Port"];
            if (authCookie != null)
            {
                _portcode = authCookie.Value;
                StringBuilder inSb = new StringBuilder(_portcode);
                StringBuilder outSb = new StringBuilder(_portcode.Length);
                char c;
                for (int i = 0; i < _portcode.Length; i++)
                {
                    c = inSb[i];
                    c = (char)(c ^ 6); /// remember to use the same XORkey value you used in javascript
                    outSb.Append(c);
                }
                _portcode = outSb.ToString();

            }
            if (!string.IsNullOrEmpty(_portcode))
            {
                //TODO : Need to maintain Portcode instead in PortName in Report Parameter(s) to avoid hardcode
                ViewBag.PortName = _portcode;
            }
            else
                ViewBag.PortName = (_portcode == "CT" ? "Cape Town" : (_portcode == "DB" ? "Durban" : (_portcode == "EL" ? "East London" : (_portcode == "MB" ? "Mossel Bay" : (_portcode == "RB" ? "Richards Bay" : (_portcode == "SB" ? "Saldanha Bay" : (_portcode == "PE" ? "Port Elizabeth" : (_portcode == "NG" ? "Ngqura" : ""))))))));
            #endregion

            ViewBag.UserName = User.Identity.Name;

            return View();
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Manifest()
        {
            Response.ContentType = "text/cache-manifest";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Cache.SetCacheability(
                System.Web.HttpCacheability.NoCache);
            return View();
        }


        [HttpPost]
        public ActionResult Upload(string url)
        {
            if (url != null)
            {
                url = url.Substring("data:image/png;base64,".Length);
                var buffer = Convert.FromBase64String(url);
                // TODO: I am saving the image on the hard disk but
                // you could do whatever processing you want with it
                System.IO.File.WriteAllBytes(Server.MapPath("~/app_data/capture.png"), buffer);
            }
            return Json(new { success = true });
        }
    }
}