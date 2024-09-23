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
using IPMS.Domain.Models;
using System.Collections.Generic;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BiztalkAnchorageResponseService : ServiceBase, IBiztalkAnchorageResponseService
    {
        private ILog log;
        private IVesselCallAnchorageService vesselAnchorageService;
        
        public BiztalkAnchorageResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            vesselAnchorageService = new VesselCallAnchorageService(_unitOfWork);
        }
        public BiztalkAnchorageResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkAnchorageResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vesselAnchorageService = new VesselCallAnchorageService(_unitOfWork);
            log.Info("BiztalkAnchorageResponseService class Instantiated....");
        }
        public List<AnchorageDataVO> BiztalkAnchorageResponse(List<AnchorageDataVO> objAnchoragedatalst)
        {
            if (objAnchoragedatalst != null)
            {
                foreach (var objAnchoragedata in objAnchoragedatalst)
                {
                    var BiztalkAnchorage = new AnchorageDataVO.AnchorageData_dml_Proc(objAnchoragedata);
                    var BiztalkAnchorage_result =
                        _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(BiztalkAnchorage);
                    objAnchoragedata.AnchorageId = BiztalkAnchorage.AnchorageId;
                    objAnchoragedata.PortCode = BiztalkAnchorage.PortCode;
                    if (objAnchoragedata.AnchorageId > 0)
                    {
                        vesselAnchorageService.VesselCallAnchorageNotification(objAnchoragedata.AnchorageId, objAnchoragedata.PortCode);
                        log.Info("Vessel Call Anchorage Notification has been sent with AnchorageId : " + objAnchoragedata.AnchorageId);
                    }
                    log.Info("AIS Anchorage Data Inserted with AnchorageId : " + objAnchoragedata.AnchorageId);

                }
            }
            return objAnchoragedatalst;
        }
    }
}
