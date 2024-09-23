using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;


namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVesselCallAnchorageService : IDisposable
    {
        [OperationContract]
        List<VesselCallVO> GetAnchorageRecordingList(string vcn,string vesselName,string etaFrom,string etaTo);

        [OperationContract]
        List<VesselCallVO> GetzAnchorageRecordingList(string vcn);

        //[OperationContract]
        //Task<List<VesselCallVO>> GetAnchorageRecordingListAsync();

        [OperationContract]
        List<SubCategory> GetReasons();

        [OperationContract]
        string GetGeneralConfigs();

        //[OperationContract]
        //Task<List<SubCategory>> GetReasonsAsync();

        [OperationContract]
        List<VesselCallVO> ModifyVesselCallAnchorageData(VesselCallVO vesselCallAnchorageData);

        //[OperationContract]
        //Task<List<VesselCallVO>> ModifyVesselCallAnchorageDataAsync(VesselCallVO vesselcallanchoragedata);




        [OperationContract]
        List<RevenuePostingVO> VesselCallVcnDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        List<VesselVO> VesselCallVesselDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        VcnCloseVO VcnClose(string vcn);

        
    }
}