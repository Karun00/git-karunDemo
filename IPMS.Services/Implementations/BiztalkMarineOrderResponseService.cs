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
    public class BiztalkMarineOrderResponseService : ServiceBase, IBiztalkMarineOrderResponseService
    {
        private ILog log;
        public BiztalkMarineOrderResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BiztalkMarineOrderResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkMarineOrderResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            log.Info("BiztalkMarineOrderResponseService class Instantiated....");
        }

        public SAPMarineOrderVO BiztalkMarineOrderResponse(SAPMarineOrderVO objMarineOrder)
        {
            return EncloseTransactionAndHandleException(() =>
             {

                 log.Info("BiztalkResponse method called....");
                 var Remarks = "";
                 if (objMarineOrder != null)
                 {
                     if (!string.IsNullOrEmpty(objMarineOrder.ERRMSG))
                     {
                         log.Error(objMarineOrder.ERRMSG);
                         _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, "Error : " + objMarineOrder.ERRMSG, objMarineOrder.SAPPOSTINGID);
                     }
                     else
                     {
                         if (!string.IsNullOrEmpty(objMarineOrder.SALESDOCUMENT))
                         {
                             if (objMarineOrder.MESSAGETYPE == SAPMessageTypes.MarineCreate)
                             {
                                 Remarks = "Marine Order Created";
                                // var brt = 
                                 _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objMarineOrder.SALESDOCUMENT, SAPPostingStatus.Completed, Remarks, objMarineOrder.SAPPOSTINGID);
                                 log.Debug("SAP Response Received with SAPReferenceNo..." + objMarineOrder.SALESDOCUMENT);
                             }
                             if (objMarineOrder.MESSAGETYPE == SAPMessageTypes.MarineUpdate)
                             {
                                 Remarks = "Marine Order Updated";
                                 var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objMarineOrder.SALESDOCUMENT, SAPPostingStatus.Completed, Remarks, objMarineOrder.SAPPOSTINGID);
                                 log.Debug("SAP Response Received with SAPReferenceNo..For Updation" + objMarineOrder.SALESDOCUMENT);
                             }
                         }
                         else
                         {
                             Remarks = "Marine Order can't be update with out SalesDocument";
                             _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objMarineOrder.SAPPOSTINGID);
                             log.Error("BusinessExceptions :  VCN is null");
                          //   throw new BusinessExceptions("Marine Order can't be update with out SalesDocument");
                         }
                         if (string.IsNullOrEmpty(objMarineOrder.SALESDOCUMENT))
                         {
                             Remarks = "Marine Order can't be update";
                             _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objMarineOrder.SAPPOSTINGID);
                             log.Error("BusinessExceptions :  SalesDocument is null");
                          //   throw new BusinessExceptions("Marine Order can't be update with out SAP Referenceno (SalesDocument)");
                         }
                     }
                 }
                 else
                 {
                     log.Error("SAPMarineOrderVO object is null");
                     //throw new BusinessExceptions("Response Object can't be null");

                 }
                 return objMarineOrder;
             });
        }
    }
}
