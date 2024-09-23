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
using Core.Repository.Providers.EntityFramework;
using System.Collections.Generic;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
   public class BiztalkPortLimitResponseService:ServiceBase,IBiztalkPortLimitResponseService
    {
        private ILog log;
        private IVesselCallAnchorageService vesselAnchorageService;
        
        public BiztalkPortLimitResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            vesselAnchorageService = new VesselCallAnchorageService(_unitOfWork);
        }
        public BiztalkPortLimitResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkPortLimitResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vesselAnchorageService = new VesselCallAnchorageService(_unitOfWork);
            log.Info("BiztalkPortLimitResponseService class Instantiated....");
        }
        public List<PortLimitDataVO> BiztalkPortLimitResponse(List<PortLimitDataVO> objPortLimitDatalst)
        {
            if (objPortLimitDatalst != null)
            {
                foreach (var objPortLimitData in objPortLimitDatalst)
                {
                    var BiztalkPortLimit = new PortLimitDataVO.PortLimitData_dml_Proc(objPortLimitData);
                    var BiztalkPortLimit_result =
                        _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(BiztalkPortLimit);
                    objPortLimitData.PortlimitId = BiztalkPortLimit.PortlimitId;
                    objPortLimitData.PortCode = BiztalkPortLimit.PortCode;
                    objPortLimitData.VCN = BiztalkPortLimit.VCN;
                    if (objPortLimitData.PortlimitId > 0)
                    {
                        vesselAnchorageService.VesselCallNotification(objPortLimitData.PortlimitId, objPortLimitData.PortCode, objPortLimitData.VCN);
                        log.Info("Vessel Capture Arrival Departure Notification has been sent for VCN  : " + objPortLimitData.VCN);
                    }
                    log.Info("AIS PortLimit Data Inserted with PortlimitId : " + objPortLimitData.PortlimitId);

                }
            }
            return objPortLimitDatalst;
        }
    }
}
