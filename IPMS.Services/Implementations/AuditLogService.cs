using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using log4net;
using IPMS.Repository;
using System.Data.SqlClient;
using System.Net;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuditLogService : ServiceBase, IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog log;
        private IAccountRepository _accountRepository;
        private IAuditLogRepository _auditLogRepository;


        public AuditLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AuditLogService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            log = LogManager.GetLogger(typeof(AuditLogService));
            _accountRepository = new AccountRepository(_unitOfWork);
            _auditLogRepository = new AuditLogRepository(_unitOfWork);
        }

        public int GetUserID(string username)
        {
            int userid = _accountRepository.GetUserId(username);
            return userid;
        }

        public string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _auditLogRepository.UserActivityLogging(auditTrailConfigDetails, auditTrailDetails);
            });
        }

        public List<AuditLogDetails> GetAuditLogs(string columnName, string searchValue, string userName, string auditFromDt, string auditToDt)
        {
            List<AuditLogDetails> data = new List<AuditLogDetails>();
            try
            {
                string ColumnNameP = columnName;
                string searchValueP = searchValue;
                //string userName = UserName;
                //string auditFromDateTime = AuditFromDt;
                //string auditToDateTime = AuditToDt;


                string query = "EXEC usp_GetAuditLogs '" + ColumnNameP + "','" + searchValueP + "','" + userName + "','" + auditFromDt + "','" + auditToDt + "'";
                var Request = _unitOfWork.SqlQuery<AuditLogDetails>(query).ToList();
                data = Request.ToList();
            }
            catch (Exception ex)
            {
                log.Error("Exception " + ex.Message);
            }

            return data;
        }

        public List<AuditLogDetails> GetAuditSearchDetails(string userName, string auditFromDt, string auditToDt, string isSecurityAuditLog)
        {
            // List<AuditLogDetails> data = new List<AuditLogDetails>();
            return ExecuteFaultHandledOperation(() =>
                   {

                       var username = new SqlParameter("@UserName", string.IsNullOrEmpty(userName) ? (object)DBNull.Value : userName);
                       var auditFrmDt = new SqlParameter("@AuditFromDate", auditFromDt);
                       var audittoDt = new SqlParameter("@AuditToDate", auditToDt);
                       var isSecurityAuditTrail = new SqlParameter("@IsSecurityAuditTrail", isSecurityAuditLog);

                       //var srq = _unitOfWork.SqlQuery<AuditLogDetails>("usp_GetAuditTrailSearchDetails @UserName,@AuditFromDate,@AuditToDate,@IsSecurityAuditTrail", username, auditFrmDt, audittoDt, isSecurityAuditTrail).ToList();


                       var srq = _unitOfWork.SqlQuery<AuditLogDetails>("usp_GetAuditTrailSearchDetails @UserName,@AuditFromDate,@AuditToDate,@IsSecurityAuditTrail", username, auditFrmDt, audittoDt, isSecurityAuditTrail).ToList();

                       return srq.ToList();

                   });

        }



        public void Dispose()
        {
        }
    }
}
