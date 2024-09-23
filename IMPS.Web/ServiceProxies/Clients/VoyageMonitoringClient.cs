using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class VoyageMonitoringClient : UserClientBase<IVoyageMonitoringService>, IVoyageMonitoringService
    {
        public List<VoyageMonitoringVO> GetVcnDetailsVoyage(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetVcnDetailsVoyage(searchValue));
        }

        public List<VoyageMonitoringVO> GetVcnDetailsVoyage_vcn(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetVcnDetailsVoyage_vcn(vcn));
        }
        public List<VoyageMonitoringVO> GetServiceRequestDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetServiceRequestDetails(VCN));
        }

        public List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetChangeAtaAndAtdDetails(VCN));
        }

        public List<VoyageMonitoringVO> GetAnchorageDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetAnchorageDetails(VCN));
        }

        public List<VoyageMonitoringVO> GetBerthDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetBerthDetails(VCN));
        }

        public List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string VCN)
        {
            return WrapOperationWithException(() => Channel.GetPortAndBreakLimitDetails(VCN));
        }
        
    }
}