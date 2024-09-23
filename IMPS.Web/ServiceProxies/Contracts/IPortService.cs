using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IPortService : IDisposable
    {
        [OperationContract]
        Port AddPort(Port portData);

        [OperationContract]
        Port ModifyPort(Port portData);

        [OperationContract]
        Port GetPortId(long id);


        [OperationContract]
        Port DelPort(long id);

        [OperationContract]
        List<Port> GetPorts();
        [OperationContract]
        List<Port> GetLoginPort();

        //[OperationContract]
        //List<DashBoard> GetDashBoradDetails(DateTime fromDate, DateTime toDate);
        //[OperationContract]
        //Task<Port> AddPortAsync(Port portdata);

        //[OperationContract]
        //Task<Port> GetPortIDAsync(long id);


        //[OperationContract]
        //Task<Port> DelPortAsync(long id);

        //[OperationContract]
        //Task<List<Port>> GetPortsAsync();




        //List<DashBoard> GetDashBoradDetails();
        //[OperationContract]
        //List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(DateTime fromDate, DateTime toDate);
        // [OperationContract]
        //List<DashBoard> GetDashBoradDetails(DateTime fromDate, DateTime toDate);
    }
}