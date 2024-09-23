using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IVoyageMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetVcnDetailsVoyage(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetVcnDetailsVoyage_vcn(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetServiceRequestDetails(string VCN);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string VCN);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetAnchorageDetails(string VCN);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetBerthDetails(string VCN);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string VCN);
    }
}
