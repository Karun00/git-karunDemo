using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class StatementFactService : ServiceBase, IStatementFactService
    {
        private IStatementFactRepository _statementfactRepository;
        private INotificationPublisher _notificationpublisher;   
        private IPortRepository _portRepository;
        private ISubCategoryRepository _subcategoryRepository;
    //    private IArrivalNotificationRepository _arrivalnotificationRepository;
        private IPortConfigurationRepository _portConfigurationRepository;
        private ITerminalOperatorRepository _terminalOperatorRepository;
        private IBerthRepository _berthRepository;

        private const string _entityCode = EntityCodes.Statement_Fact;

         public StatementFactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _statementfactRepository = new StatementFactRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
         //   _arrivalnotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _terminalOperatorRepository = new TerminalOperatorRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

         public StatementFactService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _statementfactRepository = new StatementFactRepository(_unitOfWork);
            _portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
          //  _arrivalnotificationRepository = new ArrivalNotificationRepository(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _terminalOperatorRepository = new TerminalOperatorRepository(_unitOfWork);
            _berthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

         /// <summary>
         /// To Get Entity Details Based on EntitiyCode
         /// </summary>
         /// <param name="entityCode"></param>
         /// <returns></returns>
         public Entity GetEnties(string entityCode)
         {
             return _statementfactRepository.GetEnties(entityCode);
         }

         /// <summary>
         /// To Get User Details by UserId
         /// </summary>
         /// <param name="UserID"></param>
         /// <returns></returns>
         public CompanyVO GetUserDetails(int UserID)
         {
             return _statementfactRepository.GetUserDetails(_UserId);
         }

         /// <summary>
         /// Add Statement Of Fact
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         public StatementVCNVO AddStatementFact(StatementVCNVO data)
         {
             return EncloseTransactionAndHandleException(() =>
             {
                 string name = _LoginName;
                 data.CreatedBy = _UserId;
                 data.CreatedDate = DateTime.Now;
                 data.ModifiedBy = _UserId;
                 data.ModifiedDate = DateTime.Now;
                 StatementFact statement = new StatementFact();
                 Entity entity = GetEnties(_entityCode);
                 CompanyVO nextStepCompany = GetUserDetails(_UserId);
                 statement = StatementFactMapExtension.MapToEntity(data);   
                                
                 List<StatementFactEvent> _StatementFactEvents = statement.StatementFactEvents.ToList();
                 statement.StatementFactEvents = null;

                 List<StatementCommodity> _StatementCommodities = statement.StatementCommodities.ToList();
                 statement.StatementCommodities = null;         

                 statement.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<StatementFact>().Insert(statement);
                 _unitOfWork.SaveChanges();

                 List<StatementFactEvent> _StatementFactEventList = new List<StatementFactEvent>();
                 foreach (var state in _StatementFactEvents)
                 {
                     StatementFactEvent _statementfactevent = new StatementFactEvent();

                     _statementfactevent.StatementFactID = statement.StatementFactID;
                    // _statementfactevent.StatementFactEventID = 0;
                     _statementfactevent.DelayType = state.DelayType;
                     _statementfactevent.StartOperational = state.StartOperational;
                     _statementfactevent.EndOperational = state.EndOperational;
                     _statementfactevent.Duration = state.Duration;
                     _statementfactevent.Remarks = state.Remarks;
                     _statementfactevent.RecordStatus = data.RecordStatus;
                     _statementfactevent.CreatedBy = data.CreatedBy;
                     _statementfactevent.CreatedDate = data.CreatedDate;
                     _statementfactevent.ModifiedBy = data.ModifiedBy;
                     _statementfactevent.ModifiedDate = data.ModifiedDate;

                     _StatementFactEventList.Add(_statementfactevent);

                 }
                 _unitOfWork.Repository<StatementFactEvent>().InsertRange(_StatementFactEventList);
                 _unitOfWork.SaveChanges();


                 List<StatementCommodity> _StatementCommodityList = new List<StatementCommodity>();
                 foreach (var state in _StatementCommodities)
                 {
                     StatementCommodity _statementcommodity = new StatementCommodity();

                     _statementcommodity.StatementFactID = statement.StatementFactID;
                     _statementcommodity.TerminalOperatorID = state.TerminalOperatorID;
                     _statementcommodity.PortCode = state.PortCode;
                     _statementcommodity.QuayCode = state.QuayCode;
                     _statementcommodity.BerthCode = state.BerthCode;
                     _statementcommodity.Commodity = state.Commodity;
                     _statementcommodity.CargoType = state.CargoType;
                     _statementcommodity.Package = state.Package;
                     _statementcommodity.UOM = state.UOM;
                     _statementcommodity.Quantity = state.Quantity;
                     _statementcommodity.RecordStatus = data.RecordStatus;
                     _statementcommodity.CreatedBy = data.CreatedBy;
                     _statementcommodity.CreatedDate = data.CreatedDate;
                     _statementcommodity.ModifiedBy = data.ModifiedBy;
                     _statementcommodity.ModifiedDate = data.ModifiedDate;

                     _StatementCommodityList.Add(_statementcommodity);

                 }
                 _unitOfWork.Repository<StatementCommodity>().InsertRange(_StatementCommodityList);
                 _unitOfWork.SaveChanges(); 


                 _notificationpublisher.Publish(entity.EntityID, statement.StatementFactID.ToString(System.Globalization.CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
          
                 return data;
             });
         }

         /// <summary>
         /// Modify Statement Of Fact
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         public StatementVCNVO ModifyStatementFact(StatementVCNVO data)
         {
             return EncloseTransactionAndHandleException(() =>
             {
                 data.ModifiedBy = _UserId;
                 data.ModifiedDate = DateTime.Now;
                 StatementFact statement = new StatementFact();
                 Entity entity = GetEnties(_entityCode);
                 CompanyVO nextStepCompany = GetUserDetails(_UserId);
                 statement = StatementFactMapExtension.MapToEntity(data);
                

                 List<StatementFactEvent> _StatementFactEvents = statement.StatementFactEvents.ToList();
                 statement.StatementFactEvents = null;

                 var sta = _unitOfWork.ExecuteSqlCommand("delete dbo.StatementFactEvent where StatementFactID =  @p0", statement.StatementFactID);

                 List<StatementCommodity> _StatementCommodities = statement.StatementCommodities.ToList();
                 statement.StatementCommodities = null;


                 var stacom = _unitOfWork.ExecuteSqlCommand("delete dbo.StatementCommodity where StatementFactID =  @p0", statement.StatementFactID);


                  List<StatementFactEvent> _StatementFactEventList = new List<StatementFactEvent>();

                 if (_StatementFactEvents.Count > 0)
                 {
                     foreach (var statementFactEvent in _StatementFactEvents)
                     {
                         statementFactEvent.StatementFactID = data.StatementFactID;                        
                         statementFactEvent.CreatedBy = data.CreatedBy;
                         statementFactEvent.CreatedDate = data.CreatedDate;
                         statementFactEvent.ModifiedBy = data.ModifiedBy;
                         statementFactEvent.ModifiedDate = data.ModifiedDate;
                         statementFactEvent.RecordStatus = data.RecordStatus;
                         statementFactEvent.DelayType = statementFactEvent.DelayType;
                         statementFactEvent.StartOperational = statementFactEvent.StartOperational;
                         statementFactEvent.EndOperational = statementFactEvent.EndOperational;
                         statementFactEvent.Duration = statementFactEvent.Duration;
                         statementFactEvent.Remarks = statementFactEvent.Remarks;
                         _StatementFactEventList.Add(statementFactEvent);                        

                     }
                     _unitOfWork.Repository<StatementFactEvent>().InsertRange(_StatementFactEventList);
                     _unitOfWork.SaveChanges(); 
                 }             


                 List<StatementCommodity> _StatementCommodityList = new List<StatementCommodity>();

                 if (_StatementCommodities.Count > 0)
                 {
                     foreach (var state in _StatementCommodities)
                     {
                         StatementCommodity _statementcommodity = new StatementCommodity();

                         _statementcommodity.StatementFactID = statement.StatementFactID;
                         _statementcommodity.TerminalOperatorID = state.TerminalOperatorID;
                         _statementcommodity.PortCode = state.PortCode;
                         _statementcommodity.QuayCode = state.QuayCode;
                         _statementcommodity.BerthCode = state.BerthCode;
                         _statementcommodity.Commodity = state.Commodity;
                         _statementcommodity.CargoType = state.CargoType;
                         _statementcommodity.Package = state.Package;
                         _statementcommodity.UOM = state.UOM;
                         _statementcommodity.Quantity = state.Quantity;
                         _statementcommodity.RecordStatus = data.RecordStatus;
                         _statementcommodity.CreatedBy = data.CreatedBy;
                         _statementcommodity.CreatedDate = DateTime.Now;
                         _statementcommodity.ModifiedBy = _UserId;
                         _statementcommodity.ModifiedDate = DateTime.Now;

                         _StatementCommodityList.Add(_statementcommodity);

                     }
                     _unitOfWork.Repository<StatementCommodity>().InsertRange(_StatementCommodityList);
                     _unitOfWork.SaveChanges(); 
                 }

                 _unitOfWork.Repository<StatementFact>().Update(statement);
                 _unitOfWork.SaveChanges();
                 


                 _notificationpublisher.Publish(entity.EntityID, statement.StatementFactID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                 return data;
            
             });
         }


         /// <summary>
         ///  To Get Statement Fact Details
         /// </summary>
         /// <returns></returns>
         public List<StatementVCNVO> StatementFactDetails(string vcnSearch, string vesselName)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 CompanyVO nextStepCompany = _statementfactRepository.GetUserDetails(_UserId);
                 var UserTypeID = nextStepCompany.UserTypeId;
                 var UserType = nextStepCompany.UserType;
                 return _statementfactRepository.StatementFactDetails(_PortCode, UserTypeID, UserType, vcnSearch, vesselName);
             });
         }

         /// <summary>
         /// To Get Reference Data
         /// </summary>
         /// <returns></returns>
         public StatementFactReferenceDataVO GetStatementFactReferenceDataVO()
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 
                 StatementFactReferenceDataVO _statementfactreferenceVO = new StatementFactReferenceDataVO();
                 _statementfactreferenceVO.Ports = _portRepository.GetPorts();
                 _statementfactreferenceVO.Operations = _subcategoryRepository.GetOperations();
                 _statementfactreferenceVO.DelayTypes = _subcategoryRepository.GetDelayTypes();


                 _statementfactreferenceVO.Berths = _berthRepository.GetBerthsForArrival(_PortCode).MapToDtoforArrivalBerths();
                  var cargotypes = _subcategoryRepository.CargoTypes();
                 _statementfactreferenceVO.CargoTypes = cargotypes.MapToDtoCodeName();
                 _statementfactreferenceVO.Purpose = _subcategoryRepository.Purpose().MapToDtoCodeName();
                 _statementfactreferenceVO.Uoms = _subcategoryRepository.CargoUoms().MapToDtoCodeName();
                 _statementfactreferenceVO.Commodities = _subcategoryRepository.Commoditys().MapToDtoCodeName();
                 _statementfactreferenceVO.TerminalOperators = _terminalOperatorRepository.GetTerminalOperators(_PortCode);


                 return _statementfactreferenceVO;

             });
         }

         /// <summary>
         ///  To Get Key Event Types
         /// </summary>
         /// <returns></returns>
         public List<SubCategoryVO> GetKeyEventTypes()
         {
             return ExecuteFaultHandledOperation(() =>
             {               
                 SubCategoryRepository repository = new SubCategoryRepository(_unitOfWork);
                 List<SubCategory> subcategories = repository.GetKeyEvents();
                 return subcategories.MapToDto();
             });
         }

         /// <summary>
         /// To Get VCN Details
         /// </summary>
         /// <returns></returns>
         public List<StatementVCNVO> GetStatementVCNS(string searchValue)
         {
             return ExecuteFaultHandledOperation(() =>
             {
                 CompanyVO nextStepCompany = _statementfactRepository.GetUserDetails(_UserId);
                 var UserTypeID = nextStepCompany.UserTypeId;
                 var UserType = nextStepCompany.UserType;
                 return _statementfactRepository.GetStatementVCNS(_PortCode, UserTypeID, UserType, searchValue);

             });
         }

         public StatementVCNVO GetVesselByVCN(string VCN)
         {
             return _statementfactRepository.GetVesselByVCN(VCN);
         }

         public StatementVCNVO GetTugsByVCN(string VCN)
         {
             return _statementfactRepository.GetTugsByVCN(VCN);
         }
    }
}
