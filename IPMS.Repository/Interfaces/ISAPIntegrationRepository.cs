using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface ISAPIntegrationRepository
    {
        ArrivalNotificationVO GetArrivalDataforSAPByVCN(string VCN);
        List<SAPArrivalVO> GetAllArrivalDataforSAP();
        List<SAPArrivalVO> GetAllUpdateArrivalDataforSAP();
        List<SAPMarineOrderVO> GetAllMarineDataforSAP();
        List<SAPMarineOrderVO> GetAllMarineUpdateDataforSAP();
        List<SAPVesselCreateVO> GetAllVesselDataforSAP();
        List<SAPInvoiceVO> GetAllInvoiceDataforSAP();
        List<SAPPosting> GetSAPPendingNotifications(int laterThanId);
    }
}
