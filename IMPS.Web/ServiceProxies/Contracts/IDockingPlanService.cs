using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDockingPlanService : IDisposable
    {
        /// <summary>
        /// To Get Docking Plan Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DockingPlanVO> DockingPlanDetails();

        /// <summary>
        /// To Get Vessel Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DockingPlanVO> GetVesselNames(string searchValue, string searchColumn);

        /// <summary>
        /// To Get Vessel Information By VesselID
        /// </summary>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        [OperationContract]
        DockingPlanVO GetVesselsById(int vesselId);

        /// <summary>
        /// To Add Docking Plan Data
        /// </summary>
        /// <param name="dockingplandata"></param>
        /// <returns></returns>
        [OperationContract]
        DockingPlanVO AddDockingPlan(DockingPlanVO data);

        /// <summary>
        /// To Modify Docking Plan Data
        /// </summary>
        /// <param name="dockingplandata"></param>
        /// <returns></returns>
        [OperationContract]
        DockingPlanVO ModifyDockingPlan(DockingPlanVO data);

        //Add by srinivas
        [OperationContract]
        DockingPlanVO Cancel(DockingPlanVO servicedata);
        /// <summary>
        ///  To Approve DockingPlan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void ApproveDockingPlan(string dockingplanid, string remarks, string taskcode);

        /// <summary>
        ///  To Reject DockingPlan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        void RejectDockingPlan(string dockingplanid, string remarks, string taskcode);

        /// <summary>
        ///  To get DockingPlan based on dockingplanid
        /// </summary>
        /// <param name="dockingPlanId"></param>
        /// <returns></returns>
        [OperationContract]
        List<DockingPlanVO> GetDockingPlan(int dockingPlanId);

        [OperationContract]
        List<SubCategory> GetDocumentTypes();
    }
}
