using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class ResourceAttendanceClient : UserClientBase<IResourceAttendanceService>, IResourceAttendanceService
    {
        /// <summary>
        /// To get designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return WrapOperationWithException(() => Channel.GetDesignations());
        }

        public List<ShiftVO> GetShiftDetails(ResourceAttendanceVO data)
        {
            return WrapOperationWithException(() => Channel.GetShiftDetails(data));
        }

        public List<EmployeeMasterDetails> GetResourceAttendanceDetails(ResourceAttendanceVO data)
        {
            return WrapOperationWithException(() => Channel.GetResourceAttendanceDetails(data));
        }

        public int AddResourceAttendanceDetails(ResourceAttendanceVO data)
        {
            return WrapOperationWithException(() => Channel.AddResourceAttendanceDetails(data));
        }
        
    }
}