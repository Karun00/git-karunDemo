using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IDockingPlanService
    {
        /// <summary>
        /// To Get Docking Plan Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DockingPlanVO> DockingPlanDetails();

        /// <summary>
        /// To Get Vessel Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DockingPlanVO> GetVesselNames(string searchValue, string searchColumn);

        /// <summary>
        /// To Get Vessel Details By VesselID
        /// </summary>
        /// <param name="vesselId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DockingPlanVO GetVesselsById(int vesselId);


        /// <summary>
        /// To Add Docking Plan Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DockingPlanVO AddDockingPlan(DockingPlanVO data);

        /// <summary>
        /// To Modify Docking Plan Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DockingPlanVO ModifyDockingPlan(DockingPlanVO data);

        //Add by Srinivas

        [OperationContract]
        [FaultContract(typeof(Exception))]
        DockingPlanVO Cancel(DockingPlanVO servicedata);
        /// <summary>
        ///  To Approve DockingPlan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveDockingPlan(string dockingplanid, string remarks, string taskcode);


        /// <summary>
        /// To Reject DockingPlan Request
        /// </summary>
        /// <param name="DockingPlanID"></param>
        /// <param name="remarks"></param>
        /// <param name="taskcode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectDockingPlan(string dockingplanid, string remarks, string taskcode);

        /// <summary>
        /// To get  DockingPlan based on fuelrequisitionid
        /// </summary>
        /// <param name="dockingPlanId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DockingPlanVO> GetDockingPlan(int dockingPlanId);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDocumentTypes();
    }
}
