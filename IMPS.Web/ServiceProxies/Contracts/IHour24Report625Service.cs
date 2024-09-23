using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IHour24Report625Service : IDisposable
    {
        [OperationContract]
        List<Hour24Report625VO> GetHour24Report625SList();

        [OperationContract]
        Hour24Report625VO AddHour24Report625(Hour24Report625VO hoursereportdata);
        [OperationContract]
        Hour24Report625VO Gethoursreportdetailsbyid(string value, int id);
        [OperationContract]
         Hour24ReportReferenceDataVO GetHour24ReportReferenceData();
        [OperationContract]
        Hour24Report625VO EditHour24Report625(Hour24Report625VO hoursereportdata);
    }
}
