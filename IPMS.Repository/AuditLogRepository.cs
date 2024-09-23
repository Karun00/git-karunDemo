using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using log4net;
using IPMS.Repository;
using System.Data.SqlClient;
using System.Net;

namespace IPMS.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {

        private IUnitOfWork _unitOfWork;
        private readonly ILog log;

        public AuditLogRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            log = LogManager.GetLogger(typeof(AuditLogRepository));
        }


        public string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails)
        {
            //try
            //{

            if (auditTrailConfigDetails != null)
            {
                log.Debug("AuditTrailConfiguration Details: ControlerName = " + auditTrailConfigDetails.ControlerName +
                          " , ActionName = " + auditTrailConfigDetails.ActionName);
                //}

                var resultval = from a in _unitOfWork.Repository<AuditTrailConfig>().Queryable()
                    where
                        a.ControlerName == auditTrailConfigDetails.ControlerName &&
                        a.ActionName == auditTrailConfigDetails.ActionName
                    select a;

                if (resultval.Count() == 0)
                {
                    AuditTrailConfig auditTrailConfig = new AuditTrailConfig();
                    auditTrailConfig.ObjectState = ObjectState.Added;
                    auditTrailConfig.ControlerName = auditTrailConfigDetails.ControlerName;
                    auditTrailConfig.ActionName = auditTrailConfigDetails.ActionName;
                    auditTrailConfig.IsAuditTrailRequired = "Y";

                    if (auditTrailConfigDetails.IsSecurityAuditTrail == "Y")
                        auditTrailConfig.UserFriendlyDescription = auditTrailConfigDetails.UserFriendlyDescription;
                    else
                        auditTrailConfig.UserFriendlyDescription = "User has logged into " +
                                                                   auditTrailConfigDetails.ActionName + " action";
                    if (auditTrailDetails != null)
                    {
                        auditTrailConfig.RecordStatus = "A";
                        auditTrailConfig.CreatedBy = auditTrailDetails.UserID;
                        auditTrailConfig.CreatedDate = DateTime.Now;
                        auditTrailConfig.ModifiedBy = auditTrailDetails.UserID;
                        auditTrailConfig.ModifiedDate = DateTime.Now;
                    }
                    if (auditTrailConfigDetails.IsSecurityAuditTrail == "Y")
                        auditTrailConfig.IsSecurityAuditTrail = "Y";
                    else
                        auditTrailConfig.IsSecurityAuditTrail = "N";

                    _unitOfWork.Repository<AuditTrailConfig>().Insert(auditTrailConfig);
                    _unitOfWork.SaveChanges();
                }
                else if (resultval.Count() != 0)
                {
                    if (resultval.FirstOrDefault().IsAuditTrailRequired == "Y")
                    {
                        AuditTrail auditTrail = new AuditTrail();
                        auditTrail.ObjectState = ObjectState.Added;
                        auditTrail.AuditTrailConfigID = resultval.FirstOrDefault().AuditTrailConfigID;
                        auditTrail.EntryORExit = auditTrailDetails.EntryORExit;
                        auditTrail.AuditDateTime = auditTrailDetails.AuditDateTime;
                        auditTrail.Content = auditTrailDetails.Content;
                        auditTrail.UserID = auditTrailDetails.UserID;
                        auditTrail.UserName = auditTrailDetails.UserName;
                        auditTrail.UserIPAddress = auditTrailDetails.UserIPAddress;
                        // Modified by Srini - on 28th Jan 2016 - Bug id 21130
                        if ((auditTrailDetails.Parameters != null) && (auditTrailDetails.Parameters.Length > 499))
                        {

                            auditTrail.Parameters = auditTrailDetails.Parameters.Substring(0, 499);
                        }
                        else
                        {
                            auditTrail.Parameters = auditTrailDetails.Parameters;
                        }
                        auditTrail.RecordStatus = "A";
                        auditTrail.CreatedBy = auditTrailDetails.UserID;
                        auditTrail.CreatedDate = DateTime.Now;
                        auditTrail.ModifiedBy = auditTrailDetails.UserID;
                        auditTrail.ModifiedDate = DateTime.Now;
                        auditTrail.UserComputerName = auditTrailDetails.UserComputerName;
                        _unitOfWork.Repository<AuditTrail>().Insert(auditTrail);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            return "";
        }


    }
}
