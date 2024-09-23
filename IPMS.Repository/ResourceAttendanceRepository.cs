using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Globalization;

namespace IPMS.Repository
{
    public class ResourceAttendanceRepository : IResourceAttendanceRepository
    {
        private IUnitOfWork _unitOfWork;

        public ResourceAttendanceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetDesignations
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            var DesignationType = (from ad in _unitOfWork.Repository<SubCategory>().Query().Select()
                                   where ad.SupCatCode == SuperCategoryConstants.RESGRP_DESIGANTIONCODE && ad.RecordStatus == "A"
                                   orderby ad.SubCatName ascending
                                   select new SubCategory
                                   {
                                       SubCatName = ad.SubCatName,
                                       SubCatCode = ad.SubCatCode

                                   });

            return DesignationType.ToList();
        }
        #endregion

        #region GetShiftDetails
        /// <summary>
        /// To get the shift details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data, string portCode)
        {
            var attendanceDate = string.Empty;
            var position = string.Empty;

            if (data != null)
            {
                attendanceDate = data.AttendanceDate;
                position = data.Position;
            }

            var _portCode = new SqlParameter("@portcode", portCode);
            var _date = new SqlParameter("@datename", attendanceDate);
            var _position = new SqlParameter("@designation", position);

            var shifts = _unitOfWork.SqlQuery<ShiftVO>("dbo.usp_GetDateWiseShifts @datename, @designation, @portcode", _date, _position, _portCode).ToList();

            return shifts;
        }
        #endregion

        #region GetShiftDetailsIncludingContinuous  
        /// <summary>
        /// To get the shift details Bhoji009
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ShiftVO> GetShiftDetailsIncludingContinuous(ResourceAttendanceVO data, string portCode)
        {
            var attendanceDate = string.Empty;
            var position = string.Empty;

            if (data != null)
            {
                attendanceDate = data.AttendanceDate;
                position = data.Position;
            }

            var _portCode = new SqlParameter("@portcode", portCode);
            var _date = new SqlParameter("@datename", attendanceDate);
            var _position = new SqlParameter("@designation", position);

            var shifts = _unitOfWork.SqlQuery<ShiftVO>("dbo.usp_GetDateWiseShifts_Continuous @datename, @designation, @portcode", _date, _position, _portCode).ToList();

            return shifts;
        }
        #endregion

        #region GetResourceAttendanceDetails
        /// <summary>
        /// To get the resource attendance details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data)
        {
            string status = "false";

            if (data != null)
            {
                if (DateTime.Parse(data.AttendanceDate, CultureInfo.InvariantCulture).Date == DateTime.Now.Date)
                {
                    status = "true";
                }
            }

                var shiftID = new SqlParameter("@shiftid", data.ShiftID);
                var date = new SqlParameter("@datename", data.AttendanceDate);
                var position = new SqlParameter("@designation", data.Position);
                var currentDate = new SqlParameter("@currentDate", status);
            
            var employees =
                    _unitOfWork.SqlQuery<EmployeeMasterDetails>(
                        "dbo.usp_GetResourceAttendanceData @datename, @shiftid, @designation, @currentDate", date,
                        shiftID, position, currentDate).ToList();
            
            return employees.ToList();
        }
        #endregion

        #region AddResourceAttendanceDetails
        /// <summary>
        /// To add a resource attendance data to the data base
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int AddResourceAttendanceDetails(ResourceAttendanceVO data, int userId)
        {
            ResourceAttendance resourceAttendance = null;

            resourceAttendance = data.MapToEntity();

            resourceAttendance.CreatedBy = userId;
            resourceAttendance.CreatedDate = DateTime.Now;
            resourceAttendance.ModifiedBy = userId;
            resourceAttendance.ModifiedDate = DateTime.Now;
            resourceAttendance.RecordStatus = "A";

            List<ResourceAttendanceDtl> resourceAttendanceDetails = resourceAttendance.ResourceAttendanceDtls.ToList();

            if (resourceAttendance.ResourceAttendanceID > 0)
            {
                resourceAttendance.ObjectState = ObjectState.Modified;
                resourceAttendance.ResourceAttendanceDtls = null;
                _unitOfWork.Repository<ResourceAttendance>().Update(resourceAttendance);
                _unitOfWork.SaveChanges();

               _unitOfWork.ExecuteSqlCommand("delete dbo.ResourceAttendanceDtl where ResourceAttendanceID = @p0", resourceAttendance.ResourceAttendanceID);
            }
            else
            {
                resourceAttendance.ObjectState = ObjectState.Added;
                resourceAttendance.ResourceAttendanceDtls = null;
                _unitOfWork.Repository<ResourceAttendance>().Insert(resourceAttendance);
                _unitOfWork.SaveChanges();
            }

            if (resourceAttendanceDetails.Count > 0)
            {
                foreach (var rsdtl in resourceAttendanceDetails)
                {
                    rsdtl.ResourceAttendanceID = resourceAttendance.ResourceAttendanceID;
                    rsdtl.RecordStatus = "A";
                    rsdtl.CreatedBy = userId;
                    rsdtl.CreatedDate = DateTime.Now;
                    rsdtl.ModifiedBy = userId;
                    rsdtl.ModifiedDate = DateTime.Now;
                }
                _unitOfWork.Repository<ResourceAttendanceDtl>().InsertRange(resourceAttendanceDetails);
                _unitOfWork.SaveChanges();
            }

            return resourceAttendance.ResourceAttendanceID;
        }
        #endregion
    }
}
