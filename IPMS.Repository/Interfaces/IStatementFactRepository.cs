using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;


namespace IPMS.Repository
{
    public interface IStatementFactRepository
    {
        List<StatementVCNVO> StatementFactDetails(string portcode, int UserTypeID, string UserType, string vcnSearch, string vesselName);

        List<StatementVCNVO> GetStatementVCNS(string portcode, int UserTypeId, string UserType, string searchValue);

        StatementVCNVO GetVesselByVCN(string vcn);

        Entity GetEnties(string entitycode);

        CompanyVO GetUserDetails(int UserID);

        StatementVCNVO GetStatementFactNotificationByID(string value);

        StatementVCNVO GetTugsByVCN(string vcn);
    }
}
