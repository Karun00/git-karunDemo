using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IRosterService : IDisposable
    {
        [OperationContract]
        RosterVO AddRoster(RosterVO rosterData);
        [OperationContract]
        RosterVO ModifyRoster(RosterVO rosterData);
        [OperationContract]
        List<RosterVO> GetRosterlist();
        [OperationContract]
        RosterReferenceVO GetRosterReferencesData();
        [OperationContract]
        List<RosterVO> GetRosterDetails(RosterVO data);

        //[OperationContract]
        //Task<RosterVO> AddRosterAsync(RosterVO rosterdata);
        //[OperationContract]
        //Task<RosterVO> ModifyRosterAsync(RosterVO rosterdata);
        //[OperationContract]
        //Task<List<RosterVO>> GetRosterlistAsync();
        //[OperationContract]
        //Task<RosterReferenceVO> GetRosterReferencesDataAsync();
        //[OperationContract]
        //Task<List<RosterGroupVO>> GetShiftDetailsAsync(String designation);
        [OperationContract]
        int SaveRosterDetails(RosterGroupVO data);
    }
}
