using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class RosterClient : UserClientBase<IRosterService>, IRosterService 
    {

        RosterVO IRosterService.AddRoster(RosterVO rosterData)
        {
            return WrapOperationWithException(() => Channel.AddRoster(rosterData));
        }

        RosterVO IRosterService.ModifyRoster(RosterVO rosterData)
        {
            return WrapOperationWithException(() => Channel.ModifyRoster(rosterData));
        }

        List<RosterVO> IRosterService.GetRosterlist()
        {
            return WrapOperationWithException(() => Channel.GetRosterlist());
        }
        RosterReferenceVO IRosterService.GetRosterReferencesData()
        {
            return WrapOperationWithException(() => Channel.GetRosterReferencesData());
        }

        //Task<RosterVO> IRosterService.AddRosterAsync(RosterVO rosterdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddRosterAsync(rosterdata));
        //}

        //Task<RosterVO> IRosterService.ModifyRosterAsync(RosterVO rosterdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyRosterAsync(rosterdata));
        //}

        //Task<List<RosterVO>> IRosterService.GetRosterlistAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetRosterlistAsync());
        //}

        //Task<RosterReferenceVO> IRosterService.GetRosterReferencesDataAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetRosterReferencesDataAsync());
        //}


        List<RosterVO> IRosterService.GetRosterDetails(RosterVO data)
        {
            return WrapOperationWithException(() => Channel.GetRosterDetails(data));
        }

        //Task<List<RosterGroupVO>> IRosterService.GetShiftDetailsAsync(string designation)
        //{
        //    return WrapOperationWithException(() => Channel.GetShiftDetailsAsync(designation));
        //}

        int IRosterService.SaveRosterDetails(RosterGroupVO data)
        {
            return WrapOperationWithException(() => Channel.SaveRosterDetails(data));
        }

        void System.IDisposable.Dispose()
        {
            throw new System.NotImplementedException();
        }


    }
}