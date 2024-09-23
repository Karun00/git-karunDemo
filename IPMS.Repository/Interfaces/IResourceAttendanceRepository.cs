using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IResourceAttendanceRepository
    {
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        List<SubCategory> GetDesignations();

        List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data, string portCode);
        // Bhoji009
        List<ShiftVO> GetShiftDetailsIncludingContinuous(ResourceAttendanceVO data, string portCode);

        List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data);

        int AddResourceAttendanceDetails(ResourceAttendanceVO data, int userId);
    }
}
