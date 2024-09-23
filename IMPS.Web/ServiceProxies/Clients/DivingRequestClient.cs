using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class DivingRequestClient : UserClientBase<IDivingRequestService>, IDivingRequestService
    {
        public List<QuayVO> GetPortQuays()
        {
            return WrapOperationWithException(() => Channel.GetPortQuays());
        }

        public List<BerthVO> GetQuayBerths(string quayCode)
        {
            return WrapOperationWithException(() => Channel.GetQuayBerths(quayCode));
        }

        public List<BollardVO> GetBerthBollards(string quayCode, string berthCode)
        {
            return WrapOperationWithException(() => Channel.GetBerthBollards(quayCode, berthCode));
        }

        public List<LocationVO> GetOtherLocations()
        {
            return WrapOperationWithException(() => Channel.GetOtherLocations());
        }

        public DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest)
        {
            return WrapOperationWithException(() => Channel.AddDivingRequest(divingRequest));
        }

        public List<DivingRequestVO> GetAllDivingRequests()
        {
            return WrapOperationWithException(() => Channel.GetAllDivingRequests());
        }

        public List<DivingRequestVO> GetAllDivingTaskExecutions()
        {
            return WrapOperationWithException(() => Channel.GetAllDivingTaskExecutions());
        }

        public DivingRequestVO ModifyDivingCheckList(DivingRequestVO divingRequestvo)
        {
            return WrapOperationWithException(() => Channel.ModifyDivingCheckList(divingRequestvo));
        }

        public DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestvo)
        {
            return WrapOperationWithException(() => Channel.ModifyDivingTaskExecution(divingRequestvo));
        }

        //Added by Srini
        public DivingRequestVO GetDivingRequestOccupationById(int divingRequestId)
        {
            return WrapOperationWithException(() => Channel.GetDivingRequestOccupationById(divingRequestId));
        }

        public List<DivingRequestVO> GetAllDivingRequestOccupation()
        {
            return WrapOperationWithException(() => Channel.GetAllDivingRequestOccupation());
        }

        //GetDivingRequestByID(int requestid);        
        public List<DivingRequestVO> GetDivingRequestByIdView(int requestId)
        {
            return WrapOperationWithException(() => Channel.GetDivingRequestByIdView(requestId));
        }

        //ModifyDivingRequestOccupation        
        public DivingRequestVO ModifyDivingRequestOccupation(DivingRequestVO divingrequestvo)
        {
            return WrapOperationWithException(() => Channel.ModifyDivingRequestOccupation(divingrequestvo));
        }

        public void ApproveDivingRequestOccupation(string divingRequestId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveDivingRequestOccupation(divingRequestId, remarks, taskCode));
        }

        public void VerifyDivingRequestOccupation(string divingRequestId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.VerifyDivingRequestOccupation(divingRequestId, remarks, taskCode));
        }

        public void RejectDivingRequestOccupation(string divingRequestId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectDivingRequestOccupation(divingRequestId, remarks, taskCode));
        }

        public List<LocationVO> GetAllLocations()
        {
            return WrapOperationWithException(() => Channel.GetAllLocations());
        }

        public List<DivingRequestVO> GetDivingrequestsForScroll()
        {
            return WrapOperationWithException(() => Channel.GetDivingrequestsForScroll());
        }

        //public List<DivingRequestVO> GetDivingrequestsForScrollAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetDivingrequestsForScrollAsync());
        //}

        public string GetLoggedInUserName()
        {
            return WrapOperationWithException(() => Channel.GetLoggedInUserName());
        }

        public List<SubCategoryCodeNameVO> GetDivingRequestReasons()
        {
            return WrapOperationWithException(() => Channel.GetDivingRequestReasons());
        }

    }
}