using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity;
using IPMS.Data.Context;
using System.Data.Entity.Infrastructure;
using System;
using System.Web.Mvc;
using System.Threading;


namespace IPMS.Web.Adapters
{
   
    public class SecurityAdapter :ISecurityAdapter
    {
      
        public void Initialize()
        {

            Database.SetInitializer<TnpaContext>(null);

            try
            {
                
                if (!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection("TnpaContext", "Users", "UserID", "UserName", autoCreateTables: true);

                if (!WebSecurity.UserExists("admin"))
                    WebSecurity.CreateUserAndAccount(
                        "admin",
                        "navayuga",
                        new
                        {
                            //UserID = 1,
                            FirstName = "Administrator",
                            LastName = "adminlastname",
                            UserType = "EMP",
                            UserTypeID = 12,
                            ContactNo = 98859885,
                            EmailID = "bc@gmail.com",
                            RecordStatus = 'A',
                          //  CreatedBy = 1,
                            CreatedDate = DateTime.Now,


                        },
                        false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }

            

        }
   

        public bool Login(string username, string password, bool rememberMe)
        {
            return WebSecurity.Login(username, password, persistCookie: rememberMe);
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(username, oldPassword, newPassword);
        }

        public bool UserExists(string username)
        {
            return WebSecurity.UserExists(username);
        }

        public int GetUserId(string username)
        {
            return WebSecurity.GetUserId(username);
        }
    }
}