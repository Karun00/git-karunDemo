using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class MobileIncidentReportClient : UserClientBase<IMobileIncidentReportService>, IMobileIncidentReportService
    {
        public List<SubCategoryVO> GetIncidentTypes()
        {
            return WrapOperationWithException(() => Channel.GetIncidentTypes());
        }

        public IncidentVO AddIncidentReport(IncidentVO incidentData)
        {
            return WrapOperationWithException(() => Channel.AddIncidentReport(incidentData));
        }

        public List<IncidentVO> GetIncidentReportList()
        {
            return WrapOperationWithException(() => Channel.GetIncidentReportList());
        }

        public IncidentVO ModifyIncidentData(IncidentVO incidentData)
        {
            return WrapOperationWithException(() => Channel.ModifyIncidentData(incidentData));
        }
    }
}
