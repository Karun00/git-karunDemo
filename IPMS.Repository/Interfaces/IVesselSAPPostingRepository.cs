using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IVesselSAPPostingRepository
    {
        List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue, string PortCode);
        VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO value, string PortCode);
        List<SAPPostingVO> GetSAPVesselPostGrid(string PortCode);
    }
}
