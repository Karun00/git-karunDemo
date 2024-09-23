using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ISuppDryDockRepository
    {
        List<SuppDryDockVO> GetSuppDryDockApplicationList(string portcode, int userid);

        SuppDryDock GetSuppDryDockRequestDetailsByID(string suppdrydockid);

        SuppDryDock GetSuppDryDockApproveid(string suppdrydockid);

        List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID);

        List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue, int userid, string portcode);

        CompanyVO GetUserDetails(int userid);

        SuppDryDockVO GetSuppDryDockVCN(string vcn);
    }
}
