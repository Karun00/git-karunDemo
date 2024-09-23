using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class PortClient : UserClientBase<IPortService>, IPortService
    {
        public Port AddPort(Port portData)
        {
            return WrapOperationWithException(() => Channel.AddPort(portData));
        }

        public Port ModifyPort(Port portData)
        {
            return WrapOperationWithException(() => Channel.ModifyPort(portData));
        }

        public Port GetPortId(long id)
        {
            return WrapOperationWithException(() => Channel.GetPortId(id));
        }

        public Port DelPort(long id)
        {
            return WrapOperationWithException(() => Channel.DelPort(id));
        }

        public List<Port> GetPorts()
        {
            return WrapOperationWithException(() => Channel.GetPorts());
        }

        public List<Port> GetLoginPort()
        {
            return WrapOperationWithException(() => Channel.GetLoginPort());
        }
        

        //public Task<Port> AddPortAsync(Port portdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddPortAsync(portdata));
        //}

        //public Task<Port> GetPortIDAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.GetPortIDAsync(id));
        //}

        //public Task<Port> DelPortAsync(long id)
        //{
        //    return WrapOperationWithException(() => Channel.DelPortAsync(id));
        //}

        //public Task<List<Port>> GetPortsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPortsAsync());
        //}


        //public List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(DateTime fromDate, DateTime toDate)
        //{
        //    return WrapOperationWithException(() => Channel.GetPlannedMovementsCount(fromDate, toDate));
        //}

    }
}