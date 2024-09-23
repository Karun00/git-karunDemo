using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ResourceAttendanceService : ServiceBase, IResourceAttendanceService
    {
        private IResourceAttendanceRepository _ResourceAttendanceRepository;

        public ResourceAttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _ResourceAttendanceRepository = new ResourceAttendanceRepository(_unitOfWork);
        }

        public ResourceAttendanceService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _ResourceAttendanceRepository = new ResourceAttendanceRepository(_unitOfWork);
        }

        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceAttendanceRepository.GetDesignations();
            });
        }

        /// <summary>
        /// To get the shift details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                // Bhoji009
                return _ResourceAttendanceRepository.GetShiftDetailsIncludingContinuous(data, _PortCode);
            });
        }

        /// <summary>
        /// To get the resource attendance details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceAttendanceRepository.GetResourceAttendanceDetails(data);
            });
        }

        /// <summary>
        /// To add a resource attendance data to the data base
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int AddResourceAttendanceDetails(ResourceAttendanceVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _ResourceAttendanceRepository.AddResourceAttendanceDetails(data, _UserId);
            });
        }
    }
}
