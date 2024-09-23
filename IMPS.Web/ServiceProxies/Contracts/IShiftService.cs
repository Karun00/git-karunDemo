using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IShiftService : IDisposable
    {
        [OperationContract]
        List<ShiftVO> GetShiftList();
        [OperationContract]
        ShiftVO AddShift(ShiftVO shiftdata);
        [OperationContract]
        ShiftVO ModifyShift(ShiftVO shiftdata);

        //[OperationContract]
        //Task<List<ShiftVO>> GetShiftListAsync();
        //[OperationContract]
        //Task<ShiftVO> AddShiftAsync(ShiftVO shiftdata);
        //[OperationContract]
        //Task<ShiftVO> ModifyShiftAsync(ShiftVO shiftdata);
    }
}
