using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class AuditLogClient : UserClientBase<IAuditLogService>, IAuditLogService
    {
        public string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails)
        {
            return WrapOperationWithException(() => Channel.UserActivityLogging(auditTrailConfigDetails, auditTrailDetails));
        }

        public List<AuditLogDetails> GetAuditLogs(string columnName, string searchValue, string userName, string auditFromDt, string auditToDt)
        {
            return WrapOperationWithException(() => Channel.GetAuditLogs(columnName, searchValue, userName, auditFromDt, auditToDt));
        }

        //public string UserActivityLoggingAsync(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails)
        //{
        //    return WrapOperationWithException(() => Channel.UserActivityLoggingAsync(auditTrailConfigDetails, auditTrailDetails));
        //}

        public List<AuditLogDetails> GetAuditLogsAsync()
        {
            return WrapOperationWithException(() => Channel.GetAuditLogsAsync());
        }

        public int GetUserID(string username)
        {
            return WrapOperationWithException(() => Channel.GetUserID(username));
        }

        public List<AuditLogDetails> GetAuditSearchDetails(string userName, string auditFromDt, string auditToDt, string isSecurityAuditLog)
        {
            return WrapOperationWithException(() => Channel.GetAuditSearchDetails(userName, auditFromDt, auditToDt, isSecurityAuditLog));
        }
    }
}