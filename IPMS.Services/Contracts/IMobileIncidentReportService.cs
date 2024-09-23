using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IMobileIncidentReportService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetIncidentTypes();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IncidentVO AddIncidentReport(IncidentVO incidentData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<IncidentVO> GetIncidentReportList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IncidentVO ModifyIncidentData(IncidentVO incidentData);
    }
}
