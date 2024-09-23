using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IResourceAttendanceService
    {
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDesignations();

        /// <summary>
        /// To get the shift details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data);

        /// <summary>
        /// To get the resource attendance details
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data);

        /// <summary>
        /// To add a resource attendance data to the data base
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        int AddResourceAttendanceDetails(ResourceAttendanceVO data);
    }
}
