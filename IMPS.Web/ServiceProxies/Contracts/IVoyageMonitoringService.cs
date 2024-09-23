using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVoyageMonitoringService : IDisposable
    {
        [OperationContract]
        List<VoyageMonitoringVO> GetVcnDetailsVoyage(string searchValue);

        [OperationContract]
        List<VoyageMonitoringVO> GetVcnDetailsVoyage_vcn(string vcn);

        [OperationContract]
        List<VoyageMonitoringVO> GetServiceRequestDetails(string VCN);

        [OperationContract]
        List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string VCN);

        [OperationContract]
        List<VoyageMonitoringVO> GetAnchorageDetails(string VCN);

        [OperationContract]
        List<VoyageMonitoringVO> GetBerthDetails(string VCN);

        [OperationContract]
        List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string VCN);
    }
}