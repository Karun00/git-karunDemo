using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAuditLogService : IDisposable
    {
        [OperationContract]
        string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails);

        [OperationContract]
        List<AuditLogDetails> GetAuditLogs(string columnName, string searchValue, string userName, string auditFromDt, string auditToDt);

        //[OperationContract]
        //string UserActivityLoggingAsync(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails);

        [OperationContract]
        List<AuditLogDetails> GetAuditLogsAsync();

        [OperationContract]
        int GetUserID(string username);

        [OperationContract]
        List<AuditLogDetails> GetAuditSearchDetails(string userName, string auditFromDt, string auditToDt, string isSecurityAuditLog);
    }
}