using System.Collections.Generic;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IShiftRepository
    {
        List<ShiftVO> GetShiftsByPortCode(string portCode);

        List<ShiftVO> GetActiveShiftsByPortCode(string portCode);
    }
}
