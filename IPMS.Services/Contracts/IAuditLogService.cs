using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IAuditLogService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AuditLogDetails> GetAuditLogs(string columnName, string searchValue, string userName, string auditFromDt, string auditToDt);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetUserID(string username);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AuditLogDetails> GetAuditSearchDetails(string userName, string auditFromDt, string auditToDt, string isSecurityAuditLog);
    }
}
