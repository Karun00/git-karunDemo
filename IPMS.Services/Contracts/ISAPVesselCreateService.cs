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
    public interface ISAPVesselCreateService
    {
        [OperationContract(Action = "/ZIPMS_VESSEL_DETAILS")]
        [FaultContract(typeof(Exception))]
        SAPVesselCreateVO ZIPMS_VESSEL_DETAILS(SAPVesselCreateVO objVessel);
    }
}
