using System.Collections.Generic;
using System.Linq;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System.ServiceModel;
using System;

namespace IPMS.Services
{
     [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class ShiftService: ServiceBase,IShiftService
    {

        public ShiftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ShiftService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

         /// <summary>
         /// To Add new Shift
         /// </summary>
         /// <param name="shiftdata"></param>
         /// <returns></returns>
        public ShiftVO AddShift(ShiftVO shiftdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;

                int UserID = GetUserId(name);
                Shift shift = null;
                shift = shiftdata.MapToEntity();
                //if (shift.IsShiftOff == "True")
                //    shift.IsShiftOff = "Y";
                //else
                //    shift.IsShiftOff = "N";
                shift.ObjectState = ObjectState.Added;
                shift.CreatedBy = UserID;
                shift.CreatedDate = DateTime.Now;
                shift.ModifiedBy = UserID;
                shift.ModifiedDate = DateTime.Now;
                shift.PortCode = _PortCode;
                shift.RecordStatus = "A";
                _unitOfWork.Repository<Shift>().Insert(shift);
                _unitOfWork.SaveChanges();
                shiftdata = shift.MapToDTO();
                return shiftdata;
            });
        }

         /// <summary>
         /// To Modify Shift
         /// </summary>
         /// <param name="shiftdata"></param>
         /// <returns></returns>
        public ShiftVO ModifyShift(ShiftVO shiftdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string name = _LoginName;
                int UserID = GetUserId(name);
                Shift shift = null;
                shift = shiftdata.MapToEntity();
                //if (shift.IsShiftOff == "True")
                //    shift.IsShiftOff = "Y";
                //else
                //    shift.IsShiftOff = "N";
                shift.ObjectState = ObjectState.Modified;
                shift.ObjectState = ObjectState.Added;
                //shift.CreatedBy = UserID;
                //shift.CreatedDate = DateTime.Now;
                shift.ModifiedBy = UserID;
                shift.ModifiedDate = DateTime.Now;
                shift.RecordStatus = shift.RecordStatus;
                _unitOfWork.Repository<Shift>().Update(shift);
                _unitOfWork.SaveChanges();
                shiftdata = shift.MapToDTO();
                return shiftdata;
            });
        }

         /// <summary>
         /// To get All Shift Data
         /// </summary>
         /// <returns></returns>
        public List<ShiftVO> GetShiftList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var shiftdata = _unitOfWork.Repository<Shift>().Queryable().OrderByDescending(x => x.ShiftID).Where(x => x.PortCode == _PortCode).OrderByDescending(x => x.ModifiedDate).ToList<Shift>();
                return shiftdata.MapToDTO();
            });
        }

         /// <summary>
         /// To get User Id
         /// </summary>
         /// <param name="loginName"></param>
         /// <returns></returns>
        public int GetUserId(string loginName)
        {

            var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                        where u.UserName == loginName
                        select u).FirstOrDefault<User>();
            return user.UserID;

        }
    }
}
