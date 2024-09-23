using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ILicensingRequestService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<LicenseRequestVO> GetLicensingRequestlist();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        LicenseRequestVO GetLicensingRequest(int licenserequestid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        LicenseRequestVO GetLicensingRequestbyreference(string licenserequestrefid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        LicenseRequestVO AddLicensingRequest(LicenseRequestVO licensingrequestdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        LicenseRequestVO ModifyLicensingRequest(LicenseRequestVO licensingrequestdata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        LicenseRequestReferenceVO GetLicenseRequestReferenceVO();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveLicenseRegistration(string licensereqid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyLicenseRegistration(string licensereqid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectLicenseRegistration(string licensereqid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool CheckReferenceNoExists(string referenceno);
    }
}




