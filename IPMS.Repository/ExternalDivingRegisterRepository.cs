using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;
using System.Globalization;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class ExternalDivingRegisterRepository : IExternalDivingRegisterRepository
    {
        private IUnitOfWork _unitOfWork;

        public ExternalDivingRegisterRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region AllExternalDivingRegisterDetails
        /// <summary>
        /// Method to Get All External Diving Register Details by Portcode
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails(string portCode)
        {            
            var AllExternalDivingRegisterDetails = (from q in _unitOfWork.Repository<ExternalDivingRegister>().Queryable()
                                                    join p in _unitOfWork.Repository<Berth>().Queryable()
                                                     on new { a = q.PortCode, b = q.QuayCode, c = q.BerthCode } equals new { a = p.PortCode, b = p.QuayCode, c = p.BerthCode }
                                                    join l in _unitOfWork.Repository<LicenseRequest>().Queryable() on q.CompanyName equals l.LicenseRequestID
                                                     //join v in _unitOfWork.Repository<Vessel>().Queryable() on q.VesselID equals v.VesselID
                                                    join v in _unitOfWork.Repository<Vessel>().Queryable()
                                                     on q.VesselID equals v.VesselID into gj
                                                    from v1 in gj.DefaultIfEmpty()
                                                    
                                                    where q.PortCode == portCode
                                                    orderby q.ModifiedDate descending
                                                    select new ExternalDivingRegisterVO
                                                    {
                                                        ExternalDivingRegisterID = q.ExternalDivingRegisterID,                                                        
                                                        DivingLogDateTime = q.DivingLogDateTime.ToString(),
                                                        CompanyName = q.CompanyName,                                                        
                                                        VesselID = q.VesselID,
                                                        PersonInCharge = q.PersonInCharge,                                                        
                                                        StartTime = q.StartTime.ToString(),
                                                        StopTime = q.StopTime.ToString(),
                                                        OnsiteSupervisorContNo = q.OnsiteSupervisorContNo,
                                                        OffsiteSupervisorContNo = q.OffsiteSupervisorContNo,
                                                        ClearanceNo = q.ClearanceNo,
                                                        NoOfDrivers = q.NoOfDrivers,
                                                        PermissionObtained = q.PermissionObtained,
                                                        RecordStatus = q.RecordStatus,
                                                        CreatedBy = q.CreatedBy,
                                                        CreatedDate = q.CreatedDate,
                                                        ModifiedBy = q.ModifiedBy,
                                                        ModifiedDate = q.ModifiedDate,
                                                        CompanyNameDisplay = l.RegisteredName,                                                                                                         
                                                        BerthKey = p.PortCode + "." + p.QuayCode + "." + p.BerthCode,
                                                        BerthName = p.BerthName,
                                                        VesselName = v1.VesselName,
                                                        isPermissionObtained = q.PermissionObtained == "Y" ? true : false
                                                    }).ToList<ExternalDivingRegisterVO>();
            

            foreach (var item in AllExternalDivingRegisterDetails)
            {
                item.DivingLogDateTime = item.DivingLogDateTime != "" ? Convert.ToDateTime(item.DivingLogDateTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "";
                item.StartTime = item.StartTime != "" ? Convert.ToDateTime(item.StartTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "";
                item.StopTime = item.StopTime != "" ? Convert.ToDateTime(item.StopTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture): "";
            }

            return AllExternalDivingRegisterDetails;
        }
        #endregion

        #region GetAllCompanies
        /// <summary>
        /// Gets all Companies
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<LicenseRequestVO> GetAllCompanies(string portCode)
        {
            var AllCompanies = from lic in _unitOfWork.Repository<LicenseRequest>().Queryable()
                               join lp in _unitOfWork.Repository<LicenseRequestPort>().Queryable() on lic.LicenseRequestID equals lp.LicenseRequestID
                               where lic.RecordStatus == RecordStatus.Active && lic.LicenseRequestType == DivingRequestType.DivingType && lp.PortCode == portCode && lp.WFStatus == WFStatus.Approved
                               select new LicenseRequestVO
                               {
                                   LicenseRequestID = lic.LicenseRequestID,
                                   RegisteredName = lic.RegisteredName
                               };

            return AllCompanies.OrderBy(x => x.RegisteredName).ToList();
        }
        #endregion

        #region GetAllVessels
        /// <summary>
        /// Gets all Vessels
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<VesselVO> GetAllVessels(string portCode)
        {
            var AllVessels = (from vess in _unitOfWork.Repository<Vessel>().Queryable()
                              join an in _unitOfWork.Repository<ArrivalNotification>().Queryable()
                              on vess.VesselID equals an.VesselID
                              join vc in _unitOfWork.Repository<VesselCall>().Queryable()
                              on an.VCN equals vc.VCN
                              where vess.RecordStatus == "A" && an.PortCode == portCode && an.RecordStatus == "A"
                              select new VesselVO
                              {
                                  VesselID = vess.VesselID,
                                  VesselName = vess.VesselName + " - " + an.VCN
                              }).ToList();

            AllVessels = AllVessels.OrderBy(x=>x.VesselName).Distinct().ToList();
            return AllVessels;
        }
        #endregion

        #region ModifyExternalDivingRegister
        /// <summary>
        /// Modifies / update the External diving register details
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO entity, int userId)
        {
            ExternalDivingRegister ExternalDivingRegister = new ExternalDivingRegister();
            ExternalDivingRegister = ExternalDivingRegisterMapExtension.MapToEntity(entity);
            ExternalDivingRegister.ModifiedBy = userId;
            ExternalDivingRegister.ModifiedDate = System.DateTime.Now;
            ExternalDivingRegister.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ExternalDivingRegister>().Update(ExternalDivingRegister);
            _unitOfWork.SaveChanges();
            return entity;
        }
        #endregion

        #region DeleteExternalDivingRegister
        /// <summary>
        /// Delete external diving register - not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExternalDivingRegisterVO DeleteExternalDivingRegister(long id)
        {
            var entity = _unitOfWork.Repository<ExternalDivingRegister>().Find(id);
            entity.RecordStatus = "I";
            entity.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<ExternalDivingRegister>().Update(entity);
            _unitOfWork.SaveChanges();

            ExternalDivingRegisterVO ExternalDivingRegister = ExternalDivingRegisterMapExtension.MapToDTO(entity);
            return ExternalDivingRegister;
        }
        #endregion

        public ExternalDivingRegisterVO GetExternalDivingDetailsOnCompletion(string strExternalDivingRegisterId)
        {
            int externalID = int.Parse(strExternalDivingRegisterId, CultureInfo.InvariantCulture);
            var objExternalDivingRegisterVO = (from q in _unitOfWork.Repository<ExternalDivingRegister>().Queryable()
                                               join p in _unitOfWork.Repository<Berth>().Queryable()
                                                on new { a = q.PortCode, b = q.QuayCode, c = q.BerthCode } equals new { a = p.PortCode, b = p.QuayCode, c = p.BerthCode }
                                              // join v in _unitOfWork.Repository<Vessel>().Queryable() on q.VesselID equals v.VesselID
                                               join v in _unitOfWork.Repository<Vessel>().Queryable()
                                                on q.VesselID equals v.VesselID into gj
                                               from v1 in gj.DefaultIfEmpty()
                                               join l in _unitOfWork.Repository<LicenseRequest>().Queryable() on q.CompanyName equals l.LicenseRequestID
                                               where q.ExternalDivingRegisterID == externalID
                                               orderby q.ModifiedDate descending
                                               select new ExternalDivingRegisterVO
                                               {
                                                   ExternalDivingRegisterID = q.ExternalDivingRegisterID,
                                                   DivingLogDateTime = q.DivingLogDateTime.ToString(),
                                                   CompanyNameDisplay = l.RegisteredName,
                                                   StartTime = q.StartTime.ToString(),
                                                   StopTime = q.StopTime.ToString(),
                                                   PermissionObtained = q.PermissionObtained == "Y" ? "YES" : "NO",
                                                   CreatedBy = q.CreatedBy,
                                                   CreatedDate = q.CreatedDate,
                                                   ModifiedBy = q.ModifiedBy,
                                                   ModifiedDate = q.ModifiedDate,                                                   
                                                   BerthName = p.BerthName,
                                                   VesselName = v1.VesselName,                                                   
                                                   PortCode = p.PortCode,
                                                   isPermissionObtained = q.PermissionObtained == "Y" ? true : false                                                 
                                               }).FirstOrDefault();



            objExternalDivingRegisterVO.DivingLogDateTime = objExternalDivingRegisterVO.DivingLogDateTime != "" ? Convert.ToDateTime(objExternalDivingRegisterVO.DivingLogDateTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "";
            objExternalDivingRegisterVO.StartTime = objExternalDivingRegisterVO.StartTime != "" ? Convert.ToDateTime(objExternalDivingRegisterVO.StartTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture): "";
            objExternalDivingRegisterVO.StopTime = objExternalDivingRegisterVO.StopTime != "" ? Convert.ToDateTime(objExternalDivingRegisterVO.StopTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : "";
                      

            return objExternalDivingRegisterVO;
        }
        
    }
}
