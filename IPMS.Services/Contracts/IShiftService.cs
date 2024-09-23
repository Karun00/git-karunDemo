using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    interface IShiftService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ShiftVO> GetShiftList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ShiftVO AddShift(ShiftVO shiftdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ShiftVO ModifyShift(ShiftVO shiftdata);
    }
}
