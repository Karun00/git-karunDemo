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
    public class SAPVesselCreateService : ServiceBase, ISAPVesselCreateService
    {
       
        private ILog log;

        public SAPVesselCreateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public SAPVesselCreateService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPVesselCreateService));
            log.Info("SAPVesselCreateService() Class Instantiated....");
            _unitOfWork = new UnitOfWork(new TnpaContext());
           
        }

        public SAPVesselCreateVO ZIPMS_VESSEL_DETAILS(SAPVesselCreateVO objVessel)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SAPVesselCreateVO objRes = new SAPVesselCreateVO();
                 if (objVessel != null)
                 {
                     log.Info("ZIPMS_MRN_ORDER method Instantiated....");
                     objRes.ENUMBER = "VS100";
                     log.Info("ZIPMS_VESSEL_DETAILS method completed....ENUMBER : " + objRes.ENUMBER);
                 }
                 else
                 {
                     log.Error("SAPVesselCreateVO is null");
                     throw new BusinessExceptions("SAPVesselCreateVO Object can't be null");
                 }
                 return objRes;

             });
        }
    }
}
