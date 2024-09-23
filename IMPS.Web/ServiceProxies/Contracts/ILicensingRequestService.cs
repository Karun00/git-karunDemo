using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ILicensingRequestService : IDisposable
    {
        [OperationContract]
        List<LicenseRequestVO> GetLicensingRequestlist();
        [OperationContract]
        LicenseRequestVO GetLicensingRequest(int licenserequestid);
        [OperationContract]
        LicenseRequestVO GetLicensingRequestbyreference(string licenserequestrefid);
        [OperationContract]
        LicenseRequestVO AddLicensingRequest(LicenseRequestVO licensingrequestdata);
        [OperationContract]
        LicenseRequestVO ModifyLicensingRequest(LicenseRequestVO licensingrequestdata);
        [OperationContract]
        LicenseRequestReferenceVO GetLicenseRequestReferenceVO();
        //[OperationContract]
        //Task<List<LicenseRequestVO>> GetLicensingRequestlistAsync();
        //[OperationContract]
        //Task<LicenseRequestVO> AddLicensingRequestAsync(LicenseRequestVO licensingrequestdata);
        //[OperationContract]
        //Task<LicenseRequestVO> ModifyLicensingRequestAsync(LicenseRequestVO licensingrequestdata);
        //[OperationContract]
        //LicenseRequestReferenceVO GetLicenseRequestReferenceVOAsync();
        [OperationContract]
        void ApproveLicenseRegistration(string licensereqid, string remarks, string taskcode);
        [OperationContract]
        void VerifyLicenseRegistration(string licensereqid, string remarks, string taskcode);
        [OperationContract]
        void RejectLicenseRegistration(string licensereqid, string remarks, string taskcode);

        [OperationContract]
        bool CheckReferenceNoExists(string referenceno);
    }
}


