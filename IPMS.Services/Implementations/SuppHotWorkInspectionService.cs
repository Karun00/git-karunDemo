using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using System.Web.Mvc;
using IPMS.Services.WorkFlow;
using Core.Repository;
using IPMS.Domain.DTOS;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
            ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SuppHotWorkInspectionService : ServiceBase, ISuppHotWorkInspectionService
    {
         ISuppHotWorkInspectionRepository _SuppHotWorkInspectionRepository = null;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="unitOfWork"></param>
  
        public SuppHotWorkInspectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _SuppHotWorkInspectionRepository = new SuppHotWorkInspectionRepository(_unitOfWork);
        }
        /// <summary>
        /// 
        /// </summary>
        public SuppHotWorkInspectionService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _SuppHotWorkInspectionRepository = new SuppHotWorkInspectionRepository(_unitOfWork);
            // TODO: Complete member initialization
        }

         /// <summary>
        /// Gets 
        /// </summary>
        /// <returns></returns>
        public List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                return _SuppHotWorkInspectionRepository.AllSuppHotWorkInspectionDetails();
            });

           
        }


        /// <summary>
        /// adds / inserts the external diving register details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>

        public SuppHotWorkInspectionVO AddSuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                //string name = _LoginName;
                int UserID = _UserId;//GetUserID(name);
                SuppHotWorkInspection SuppHotWorkInspection = new SuppHotWorkInspection();
                SuppHotWorkInspection = SuppHotWorkInspectionMapExtension.MapToEntity(SuppHotWorkInspectiondata);
                SuppHotWorkInspection.CreatedBy = UserID;
                SuppHotWorkInspection.CreatedDate = System.DateTime.Now;

                SuppHotWorkInspection.ModifiedBy = UserID;
                SuppHotWorkInspection.ModifiedDate = System.DateTime.Now;
                SuppHotWorkInspection.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<SuppHotWorkInspection>().Insert(SuppHotWorkInspection);
                _unitOfWork.SaveChanges();
                return SuppHotWorkInspectiondata;
            });
        }

        /// <summary>
        /// gets the user id by user name
        /// </summary>
        /// <param name="_LoginName"></param>
        /// <returns></returns>
        public int GetUserID(string LogOn)
        {

            var user = (from u in _unitOfWork.Repository<User>().Query().Select()
                        where u.UserName == LogOn
                        select u).FirstOrDefault<User>();
            return user.UserID;

        }

        /// <summary>
        /// Modifies / update the External diving register details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>
        //public SuppHotWorkInspectionVO ModifySuppHotWorkInspection(SuppHotWorkInspectionVO SuppHotWorkInspectiondata)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        string name = _LoginName;
        //        int UserID = GetUserID(name);
        //        SuppHotWorkInspection SuppHotWorkInspection = new SuppHotWorkInspection();


        //        SuppHotWorkInspection = SuppHotWorkInspectionMapExtension.MapToEntity(SuppHotWorkInspectiondata);
        //        SuppHotWorkInspection.ModifiedBy = UserID;
        //        SuppHotWorkInspection.ModifiedDate = System.DateTime.Now;

        //        SuppHotWorkInspection.ObjectState = ObjectState.Added;
        //        _unitOfWork.Repository<SuppHotWorkInspection>().Insert(SuppHotWorkInspection);
        //        _unitOfWork.SaveChanges();
        //        return SuppHotWorkInspectiondata;
        //    });
        //}

        public SuppServiceRequestVO ModifySuppHotWorkInspection(SuppServiceRequestVO SuppHotWorkInspectiondata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
               // string name = _LoginName;
               //// if (name != "")
               // if (string.IsNullOrEmpty(name))
               // {
               //     name = "admin";
               // }
                int UserID = _UserId;//GetUserID(name);
              //  int UserID = GetUserIdByLoginname(_LoginName);
               
                SuppHotWorkInspection suppHotWorkInspection = suppHotWorkInspection = SuppHotWorkInspectiondata.SuppHotWorkInspectionVO.MapToEntity();
                
                suppHotWorkInspection.ModifiedBy = UserID;
                suppHotWorkInspection.ModifiedDate = System.DateTime.Now;
                if (SuppHotWorkInspectiondata.ActionName=="Update")
                suppHotWorkInspection.PermitStatus = "Issued";
                else if (SuppHotWorkInspectiondata.ActionName=="Verify")
                    suppHotWorkInspection.PermitStatus = "Verified";

                if (suppHotWorkInspection.SuppHotWorkInspectionID == 0)
                {
                    suppHotWorkInspection.SuppServiceRequestID = SuppHotWorkInspectiondata.SuppServiceRequestID;
                    suppHotWorkInspection.HWPN = GenerateHWPN();
                    suppHotWorkInspection.CreatedBy = UserID;
                    suppHotWorkInspection.CreatedDate = System.DateTime.Now;
                    suppHotWorkInspection.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<SuppHotWorkInspection>().Insert(suppHotWorkInspection);
                }
                else
                {
                    suppHotWorkInspection.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<SuppHotWorkInspection>().Update(suppHotWorkInspection);
                }
                _unitOfWork.SaveChanges();
                return SuppHotWorkInspectiondata;
            });
        }
        /// <summary>
        /// Deletes external diving register - not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuppHotWorkInspectionVO DeleteSuppHotWorkInspection(long id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                // throw new NotImplementedException();
                var SuppHotWorkInspectionObj = _unitOfWork.Repository<SuppHotWorkInspection>().Find(id);
                SuppHotWorkInspectionObj.RecordStatus = "I";
                SuppHotWorkInspectionObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SuppHotWorkInspection>().Update(SuppHotWorkInspectionObj);
                _unitOfWork.SaveChanges();

                SuppHotWorkInspectionVO SuppHotWorkInspection = SuppHotWorkInspectionMapExtension.MapToDTO(SuppHotWorkInspectionObj);

                return SuppHotWorkInspection;
            });

        }

        public string ModifyHotWorkInspectionPermitStatus(long id)
        {
            string Result = "Failure";
            Result = ExecuteFaultHandledOperation(() =>
            {
                SuppHotWorkInspection _objSupp = (from s in _unitOfWork.Repository<SuppHotWorkInspection>().Query().Select()
                                                     .Where(s => s.SuppHotWorkInspectionID == id)
                                                  select s).First();
                _objSupp.PermitStatus = "Verified";
               
                _objSupp.ModifiedDate = DateTime.Now;
                _objSupp.ModifiedBy = _UserId;
                _objSupp.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SuppHotWorkInspection>().Update(_objSupp);
                _unitOfWork.SaveChanges();
                return "Success";

            });
            
             return Result;
        }

        /// <summary>
        /// function to auto generate DRN Number
        /// </summary>
        /// <returns></returns>
        public string GenerateHWPN()
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;

            StringBuilder HWPN = new StringBuilder();

            int count = (from a in _unitOfWork.Repository<SuppHotWorkInspection>().Query().Select()

                         select a).Count();

            count = count + 1;
            string strCount = count.ToString(CultureInfo.InvariantCulture);
            string stMonth = Month.ToString(CultureInfo.InvariantCulture);
            string stDay = Day.ToString(CultureInfo.InvariantCulture);

            if (count <= 9)
                strCount = "0" + count.ToString(CultureInfo.InvariantCulture);

            if (Month <= 9)
                stMonth = "0" + Month.ToString(CultureInfo.InvariantCulture);
            if (Day <= 9)
                stDay = "0" + Day.ToString(CultureInfo.InvariantCulture);

            HWPN.Append("HWPN" + _PortCode.ToString() + Year.ToString(CultureInfo.InvariantCulture) + stMonth + stDay + strCount.ToString());

            return HWPN.ToString();
        }
    }
}
