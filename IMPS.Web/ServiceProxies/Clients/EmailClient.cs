using IPMS.Web.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class EmailClient : UserClientBase<IEmailService>, IEmailService
    {
        public bool SendEmail(string msgbody, string subject, string strToAddress)
        {
            return WrapOperationWithException(() => Channel.SendEmail(msgbody, subject, strToAddress));
        }
        //public bool SendEmailAsync(string msgbody, string subject, string strToAddress)
        //{
        //    return WrapOperationWithException(() => Channel.SendEmailAsync(msgbody, subject, strToAddress));
        //}
    }
}