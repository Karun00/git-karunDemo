﻿using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceContract(Namespace = "urn:sap-com:document:sap:rfc:functions")]
    public interface ISAPMarineOrderCreateService
    {
        [OperationContract(Action = "/ZIPMS_MRN_ORDER")]
        [FaultContract(typeof(Exception))]
        SAPMarineOrderVO ZipmsMrnOrder(SAPMarineOrderVO objMarineOrder);
    }
}