using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using log4net;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace IPMS.Web.Api
{
    public class AuditLogController : ApiControllerBase
    {
        IAuditLogService _auditlogservice;
        ILog log = log4net.LogManager.GetLogger(typeof(AccountController));

        public AuditLogController()
        {
            _auditlogservice = new AuditLogClient();
        }

        public HttpResponseMessage GetAllAuditLogs(HttpRequestMessage request, string columnName, string searchValue, string userName, string auditFromDT, string auditToDT)
        {
            log.Info("Audit Trails");
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<AuditLogDetails> auditlogs = _auditlogservice.GetAuditLogs(columnName, searchValue, userName, auditFromDT, auditToDT);
                response = request.CreateResponse<List<AuditLogDetails>>(HttpStatusCode.OK, auditlogs);
                return response;
            });
        }


        public HttpResponseMessage GetAuditSearchDetails(HttpRequestMessage request, string userName, string auditFromDt, string auditToDt, string isSecurityAuditLog)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage responce = null;
                List<AuditLogDetails> auditDtl = _auditlogservice.GetAuditSearchDetails(userName, auditFromDt, auditToDt, isSecurityAuditLog);
                responce = request.CreateResponse<List<AuditLogDetails>>(HttpStatusCode.OK, auditDtl);
                return responce;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _auditlogservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
