using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using IPMSFeedService.Models;

namespace IPMSFeedService.Repository
{
    public class Accounts: ApplicationDbContexts
    {

        //Verifying user credentials
        public bool Login(string userName, string password)
        {
            try
            {
                var userInfo = new User();

                string authUsername = ConfigurationManager.AppSettings["userName"];
                string authPassWord = ConfigurationManager.AppSettings["passWord"];

                if (userName == authUsername && password == authPassWord)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}