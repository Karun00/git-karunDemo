using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


namespace IPMS.Services
{

   [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
   public class BollardService : ServiceBase,IBollardService
    {
       private IBollardRepository _bollardRepository;
       
        public BollardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bollardRepository = new BollardRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }


        public BollardService()  
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _bollardRepository = new BollardRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);

        }

       /// <summary>
        /// To Get Quays based on Port
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public List<QuayVO> GetPortQuays(string id) 
        {
            return ExecuteFaultHandledOperation(() =>
            {
                if(string.IsNullOrEmpty(id)){
                id= _PortCode;
                }
                return _bollardRepository.GetPortQuays(id);
            });      
        }

       /// <summary>
        /// To Get Berths based on Port and Quay
       /// </summary>
       /// <param name="portCode"></param>
       /// <param name="quayCode"></param>
       /// <returns></returns>
        public List<BerthVO> GetQuayBerths(string portCode,string quayCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _bollardRepository.GetQuayBerths(portCode, quayCode);
            });   
        }

       /// <summary>
        /// To get Bollards based on port, quay and berth
       /// </summary>
       /// <param name="portCode"></param>
       /// <param name="quayCode"></param>
       /// <param name="berthCode"></param>
       /// <returns></returns>
        public List<BollardVO> GetBollardsInBerths(string portCode,string quayCode,string berthCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _bollardRepository.GetBollardsInBerths(portCode, quayCode, berthCode);
            });   
           
        }

       /// <summary>
        /// To Get Bollard Details
       /// </summary>
       /// <returns></returns>
        public List<BollardVO> GetBollardDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _bollardRepository.GetBollardDetails(_PortCode);
            });   
           
        }

       /// <summary>
       /// Add Bollard
       /// </summary>
       /// <param name="bollardData"></param>
       /// <returns></returns>
        public BollardVO AddBollard(BollardVO bollardData)
        {
            return EncloseTransactionAndHandleException(() =>
            {                             
                bollardData.CreatedBy = _UserId;
                bollardData.CreatedDate = DateTime.Now;
                bollardData.ModifiedBy = _UserId;
                bollardData.ModifiedDate = DateTime.Now;
                Bollard obj = new Bollard();
                obj = BollardMapExtension.MapToEntity(bollardData);
                obj.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Bollard>().Insert(obj);
                _unitOfWork.SaveChanges();
                return bollardData;
            });
        }


       /// <summary>
       /// Modify bollard
       /// </summary>
       /// <param name="bollardData"></param>
       /// <returns></returns>
        public BollardVO ModifyBollard(BollardVO bollardData)
        {
            return EncloseTransactionAndHandleException(() =>
            {                    
                bollardData.ModifiedBy = _UserId;
                bollardData.ModifiedDate = DateTime.Now;
                Bollard obj = new Bollard();
                obj = BollardMapExtension.MapToEntity(bollardData);
                obj.ObjectState = ObjectState.Added;               
                _unitOfWork.Repository<Bollard>().Update(obj);
                _unitOfWork.SaveChanges();
                return bollardData;
            });
        }

       /// <summary>
       /// To Delete Bollard By Id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public BollardVO DelBollardById(long id)
        {
            var bollardObj = _unitOfWork.Repository<Bollard>().Find(id);
            bollardObj.RecordStatus = "I";
            bollardObj.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Bollard>().Update(bollardObj);
            _unitOfWork.SaveChanges();

            BollardVO bollardvo = new BollardVO();
            bollardvo = BollardMapExtension.MapToDTO(bollardObj);
              string portname = (from p in _unitOfWork.Repository<Port>().Query().Select()
                                   where p.PortCode == bollardObj.PortCode
                                   select new { p.PortName }).SingleOrDefault().ToString();
              string quayname = (from p in _unitOfWork.Repository<Quay>().Query().Select()
                                   where p.QuayCode == bollardObj.QuayCode
                                   select new { p.QuayName }).SingleOrDefault().ToString();
              string berthname = (from p in _unitOfWork.Repository<Berth>().Query().Select()
                                   where p.BerthCode == bollardObj.BerthCode
                                   select new { p.BerthName }).SingleOrDefault().ToString();
              bollardvo.PortName = portname;
              bollardvo.QuayName = quayname;
              bollardvo.BerthName = berthname;
            return bollardvo;
        }

       /// <summary>
       /// To Get Bollards By Id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public BollardVO GetBollardById(string id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _bollardRepository.GetBollardById(id);
            }); 
        }


    }
}
