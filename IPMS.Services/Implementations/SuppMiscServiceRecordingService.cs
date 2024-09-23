using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
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
    public class SuppMiscServiceRecordingService : ServiceBase, ISuppMiscServiceRecordingService
    {
      //  private IPortConfigurationRepository _portConfigurationRepository;
        private ISubCategoryRepository _subcategoryRepository;
        private ISuppMiscServiceRepository _suppMiscServiceRepository;

        
        public SuppMiscServiceRecordingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;        
            _suppMiscServiceRepository = new SuppMiscServiceRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public SuppMiscServiceRecordingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _suppMiscServiceRepository = new SuppMiscServiceRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);    
            _UserId = GetUserIdByLoginname(_LoginName);                
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Get Miscellaneous Details
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> SuppMiscServiceDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppMiscServiceRepository.GetSuppMiscServiceDetails(_PortCode);
            });
        }

        /// <summary>
        /// To Get Reference Data While initialization  
        /// </summary>
        /// <returns></returns>
        public SuppMiscServiceVO GetSuppMiscReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SuppMiscServiceVO VO = new SuppMiscServiceVO();
                VO.MiscServiceTypes = _suppMiscServiceRepository.GetServiceTypes();
                VO.PhaseTypes = _subcategoryRepository.GetPhaseTypes().MapToDto();
           
                return VO;
            });
        }

        /// <summary>
        /// To Get Miscellaneous Details
        /// </summary>
        /// <returns></returns>
        public List<SuppMiscServiceVO> SuppMiscServiceRecordingDetails(int SuppDryDockID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _suppMiscServiceRepository.GetSuppMiscServiceRecordDetails(SuppDryDockID);
            });
        }



        public SuppMiscServiceVO ModifySuppMiscServiceRecord(SuppMiscServiceVO data)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                data.CreatedBy = _UserId;
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = _UserId;
                data.ModifiedDate = DateTime.Now;
                data.RecordStatus = "A";             
                var suppmiscserviceid = data.SuppMiscServiceID;

                if (suppmiscserviceid == 0)
                {
                    SuppMiscService suppmiscservice = new SuppMiscService();
                    suppmiscservice = SuppMiscServiceMapExtension.MapToEntity(data);
                    suppmiscservice.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<SuppMiscService>().Insert(suppmiscservice);            
                     _unitOfWork.SaveChanges();
                }
                else
                {

                    var suppmiscservice = _unitOfWork.Repository<SuppMiscService>().Find(Convert.ToInt32(data.SuppMiscServiceID));
                    suppmiscservice.FromDateTime = Convert.ToDateTime(data.FromDateTime, CultureInfo.InvariantCulture);
                    suppmiscservice.ToDateTime = Convert.ToDateTime(data.ToDateTime, CultureInfo.InvariantCulture);
                    suppmiscservice.Quantity = data.Quantity;
                    suppmiscservice.Remarks = data.Remarks;
                    suppmiscservice.StartMeterReading = data.StartMeterReading;//added by divya on 30Oct2017
                    suppmiscservice.EndMeterReading = data.EndMeterReading;//added by divya on 30Oct2017
                    suppmiscservice.ModifiedDate = DateTime.Now;
                    data.ModifiedBy = _UserId;
                    suppmiscservice.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<SuppMiscService>().Update(suppmiscservice);
                    _unitOfWork.SaveChanges();

           
                }

                return data;
            });
        }

      

    }
}
