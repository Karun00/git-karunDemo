using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceContract(Namespace = "urn:sap-com:document:sap:rfc:functions")]
    public interface ISAPVesselArrivalChangeService
    {
        [OperationContract(Action = "/ZIPMS_VESSEL_ARRIVAL_CHNG")]
        [FaultContract(typeof(Exception))]
        SAPArrivalResponseVO ZIPMS_VESSEL_ARRIVAL_CHNG(SAPArrivalVO objVesselArrival);
    }
}
