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
    public class BiztalkVesselCreateResponseService : ServiceBase, IBiztalkVesselCreateResponseService
    {
        private ILog log;
        public BiztalkVesselCreateResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BiztalkVesselCreateResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkVesselCreateResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            log.Info("BiztalkVesselCreateResponse class Instantiated....");
        }

        public SAPVesselCreateVO BiztalkVesselCreateResponse(SAPVesselCreateVO objVessel)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                log.Info("BiztalkResponse method called....");
                var Remarks = "";
                if (objVessel != null)
                {

                    if (objVessel.SUBRC == 0) //Created
                    {
                        log.Debug("SAP vessel create response SUBRC : " + objVessel.SUBRC + " : SAPNo." + objVessel.ENUMBER);
                        var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objVessel.ENUMBER, SAPPostingStatus.Completed, objVessel.ERRMSG, objVessel.SAPPOSTINGID);
                        var vsap = _unitOfWork.ExecuteSqlCommand("insert into VesselSAPAccount(VesselID, SAPAccountNo, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", objVessel.VesselID, objVessel.ENUMBER, "A", 1, DateTime.Now, 1, DateTime.Now);
                        log.Debug("SAP Response Received with SAPReferenceNo..." + objVessel.ENUMBER);
                    }
                    else if (objVessel.SUBRC == 1) //already Existed
                    {
                        log.Debug("SAP vessel already exist SUBRC : " + objVessel.SUBRC + " : SAPNo." + objVessel.ENUMBER);
                        _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1,SAPReferenceNo=@p2 where SAPPostingID = @p3", SAPPostingStatus.Completed, objVessel.ERRMSG, objVessel.ENUMBER, objVessel.SAPPOSTINGID);
                        _unitOfWork.ExecuteSqlCommand("insert into VesselSAPAccount(VesselID, SAPAccountNo, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", objVessel.VesselID, objVessel.ENUMBER, "A", 1, DateTime.Now, 1, DateTime.Now);
                        log.Debug("SAP Response Received with SAPReferenceNo...Updation" + objVessel.ENUMBER);
                    }
                    else //Other than Create and Update i.e. Error
                    {
                        log.Debug("SAP vessel not posted SUBRC : " + objVessel.SUBRC + " : SAPNo." + objVessel.ENUMBER);
                        _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, objVessel.ERRMSG, objVessel.SAPPOSTINGID);

                    }

                    #region Old Code
                    //if (!string.IsNullOrEmpty(objVessel.ERRMSG) && objVessel.SUBRC != 1)
                    //{
                    //    log.Error(objVessel.ERRMSG);
                    //    _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1,SAPReferenceNo=@p2 where SAPPostingID = @p3", SAPPostingStatus.Error, "Error : " + objVessel.ERRMSG, objVessel.ENUMBER, objVessel.SAPPOSTINGID);
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(objVessel.ENUMBER))
                    //    {
                    //        if (objVessel.MESSAGETYPE == SAPMessageTypes.VesselCreate || objVessel.SUBRC == 1)
                    //        {
                    //            log.Debug("SAP vessel create response" + objVessel.ENUMBER);
                    //          //  Remarks = "Vessel Created";
                    //            var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objVessel.ENUMBER, SAPPostingStatus.Completed, objVessel.ERRMSG, objVessel.SAPPOSTINGID);                                
                    //            var vsap = _unitOfWork.ExecuteSqlCommand("insert into VesselSAPAccount(VesselID, SAPAccountNo, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6)", objVessel.VesselID, objVessel.ENUMBER, "A", 1, DateTime.Now, 1, DateTime.Now);
                    //            log.Debug("SAP Response Received with SAPReferenceNo..." + objVessel.ENUMBER);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Remarks = "SAP Posting can't be update with out ENUMBER";
                    //        _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objVessel.SAPPOSTINGID);
                    //        log.Error("BusinessExceptions :  ENUMBER is null");
                    //      //  throw new BusinessExceptions("Marine Order can't be update with out SUBRC");
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    Remarks = "SAP Returned Null Values..Contact Admin";
                    log.Error("SAPVesselCreateVO object is null");
                    _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objVessel.SAPPOSTINGID);
                    // throw new BusinessExceptions("Response Object can't be null");

                }
                return objVessel;
            });
        }
    }
}
