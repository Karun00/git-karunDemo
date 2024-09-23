using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Clients
{
    public class Hour24Report625ServiceClient : UserClientBase<IHour24Report625Service>, IHour24Report625Service
    {
        public List<Hour24Report625VO> GetHour24Report625SList()
        {
            return WrapOperationWithException(() => Channel.GetHour24Report625SList());
        }

        public Hour24Report625VO AddHour24Report625(Hour24Report625VO hoursereportdata)
        {
            return WrapOperationWithException(() => Channel.AddHour24Report625(hoursereportdata));
        }

        public Hour24Report625VO Gethoursreportdetailsbyid(string value, int id)
        {
            return WrapOperationWithException(() => Channel.Gethoursreportdetailsbyid(value, id));
        }


        public Hour24ReportReferenceDataVO GetHour24ReportReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetHour24ReportReferenceData());
        }

        public Hour24Report625VO EditHour24Report625(Hour24Report625VO hoursereportdata)
        {
            return WrapOperationWithException(() => Channel.EditHour24Report625(hoursereportdata));
        }
    }
}