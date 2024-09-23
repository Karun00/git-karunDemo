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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CraftOutOfCommissionService : ServiceBase, ICraftOutOfCommissionService
    {
        private ICraftOutOfCommissionRepository _craftoutofcommRepository;
        private INotificationPublisher _notificationpublisher;
        private IPortConfigurationRepository _portConfigurationRepository;
       
        private const string _entityCodeCO = EntityCodes.CraftOut_Commision;
        private const string _entityCodeCI = EntityCodes.CraftIn_Commision;

        public CraftOutOfCommissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _craftoutofcommRepository = new CraftOutOfCommissionRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public CraftOutOfCommissionService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _craftoutofcommRepository = new CraftOutOfCommissionRepository(_unitOfWork);
            _notificationpublisher = new NotificationPublisher(_unitOfWork);
            _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Add Craft Out of Commissions Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CraftOutOfCommissionVO AddCraftOutOfCommission(CraftOutOfCommissionVO entity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Entity entity1 = GetEnties(_entityCodeCO);
               // var portcode = _PortCode;
                CompanyVO nextStepCompany = GetUserDetails(_UserId);

           
                entity.CreatedBy = _UserId;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = _UserId;
                entity.ModifiedDate = DateTime.Now;
                entity.OutOfCommissionDate = DateTime.Now;
                CraftOutOfCommission CraftOutOfCommissions = new CraftOutOfCommission();
                CraftOutOfCommissions = CraftOutOfCommissionMapExtension.MapToEntity(entity);
                _unitOfWork.Repository<CraftOutOfCommission>().Insert(CraftOutOfCommissions);
                var recordst = "I";
                var commstat = "OC";

                _unitOfWork.ExecuteSqlCommand("update Craft  set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftOutOfCommissions.CraftID);
                _unitOfWork.SaveChanges();

                _notificationpublisher.Publish(entity1.EntityID, CraftOutOfCommissions.CraftOutOfCommissionID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, _portConfigurationRepository.GetPortConfiguration(_PortCode).WorkFlowInitialStatus);
                
                return entity;

                //  return _craftoutofcommRepository.AddCraftOutOfComm(entity, _UserId);              

             
            });
        }

        /// <summary>
        /// To Modify Craft Out of Commissions Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CraftOutOfCommissionVO ModifyCraftOutOfCommission(CraftOutOfCommissionVO entity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Entity entity1 = GetEnties(_entityCodeCO);
              //  var portcode = _PortCode;
                CompanyVO nextStepCompany = GetUserDetails(_UserId);


                entity.ModifiedBy = _UserId;
                entity.ModifiedDate = DateTime.Now;
                CraftOutOfCommission CraftOutOfCommissions = new CraftOutOfCommission();
                CraftOutOfCommissions = CraftOutOfCommissionMapExtension.MapToEntity(entity);
                CraftOutOfCommissions.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<CraftOutOfCommission>().Update(CraftOutOfCommissions);
                var recordst = CraftOutOfCommissions.RecordStatus;
                var commstat = CraftOutOfCommissions.CraftCommissionStatus;

                _unitOfWork.ExecuteSqlCommand("update Craft set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftOutOfCommissions.CraftID);
                _unitOfWork.SaveChanges();

                _notificationpublisher.Publish(entity1.EntityID, CraftOutOfCommissions.CraftOutOfCommissionID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, "UPDT");

                return entity;
                // return _craftoutofcommRepository.ModifyCraftOutOfComm(entity, _UserId);
            });
        }

        /// <summary>
        /// To Modify Craft Back to Commissions Data
        /// </summary>
        /// <param name="craftInCommissionData"></param>
        /// <returns></returns>
        public CraftOutOfCommissionVO ModifyCraftInCommission(CraftOutOfCommissionVO craftInCommissionData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Entity entity1 = GetEnties(_entityCodeCI);
                //var portcode = _PortCode;
                CompanyVO nextStepCompany = GetUserDetails(_UserId);


                craftInCommissionData.ModifiedBy = _UserId;
                craftInCommissionData.ModifiedDate = DateTime.Now;
                craftInCommissionData.BackToCommissionDate = DateTime.Now;
                CraftOutOfCommission CraftInCommissions = new CraftOutOfCommission();
                CraftInCommissions = CraftOutOfCommissionMapExtension.MapToEntity(craftInCommissionData);
                CraftInCommissions.RecordStatus = "I";
                _unitOfWork.Repository<CraftOutOfCommission>().Update(CraftInCommissions);
                var recordst = "A";
                var commstat = "IC";

                _unitOfWork.ExecuteSqlCommand("update Craft set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftInCommissions.CraftID);
                _unitOfWork.SaveChanges();

                _notificationpublisher.Publish(entity1.EntityID, CraftInCommissions.CraftOutOfCommissionID.ToString(CultureInfo.InvariantCulture), _UserId, nextStepCompany, _PortCode, "UPDT");

                return craftInCommissionData;
             //   return _craftoutofcommRepository.ModifyCraftInComm(craftincommdata, _UserId);
            });
        }

        /// <summary>
        ///  To Get Craft Out of Commissions Details
        /// </summary>
        /// <returns></returns>
        public List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.CraftOutOfCommissionDetails(_PortCode);
            });
        }

        /// <summary>
        ///  To Get Craft Back to Commissions Details
        /// </summary>
        /// <returns></returns>
        public List<CraftOutOfCommissionVO> CraftInCommissionDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.CraftInCommissionDetails(_PortCode);
            });
        }

        /// <summary>
        ///  To Get Craft Details
        /// </summary>
        /// <returns></returns>
        public List<CraftVO> CraftsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.CraftsDetails(_PortCode);
            });
        }

        /// <summary>
        /// To Get Craft Details based on CraftID
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        public List<CraftVO> CraftsDetailsWithCraftId(int craftId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.CraftsDetailsWithCraftId(craftId, _PortCode);
            });
        }

        /// <summary>
        /// To Get  Reason for Out of Commission Details
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <returns></returns>
        public List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.ReasonForOutOfCommissionDetails(reasonCode);
            });
        }

        /// <summary>
        ///  To Get  Craft Commission Status Details
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<SubCategoryVO> CommissionStatusDetails(string status)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftoutofcommRepository.CommissionStatusDetails(status);
            });
        }

        public Entity GetEnties(string entityCode)
        {
            return _craftoutofcommRepository.GetEntities(entityCode);
        }

      
        public CompanyVO GetUserDetails(int userId)
        {
            return _craftoutofcommRepository.GetUserDetails(_UserId);
        }
    }
}
