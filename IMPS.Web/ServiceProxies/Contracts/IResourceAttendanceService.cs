using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IResourceAttendanceService : IDisposable
    {
        /// <summary>
        /// To get deisgnation details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SubCategory> GetDesignations();

        [OperationContract]
        List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data);

        [OperationContract]
        List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data);

        [OperationContract]
        int AddResourceAttendanceDetails(ResourceAttendanceVO data);

    }
}
