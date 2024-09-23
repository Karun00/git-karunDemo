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
    public class SAPMarineOrderCreateService : ServiceBase, ISAPMarineOrderCreateService
    {
       
        private ILog log;

        public SAPMarineOrderCreateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public SAPMarineOrderCreateService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPMarineOrderCreateService));
            log.Info("SAPMarineOrderCreateService() Class Instantiated....");
            _unitOfWork = new UnitOfWork(new TnpaContext());
           
        }

        public SAPMarineOrderVO ZipmsMrnOrder(SAPMarineOrderVO objMarineOrder)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SAPMarineOrderVO objRes = new SAPMarineOrderVO();
                 if (objMarineOrder != null)
                 {
                     log.Info("ZIPMS_MRN_ORDER method Instantiated....");
                     objRes.CREATEDDATE = Convert.ToDateTime("2015-04-10 12:00", System.Globalization.CultureInfo.InvariantCulture);
                     objRes.SALESDOCUMENT = "SO100";
                     log.Info("ZIPMS_MRN_ORDER method completed....SALESDOCUMENT : " + objRes.SALESDOCUMENT);
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
