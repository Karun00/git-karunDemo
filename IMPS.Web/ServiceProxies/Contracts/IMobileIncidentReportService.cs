using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IMobileIncidentReportService : IDisposable
    {
        [OperationContract]
        List<SubCategoryVO> GetIncidentTypes();

        [OperationContract]
        IncidentVO AddIncidentReport(IncidentVO incidentData);

        [OperationContract]
        List<IncidentVO> GetIncidentReportList();

        [OperationContract]
        IncidentVO ModifyIncidentData(IncidentVO incidentData);
    }
}

