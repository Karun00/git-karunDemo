using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IHour24Report625Service
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Hour24Report625VO> GetHour24Report625SList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Hour24ReportReferenceDataVO GetHour24ReportReferenceData();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        Hour24Report625VO AddHour24Report625(Hour24Report625VO hoursereportdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Hour24Report625VO Gethoursreportdetailsbyid(string value, int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Hour24Report625VO EditHour24Report625(Hour24Report625VO hoursereportdata);
    }
}
