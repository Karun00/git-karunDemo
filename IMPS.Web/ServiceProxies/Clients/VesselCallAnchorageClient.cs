using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class VesselCallAnchorageClient : UserClientBase<IVesselCallAnchorageService>, IVesselCallAnchorageService
    {
        public List<VesselCallVO> GetAnchorageRecordingList(string vcn, string vesselName, string etaFrom, string etaTo)
        {
            //return Channel.GetAnchorageRecordingList();
            return WrapOperationWithException(() => Channel.GetAnchorageRecordingList(vcn, vesselName, etaFrom, etaTo));
        }

        public List<VesselCallVO> GetzAnchorageRecordingList(string vcn)
        {
            //return Channel.GetzAnchorageRecordingList(vcn);
            return WrapOperationWithException(() => Channel.GetzAnchorageRecordingList(vcn));
        }

        //public Task<List<VesselCallVO>> GetAnchorageRecordingListAsync()
        //{
        //    //return Channel.GetAnchorageRecordingListAsync();
        //    return WrapOperationWithException(() => Channel.GetAnchorageRecordingListAsync());
        //}

        //public Task<VesselVO> GetReasons(string vcn)
        //{
        //    return Channel.GetVCNDetatilsAsync(vcn);

        //}

        public List<VesselCallVO> ModifyVesselCallAnchorageData(VesselCallVO vesselCallAnchorageData)
        {
            //return Channel.ModifyVesselCallAnchorageData(vesselcallanchoragedata);
            return WrapOperationWithException(() => Channel.ModifyVesselCallAnchorageData(vesselCallAnchorageData));
        }

        //public Task<List<VesselCallVO>> ModifyVesselCallAnchorageDataAsync(VesselCallVO vesselcallanchoragedata)
        //{
        //    //return Channel.ModifyVesselCallAnchorageDataAsync(vesselcallanchoragedata);
        //    return WrapOperationWithException(() => Channel.ModifyVesselCallAnchorageDataAsync(vesselcallanchoragedata));
        //}

        public List<SubCategory> GetReasons()
        {
            //return Channel.GetReasons();
            return WrapOperationWithException(() => Channel.GetReasons());
        }
        public string GetGeneralConfigs()
        {
            return WrapOperationWithException(() => Channel.GetGeneralConfigs());
        }

        //public Task<List<SubCategory>> GetReasonsAsync()
        //{
        //    //return Channel.GetReasonsAsync();
        //    return WrapOperationWithException(() => Channel.GetReasonsAsync());
        //}

        public List<RevenuePostingVO> VesselCallVcnDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.VesselCallVcnDetailsforAutocomplete(searchvalue));
        }

        public List<VesselVO> VesselCallVesselDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.VesselCallVesselDetailsforAutocomplete(searchvalue));
        }
        public VcnCloseVO VcnClose(string vcn)
        {
            return WrapOperationWithException(() => Channel.VcnClose(vcn));
        }
        
    }
}