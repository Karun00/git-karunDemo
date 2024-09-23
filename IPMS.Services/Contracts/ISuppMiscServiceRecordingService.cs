using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISuppMiscServiceRecordingService
    {
        /// <summary>
        /// To Get Miscellaneous Service Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppDryDockVO> SuppMiscServiceDetails();

        /// <summary>
        /// To Get Reference data While initialization
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppMiscServiceVO GetSuppMiscReferenceVO();

       
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppMiscServiceVO> SuppMiscServiceRecordingDetails(int SuppDryDockID);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppMiscServiceVO ModifySuppMiscServiceRecord(SuppMiscServiceVO data);
    }
}
