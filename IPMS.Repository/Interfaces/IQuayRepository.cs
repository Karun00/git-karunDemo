using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IQuayRepository
    {
        QuayVO GetQuayId(string id);
        QuayVO DeleteQuay(long id);
        List<QuayVO> QuayDetails(string portCode);
        List<QuayVO> QuayPortDetails(string portCode);
        List<QuayVO> GetQuaysWithBerths(string portCode);
        List<QuayVO> GetQuaysWithBerthsMobile(string portCode);
    }
}
