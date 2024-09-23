using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Clients
{
    public class DredgingPriorityClient : UserClientBase<IDredgingPriorityService>, IDredgingPriorityService
    {
        public DredgingPriorityVO GetDredgingPriorityReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetDredgingPriorityReferenceVO());
        }
        public List<FinancialYearVO> GetMonths(int financialYearId)
        {
            return WrapOperationWithException(() => Channel.GetMonths(financialYearId));
        }
        public List<DredgingPriorityVO> DredgingPriorityDetails(int financialYearId)
        {
            return WrapOperationWithException(() => Channel.DredgingPriorityDetails(financialYearId));
        }
        public List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId)
        {
            return WrapOperationWithException(() => Channel.GetDredgingPriorityVolumes(financialYearId));
        }
        public List<DredgingPriorityVO> GetLocationTypes()
        {
            return WrapOperationWithException(() => Channel.GetLocationTypes());
        }
        public List<DredgingPriorityVO> GetBerthTypes()
        {
            return WrapOperationWithException(() => Channel.GetBerthTypes());
        }

        /// <summary>
        /// To add Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dredgingPriorityData"></param>
        /// <returns></returns>
        public DredgingPriorityVO AddDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData)
        {
            return WrapOperationWithException(() => Channel.AddDredgingPriorityDetails(dredgingPriorityData));
        }

        /// <summary>
        /// To Update Dredging Priority data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="DredgingPrioritydata"></param>
        /// <returns></returns>

        public DredgingPriorityVO ModifyDredgingPriorityDetails(DredgingPriorityVO dredgingPriorityData)
        {
            return WrapOperationWithException(() => Channel.ModifyDredgingPriorityDetails(dredgingPriorityData));
        }

        public List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId)
        {
            return WrapOperationWithException(() => Channel.GetDredgingPriorityPendingView(dredgingPriorityId));
        }
        public void ApproveDredgingPriority(string dredgingPriorityId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveDredgingPriority(dredgingPriorityId, remarks, taskCode));
        }
        public void RejectDredgingPriority(string dredgingPriorityId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectDredgingPriority(dredgingPriorityId, remarks, taskCode));
        }
        public List<FinancialYearVO> GetFinancialYear()
        {
            return WrapOperationWithException(() => Channel.GetFinancialYear());
        }

        public List<DredgingOperationVO> GetBerthOccupationList()
        {
            return WrapOperationWithException(() => Channel.GetBerthOccupationList());
        }

        public List<DredgingOperationVO> GetBerthOccupationById(int id)
        {
            return WrapOperationWithException(() => Channel.GetBerthOccupationById(id));
        }
        public void ApproveBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveBerthOccupation(dredgingPriorityAreaId, remarks, taskCode));
        }


        public void RejectBerthOccupation(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectBerthOccupation(dredgingPriorityAreaId, remarks, taskCode));
        }


        public void ApproveDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveDredgingVolume(dredgingPriorityAreaId, remarks, taskCode));
        }


        public void RejectDredgingVolume(string dredgingPriorityAreaId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectDredgingVolume(dredgingPriorityAreaId, remarks, taskCode));
        }

        public List<DredgingOperationVO> GetDredgingVolumeList()
        {
            return WrapOperationWithException(() => Channel.GetDredgingVolumeList());
        }
        public List<DredgingOperationVO> GetDredgingVolumeById(int id)
        {
            return WrapOperationWithException(() => Channel.GetDredgingVolumeById(id));
        }
        public DredgingOperationVO UpdateDredgingVolume(DredgingOperationVO data)
        {
            return WrapOperationWithException(() => Channel.UpdateDredgingVolume(data));
        }

        public DredgingOperationVO UpdateBerthOccupation(DredgingOperationVO data)
        {
            return WrapOperationWithException(() => Channel.UpdateBerthOccupation(data));
        }



        public List<DredgingOperationVO> DredgingPriorityAreaDetails(int dredgingPriorityId)
        {
            return WrapOperationWithException(() => Channel.DredgingPriorityAreaDetails(dredgingPriorityId));
        }
        public List<DredgingOperationVO> DredgingPriorityAreaDetailsPending(int dredgingPriorityId)
        {
            return WrapOperationWithException(() => Channel.DredgingPriorityAreaDetailsPending(dredgingPriorityId));
        }
        public List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId)
        {
            return WrapOperationWithException(() => Channel.GetDocument(dredgingPriorityId));
        }
        public List<DredgingOperationVO> Cancel(DredgingOperationVO data)
        {
            return WrapOperationWithException(() => Channel.Cancel(data));
        }
            
    }
}