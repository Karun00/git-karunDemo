using Core.Repository;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using IPMS.Core.Repository.Exceptions;
using System;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SAPVesselArrivalChangeService : ServiceBase, ISAPVesselArrivalChangeService
    {
     
        private ILog log;

        public SAPVesselArrivalChangeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SAPVesselArrivalChangeService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPVesselArrivalChangeService));
            log.Info("SAPVesselArrivalChangeService() Class Instantiated....");
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }


        public SAPArrivalResponseVO ZIPMS_VESSEL_ARRIVAL_CHNG(SAPArrivalVO objVesselArrival)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SAPArrivalResponseVO objRes = new SAPArrivalResponseVO();
                 if (objVesselArrival != null)
                 {
                     log.Info("ZIPMS_VESSEL_ARRIVAL_CHNG method Instantiated....");

                     //Dummy Service for Biztalk instead of Hitting SAP server
                     //var resultVcn = _arrivalnotificationRepository.GetArrivalDataforSAP(objVesselArrival.VCN);

                     log.Info("objVesselArrival.CODE: " + objVesselArrival.CODE);
                     objRes.STATUS = "Amended"; //Dummy Status
                     //objRes.ZZARRNO = Convert.ToInt32(objVesselArrival.EDD.ToString("yyyyMMddss")); //DUMMY Arrival No.
                     objRes.ZZARRNO = Convert.ToInt32(objVesselArrival.EDD); //DUMMY Arrival No.
                     log.Info("ZIPMS_VESSEL_ARRIVAL_CHNG method completed....ZZARRNO : " + objRes.ZZARRNO);
                 }
                 else
                 {
                     log.Error("SAPArrivalVO is null");
                     throw new BusinessExceptions("SAPArrivalVO Object can't be null");
                 }
                 return objRes;

             });
        }
    }
}
