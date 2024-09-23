using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IRosterService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        RosterVO AddRoster(RosterVO rosterData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RosterVO ModifyRoster(RosterVO rosterData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RosterVO> GetRosterlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        RosterReferenceVO GetRosterReferencesData();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RosterVO> GetRosterDetails(RosterVO data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int SaveRosterDetails(RosterGroupVO data);
    }
}
