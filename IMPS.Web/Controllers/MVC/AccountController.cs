using System;
using System.Web;
using System.Web.Mvc;
using IPMS.Domain.Models;
using System.Web.Security;
using IPMS.Web.Filters;

namespace IPMS.Web.Controllers
{
    public class AccountController : IpmsBaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (Request.Browser.IsMobileDevice)
            {
                if (Request.IsAuthenticated)
                {
                    HttpContext.Response.Write("<script type='text/javascript'>window.parent.location.href = window.parent.location.href + 'Mobile/Dashboard';</script>");
                    HttpContext.Response.Flush();
                    HttpContext.Response.End();
                }
                else
                {
                    HttpCookie authCookieUN = System.Web.HttpContext.Current.Request.Cookies["userName"];
                    HttpCookie authCookiePwd = System.Web.HttpContext.Current.Request.Cookies["Password"];
                    if (authCookieUN != null & authCookiePwd != null)
                    {
                        FormsAuthenticationTicket authTicketUN = FormsAuthentication.Decrypt(authCookieUN.Value);
                        FormsAuthenticationTicket authTicketPwd = FormsAuthentication.Decrypt(authCookiePwd.Value);
                        if ((authTicketUN != null & !authTicketUN.Expired) & (authTicketPwd != null & !authTicketPwd.Expired))
                        {
                            ViewBag.UserName = authTicketUN.Name;
                            ViewBag.Password = authTicketPwd.Name;
                        }
                    }
                }

                ViewBag.ReturnUrl = returnUrl;

                return View("Login.Mobile", new AccountLoginModel() { ReturnUrl = returnUrl });
            }
            else
            {
                //BugID:4845
                //Redirecting to dashboard if user IsAuthenticated  
                if (Request.IsAuthenticated)
                {
                    HttpContext.Response.Write("<script type='text/javascript'>window.parent.location.href = window.parent.location.href + 'Welcome';</script>");
                    HttpContext.Response.Flush();
                    HttpContext.Response.End();
                }
                else
                {
                    HttpCookie authCookieUN = System.Web.HttpContext.Current.Request.Cookies["userName"];
                    HttpCookie authCookiePwd = System.Web.HttpContext.Current.Request.Cookies["Password"];
                    if (authCookieUN != null & authCookiePwd != null)
                    {
                        FormsAuthenticationTicket authTicketUN = FormsAuthentication.Decrypt(authCookieUN.Value);
                        FormsAuthenticationTicket authTicketPwd = FormsAuthentication.Decrypt(authCookiePwd.Value);
                        if ((authTicketUN != null & !authTicketUN.Expired) & (authTicketPwd != null & !authTicketPwd.Expired))
                        {
                            ViewBag.UserName = authTicketUN.Name;
                            ViewBag.Password = authTicketPwd.Name;
                        }
                    }
                }

                //HttpCookie portc = Request.Cookies["Port"];
                //if (returnUrl == "" || returnUrl == null)
                //{
                //    portc.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Set(portc);
                //}
                if (!string.IsNullOrEmpty(returnUrl) && returnUrl != null)
                {
                    if (returnUrl.ToLower().Contains("logout"))
                        returnUrl = null;
                }
                ViewBag.ReturnUrl = returnUrl;
                return View(new AccountLoginModel() { ReturnUrl = returnUrl });
            }

        }

        [Route("ChangePassword")]
        public ActionResult ChangePassword()
        {
            ViewBag.isFirstTimeLogin = "N";
            return View("ChangePassword");
        }

        [Route("ChangePassword/{isFirstTimeLogin?}")]
        public ActionResult ChangePassword(string isFirstTimeLogin)
        {
            ViewBag.isFirstTimeLogin = isFirstTimeLogin;
            return View("ChangePassword");
        }


        public ActionResult Logout()
        {

            //HttpCookie portc = Request.Cookies["Port"];
            //if (portc != null)
            //{
            //    portc.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Set(portc);
            //}



            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Abandon();
            FormsAuthentication.SignOut();
            //WebSecurity.Logout();

            LogUserActivityAttribute audit = new LogUserActivityAttribute();
            audit.AuditTrail("LogOut", "LogOut", DateTime.Now, User.Identity.Name, string.Empty, "EXIT");

            return RedirectToAction("Login", "Account");
        }
         [AllowAnonymous]
        [Route("TermsConditions")]
        public ActionResult TermsConditions()
        {
             return View("TermsAndConditions");
        }

    }
}