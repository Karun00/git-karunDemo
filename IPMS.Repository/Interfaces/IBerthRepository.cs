using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IBerthRepository
    {
        List<BerthVO> GetBerths(string portCode);
        List<Berth> GetBerthsForArrival(string portCode);
        
        List<BerthVO> GetDryDocBerths(string portCode);

        List<BerthVO> GetBerthsDetails(string portCode);

        List<QuayVO> GetPortQuayDetails(string portId);

        List<BerthVO> GetBerthsInQuay(string portCode, string quayCode);

        List<SubCategory> GetBerthType();

        List<BerthVO> GetBerthsWithBollards(string portCode);

        List<BerthVO> GetBerthsOnTerminalOperator(int toId);

        BerthVO AddBerth(BerthVO berthData, int userId, string portCode);

        BerthVO ModifyBerth(BerthVO berthData, int userId, string portCode);

        BerthVO DelBerthById(BerthVO berthData, int userId, string portCode);
    }
}
