using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{

    public class BerthClient : UserClientBase<IBerthService>, IBerthService
    {
        public List<BerthVO> GetBerthsDetails()
        {
            return WrapOperationWithException(() => Channel.GetBerthsDetails());
        }

        public List<BerthVO> GetBerthsInQuay(string portCode, string quayCode)
        {
            return WrapOperationWithException(() => Channel.GetBerthsInQuay(portCode, quayCode));
        }

        public BerthVO AddBerth(BerthVO berthData)
        {
            return WrapOperationWithException(() => Channel.AddBerth(berthData));
        }

        public BerthVO ModifyBerth(BerthVO berthData)
        {
            return WrapOperationWithException(() => Channel.ModifyBerth(berthData));
        }

        public List<SubCategory> GetBerthType()
        {
            return WrapOperationWithException(() => Channel.GetBerthType());
        }

        public BerthVO DelBerthById(BerthVO berthData)
        {
            return WrapOperationWithException(() => Channel.DelBerthById(berthData));
        }

        public List<QuayVO> GetPortQuayDetails()
        {
            return WrapOperationWithException(() => Channel.GetPortQuayDetails());
        }

        //public Task<List<BerthVO>> GetBerthsDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthsDetailsAsync());
        //}

        //public Task<BerthVO> AddBerthAsync(BerthVO berthdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddBerthAsync(berthdata));
        //}

        //public Task<BerthVO> ModifyBerthAsync(BerthVO berthdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyBerthAsync(berthdata));
        //}

        //public Task<BerthVO> DelBerthByIDAsync(BerthVO berthdata)
        //{
        //    return WrapOperationWithException(() => Channel.DelBerthByIDAsync(berthdata));
        //}

        //public Task<List<SubCategory>> GetBerthTypeAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthTypeAsync());
        //}   

        public List<SubCategory> GetCargoType()
        {
            return WrapOperationWithException(() => Channel.GetCargoType());
        }

        public List<SubCategoryCodeNameVO> GetVesselType()
        {
            return WrapOperationWithException(() => Channel.GetVesselType());
        }

        public List<SubCategoryCodeNameVO> GetReasonType()
        {
            return WrapOperationWithException(() => Channel.GetReasonType());
        }

        ////////mahesh//////

        public List<BerthVO> GetBerthsWithBollards()
        {
            return WrapOperationWithException(() => Channel.GetBerthsWithBollards());
        }

        //public Task<List<BerthVO>> GetBerthsWithBollardsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBerthsWithBollardsAsync());
        //}

        /// <summary>
        /// Author   :  Sandeep Appana
        /// Date     :  28-8-2014
        /// Purpose  :  To Get Berthlist based on Ports.
        /// </summary>
        /// <returns></returns>        
        public List<BerthVO> GetBerthsWithPortCode()
        {
            return WrapOperationWithException(() => Channel.GetBerthsWithPortCode());
        }
    }
}