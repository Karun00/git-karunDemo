using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;
using IPMS.ServiceProxies.Contracts;

namespace IPMS.ServiceProxies.Clients
{
   
        public class ShiftClient : UserClientBase<IShiftService>, IShiftService
        {
            public List<ShiftVO> GetShiftList()
            {
                return WrapOperationWithException(() => Channel.GetShiftList());
            }

            public ShiftVO AddShift(ShiftVO shiftData)
            {
                return WrapOperationWithException(() => Channel.AddShift(shiftData));
            }

            public ShiftVO ModifyShift(ShiftVO shiftData)
            {
                return WrapOperationWithException(() => Channel.ModifyShift(shiftData));
            }

            //public Task<List<ShiftVO>> GetShiftListAsync()
            //{
            //    return WrapOperationWithException(() => Channel.GetShiftListAsync());
            //}

            //public Task<ShiftVO> AddShiftAsync(ShiftVO shiftData)
            //{
            //    return WrapOperationWithException(() => Channel.AddShiftAsync(shiftData));
            //}

            //public Task<ShiftVO> ModifyShiftAsync(ShiftVO shiftData)
            //{
            //    return WrapOperationWithException(() => Channel.ModifyShiftAsync(shiftData));
            //}
        }
    
}