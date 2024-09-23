using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ISuppMiscServiceRecordingService : IDisposable
    {
        /// <summary>
        /// To Get Miscellaneous Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SuppDryDockVO> SuppMiscServiceDetails();

        /// <summary>
        /// To Get Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SuppMiscServiceVO GetSuppMiscReferenceVO();

        /// <summary>
        /// To Get Miscellaneous Recording Details 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SuppMiscServiceVO> SuppMiscServiceRecordingDetails(int SuppDryDockID);

        /// <summary>
        /// To Add Supp Misc Service Recording
        /// </summary>
        /// <param name="SuppMiscservice"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppMiscServiceVO ModifySuppMiscServiceRecord(SuppMiscServiceVO data);
    }
}
