using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Security.Cryptography;
using System.IO;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Data.Entity.SqlServer;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
           ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AutomatedSlotConfigurationService : ServiceBase, IAutomatedSlotConfigurationService
    {
        //private IWorkFlowEngine<VesselRegistrationWorkFlow> wfEngine;
     //   private IAccountRepository _accountRepository;
        private ISubCategoryRepository _subCategoryRepository;
        private IAutomatedSlotConfigRepository _automatedslotconfigrepository;
        public AutomatedSlotConfigurationService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
          //  _accountRepository = new AccountRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _automatedslotconfigrepository = new AutomatedSlotConfigRepository(_unitOfWork);
        }

        public AutomatedSlotConfigurationService(IUnitOfWork unitofWork)
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
           // _accountRepository = new AccountRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _automatedslotconfigrepository = new AutomatedSlotConfigRepository(_unitOfWork);
        }


        public List<AutomatedSlotConfigurationVO> GetAutomatedSlotConfigList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portid = _PortCode;
                return _automatedslotconfigrepository.GetAutomatedSlotConfigurationDetails(portid).MapToDTO();
            });
        }


        public AutomatedSlotConfigurationVO UpdateAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data)
        {
            return EncloseTransactionAndHandleException(() =>
           {

               AutomatedSlotConfiguration AutomatedSlot = new AutomatedSlotConfiguration();
               AutomatedSlot = data.MapToEntity();
               AutomatedSlot.CreatedBy = _UserId;
               AutomatedSlot.CreatedDate = DateTime.Now;
               AutomatedSlot.ModfiedBy = _UserId;
               AutomatedSlot.ModifiedDate = DateTime.Now;
               AutomatedSlot.RecordStatus = "A";
               AutomatedSlot.PortCode = _PortCode;

                DateTime opTime = Convert.ToDateTime(AutomatedSlot.OperationalPeriod, CultureInfo.InvariantCulture);
                AutomatedSlot.OperationalPeriod = Convert.ToString(opTime.TimeOfDay.TotalMinutes, CultureInfo.InvariantCulture);

               List<SlotPriorityConfiguration> SlotPriorityConfigurations = data.SlotPriorityConfigurations.MapToEntity();
               AutomatedSlot.SlotPriorityConfigurations = null;
               AutomatedSlot.ObjectState = ObjectState.Modified;
               _unitOfWork.Repository<AutomatedSlotConfiguration>().Update(AutomatedSlot);
               _unitOfWork.SaveChanges();

               //var brt = _unitOfWork.ExecuteSqlCommand(" update dbo.SlotPriorityConfiguration SET RecordStatus =  @p0 where SlotCofiguratinid = @p1", "I", AutoslotDetails.SlotCofiguratinid);

               _unitOfWork.ExecuteSqlCommand(" Delete dbo.SlotPriorityConfiguration where SlotCofiguratinid = @p0", data.SlotCofiguratinid);

               if (SlotPriorityConfigurations.Count > 0)
               {
                   foreach (var SlotPriorityConfiguration in SlotPriorityConfigurations)
                   {
                       SlotPriorityConfiguration.SlotCofiguratinid = AutomatedSlot.SlotCofiguratinid;
                       SlotPriorityConfiguration.RecordStatus = "A";
                       SlotPriorityConfiguration.ObjectState = ObjectState.Modified;

                   }
                   _unitOfWork.Repository<SlotPriorityConfiguration>().InsertRange(SlotPriorityConfigurations);
                   _unitOfWork.SaveChanges();
               }
               //_unitOfWork.Commit();
               

               data = AutomatedSlot.MapToDTO();
               return data;
           });
        }

        public AutomatedSlotConfigurationVO SaveAutomatedSlotConfigDetails(AutomatedSlotConfigurationVO data)
        {
             return EncloseTransactionAndHandleException(() =>
            {
                //string name = _LoginName;             
              
                AutomatedSlotConfiguration AutomatedSlot = new AutomatedSlotConfiguration();
                AutomatedSlot = data.MapToEntity();
                AutomatedSlot.CreatedBy = _UserId;
                AutomatedSlot.CreatedDate = DateTime.Now;
                AutomatedSlot.ModfiedBy = _UserId;
                AutomatedSlot.ModifiedDate = DateTime.Now;
                AutomatedSlot.RecordStatus = "A";
                AutomatedSlot.PortCode = _PortCode;

                DateTime opTime = Convert.ToDateTime(AutomatedSlot.OperationalPeriod, CultureInfo.InvariantCulture);
                AutomatedSlot.OperationalPeriod = Convert.ToString(opTime.TimeOfDay.TotalMinutes, CultureInfo.InvariantCulture);

                List<SlotPriorityConfiguration> SlotPriorityConfigurations = data.SlotPriorityConfigurations.MapToEntity();
                AutomatedSlot.SlotPriorityConfigurations = null;
                AutomatedSlot.User = null;
                AutomatedSlot.User1 = null;       

                AutomatedSlot.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<AutomatedSlotConfiguration>().Insert(AutomatedSlot);
                _unitOfWork.SaveChanges();

                foreach (var SlotPriorityConfiguration in SlotPriorityConfigurations)
                {
                    SlotPriorityConfiguration.SlotCofiguratinid = AutomatedSlot.SlotCofiguratinid;
                    SlotPriorityConfiguration.RecordStatus = "A";
                    _unitOfWork.Repository<SlotPriorityConfiguration>().Insert(SlotPriorityConfiguration);
                    _unitOfWork.SaveChanges();
                }
               // _unitOfWork.Commit();
                data = AutomatedSlot.MapToDTO();
                return data;
            });
        }
        


        public AutomatedSlotConfigurationReferenceDataVO GetAutomatedSlotConfigurationReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                AutomatedSlotConfigurationReferenceDataVO VO = new AutomatedSlotConfigurationReferenceDataVO();
                VO.VesselType = _subCategoryRepository.VesselTypes().MapToDto();

                VO.PrioprtySeqList = _automatedslotconfigrepository.PrioprtySeqList();

                //  VO.SlotpriorityDetails = _automatedslotconfigrepository.GetSlotPriorityConfigurationDetails().MapToDTO();

                return VO;
            });
        }
    }
}
