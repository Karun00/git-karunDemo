using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IEmailService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool SendEmail(string msgbody, string subject, string strToAddress);
    }
}
