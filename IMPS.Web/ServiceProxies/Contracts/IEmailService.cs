using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IEmailService : IDisposable
    {
        [OperationContract]
        bool SendEmail(string msgbody, string subject, string strToAddress);
        //[OperationContract]
        //bool SendEmailAsync(string msgbody, string subject, string strToAddress);
    }
}
