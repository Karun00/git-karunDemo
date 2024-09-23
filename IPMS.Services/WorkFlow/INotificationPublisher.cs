using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services.WorkFlow
{
    public interface INotificationPublisher
    {
        void Publish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode);
        void ManualPublish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode, string NotificationTemplateBase, int PermitRequestID, string EmailAddress);
    
    }

   
       

}
