using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IBollardRepository
    {
       List<QuayVO> GetPortQuays(string id);

       List<BerthVO> GetQuayBerths(string portCode, string quayCode);

       List<BollardVO> GetBollardsInBerths(string portCode, string quayCode, string berthCode);

       List<BollardVO> GetBollardDetails(string portCode);

       BollardVO GetBollardById(string id);


    }
}
