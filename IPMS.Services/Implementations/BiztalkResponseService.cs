using Core.Repository;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Core.Repository.Exceptions;
using System;
using log4net;
using log4net.Config;
using IPMS.Domain;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BiztalkResponseService : ServiceBase, IBiztalkResponseService
    {
        private ILog log;
        public BiztalkResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_arrivalnotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
        }
        public BiztalkResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());

            log.Info("BiztalkResponseService class Instantiated....");
        }
     
        public SAPArrivalVO BiztalkResponse(SAPArrivalVO objVesselArrival)
        {
            return EncloseTransactionAndHandleException(() =>
             {

                 log.Info("BiztalkResponse method called....");
                 var Remarks = "";
                 //Response from Biztalk server
                 //var resultVcn = _arrivalnotificationRepository.GetArrivalDataforSAP(objVesselArrival.VCN);
                 if (objVesselArrival != null)
                 {
                     if (!string.IsNullOrEmpty(objVesselArrival.ERRMSG))
                     {
                         log.Error(objVesselArrival.ERRMSG);
                         _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, "Error : " + objVesselArrival.ERRMSG, objVesselArrival.SAPPOSTINGID);
                     }
                     else
                     {
                         if (!string.IsNullOrEmpty(objVesselArrival.ZZARRNO) && objVesselArrival.ZZARRNO != "0000000000")
                         {
                             if (objVesselArrival.MESSAGETYPE == SAPMessageTypes.ArrivalCreate)
                             {
                                 var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objVesselArrival.ZZARRNO, SAPPostingStatus.Completed, objVesselArrival.STATUS, objVesselArrival.SAPPostingID);
                                 log.Debug("SAP Response Received with SAPReferenceNo..." + objVesselArrival.ZZARRNO);
                             }
                             if (objVesselArrival.MESSAGETYPE == SAPMessageTypes.ArrivalUpdate)
                             {
                                 var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objVesselArrival.ZZARRNO, SAPPostingStatus.Completed, objVesselArrival.STATUS, objVesselArrival.SAPPostingID);
                                 log.Debug("SAP Response Received with SAPReferenceNo..For Updation" + objVesselArrival.ZZARRNO);
                             }
                         }
                         else
                         {
                             if (objVesselArrival.ZZARRNO == "0000000000")
                             {
                             Remarks = "SAP Posting can't be update with out ZZARRNO,It is giving Zero's(ZZARRNO=0000000000)";
                             _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, objVesselArrival.STATUS + " With ZZARRNO is" + objVesselArrival.ZZARRNO + ",can't be update SAP Posting with this ZZARRNO", objVesselArrival.SAPPostingID);
                             log.Error("BusinessExceptions :  ZZARRNO is returning 0000000000");
                             }
                             else
                             {
                             if (string.IsNullOrEmpty(objVesselArrival.ZZARRNO) )
                             Remarks = "SAP Posting can't be update with out ZZARRNO,it is returning null";
                             _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, objVesselArrival.STATUS + " With ZZARRNO is" + objVesselArrival.ZZARRNO + ",can't be update SAP Posting with null value ZZARRNO", objVesselArrival.SAPPostingID);
                             log.Error("BusinessExceptions :  ZZARRNO is null");
                             }
                         }
                         
                     }
                 }
                 else
                 {
                     log.Error("SAPArrivalVO object is null");
                     throw new BusinessExceptions("Response Object can't be null");

                 }
                 return objVesselArrival;
             });
        }
    }
}
