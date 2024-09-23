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
    public class SAPMarineOrderChangeService : ServiceBase, ISAPMarineOrderChangeService
    {
       
        private ILog log;

        public SAPMarineOrderChangeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public SAPMarineOrderChangeService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPMarineOrderCreateService));
            log.Info("SAPMarineOrderChangeService() Class Instantiated....");
            _unitOfWork = new UnitOfWork(new TnpaContext());
           
        }

        public SAPMarineOrderVO ZipmsMrnOrderResponse(SAPMarineOrderVO objMarineOrder)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SAPMarineOrderVO objRes = new SAPMarineOrderVO();
                 if (objMarineOrder != null)
                 {
                     log.Info("ZIPMS_MRN_ORDERResponse method Instantiated for Updation....");
                     objRes.CREATEDDATE = Convert.ToDateTime("2015-04-10 12:00", System.Globalization.CultureInfo.InvariantCulture);
                     objRes.SALESDOCUMENT = "SO100";
                     log.Info("ZIPMS_MRN_ORDERResponse method Updated....SALESDOCUMENT : " + objRes.SALESDOCUMENT);
                 }
                 else
                 {
                     log.Error("SAPMarineOrderVO is null");
                     throw new BusinessExceptions("SAPMarineOrderVO Object can't be null");
                 }
                 return objRes;

             });
        }
    }
}
