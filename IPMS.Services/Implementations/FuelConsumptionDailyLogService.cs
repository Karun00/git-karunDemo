using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class FuelConsumptionDailyLogService : ServiceBase, IFuelConsumptionDailyLogService
    {
        private IFuelConsumptionDailyLogRepository _FuelConsumptionDailyLogRepository;
     //   private readonly IUnitOfWork _unitOfWork;
        public FuelConsumptionDailyLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _FuelConsumptionDailyLogRepository = new FuelConsumptionDailyLogRepository(_unitOfWork);
        }


        public FuelConsumptionDailyLogService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _FuelConsumptionDailyLogRepository = new FuelConsumptionDailyLogRepository(_unitOfWork);
        }


        /// <summary>
        /// Gets Crafts list
        /// </summary>
        /// <returns></returns>
        public List<CraftVO> GetCraftDetails(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _FuelConsumptionDailyLogRepository.GetCraftDetails(searchValue);
            });
        }



        /// <summary>
        /// Gets User id by logged in user name
        /// </summary>
        /// <param name="_LoginName"></param>
        /// <returns></returns>
        //public int GetUserID(string _LoginName)
        //{

        //    var user = (from u in _unitOfWork.Repository<User>().Query().Select()
        //                where u.UserName == _LoginName
        //                select u).FirstOrDefault<User>();
        //    return user.UserID;

        //}




        public List<FuelConsumptionDailyLogVO> GetAllFuelConsumptionDailyLogDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string portCode = _PortCode;
                return _FuelConsumptionDailyLogRepository.GetAllFuelConsumptionDailyLogDetails(portCode).MapToDTO();
            });
        }



        /// <summary>
        ///  Add / Inserts the new Fuel Consumption Daily Log
        /// </summary>
        /// <param name="fuelconsumptiondailylog"></param>
        /// <returns></returns>
        public FuelConsumptionDailyLogVO AddFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                data.CraftID = data.Crafts.CraftID;

                FuelConsumptionDailyLog fuelconsumptiondailylog = null;
                fuelconsumptiondailylog = data.MapToEntity();
                fuelconsumptiondailylog.ObjectState = ObjectState.Added;
                fuelconsumptiondailylog.CreatedBy = _UserId;
                fuelconsumptiondailylog.CreatedDate = DateTime.Now;
                fuelconsumptiondailylog.ModifiedBy = _UserId;
                fuelconsumptiondailylog.ModifiedDate = DateTime.Now;
                fuelconsumptiondailylog.PortCode = _PortCode;
                fuelconsumptiondailylog.RecordStatus = "A";
                _unitOfWork.Repository<FuelConsumptionDailyLog>().Insert(fuelconsumptiondailylog);
                _unitOfWork.SaveChanges();
                data = fuelconsumptiondailylog.MapToDto();
                return data;
            });
        }

        /// <summary>
        /// Modify Fuel Consumption Daily Log
        /// </summary>
        /// <param name="fuelconsumptiondailylog"></param>
        /// <returns></returns>
        public FuelConsumptionDailyLogVO ModifyFuelConsumptionDailyLog(FuelConsumptionDailyLogVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                data.CraftID = data.Crafts.CraftID;

                FuelConsumptionDailyLog fuelconsumptiondailylog = null;
                fuelconsumptiondailylog = data.MapToEntity();
                fuelconsumptiondailylog.ObjectState = ObjectState.Modified;
                fuelconsumptiondailylog.CreatedBy = _UserId;
                fuelconsumptiondailylog.CreatedDate = DateTime.Now;
                fuelconsumptiondailylog.ModifiedBy = _UserId;
                fuelconsumptiondailylog.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<FuelConsumptionDailyLog>().Update(fuelconsumptiondailylog);
                _unitOfWork.SaveChanges();
                data = fuelconsumptiondailylog.MapToDto();
                return data;
            });
        }


        public List<FuelConsumptionDailyLogVO> GetFuelConsumptionDailyLoggridDetails(int craftId)
        {
            return ExecuteFaultHandledOperation(() =>
            {              
                return _FuelConsumptionDailyLogRepository.GetFuelConsumptionDailyLoggridDetails(craftId).MapToDTO();
            });
        }
    }
}
