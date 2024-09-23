using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVesselSAPPostingService
    {
        /// <summary>
        /// To Get Vessels for SAP Posting
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue);

        /// <summary>
        /// To Post Vessel SAP Data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO value);

        [OperationContract]
        List<SAPPostingVO> GetSAPVesselPostGrid();
    }
}
