using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{

    public class LicensingRequestClient : UserClientBase<ILicensingRequestService>, ILicensingRequestService
    {
        public List<LicenseRequestVO> GetLicensingRequestlist()
        {
            return WrapOperationWithException(() => Channel.GetLicensingRequestlist());
        }

        public LicenseRequestVO GetLicensingRequestbyreference(string licenserequestrefid)
        {
            return WrapOperationWithException(() => Channel.GetLicensingRequestbyreference(licenserequestrefid));
        }

        public LicenseRequestVO GetLicensingRequest(int licenserequestid)
        {
            return WrapOperationWithException(() => Channel.GetLicensingRequest(licenserequestid));
        }

        public LicenseRequestVO AddLicensingRequest(LicenseRequestVO licensingrequestdata)
        {
            return WrapOperationWithException(() => Channel.AddLicensingRequest(licensingrequestdata));
        }

        public LicenseRequestVO ModifyLicensingRequest(Domain.ValueObjects.LicenseRequestVO licensingrequestdata)
        {
            return WrapOperationWithException(() => Channel.ModifyLicensingRequest(licensingrequestdata));
        }

        public LicenseRequestReferenceVO GetLicenseRequestReferenceVO()
        {
            return WrapOperationWithException(() => Channel.GetLicenseRequestReferenceVO());
        }

        //public LicenseRequestReferenceVO GetLicenseRequestReferenceVOAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetLicenseRequestReferenceVO());
        //}

        //public Task<List<LicenseRequestVO>> GetLicensingRequestlistAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetLicensingRequestlistAsync());
        //}

        //public Task<LicenseRequestVO> AddLicensingRequestAsync(LicenseRequestVO licensingrequestdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddLicensingRequestAsync(licensingrequestdata));
        //}

        //public Task<LicenseRequestVO> ModifyLicensingRequestAsync(LicenseRequestVO licensingrequestdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyLicensingRequestAsync(licensingrequestdata));
        //}

        public void ApproveLicenseRegistration(string licensereqid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveLicenseRegistration(licensereqid, remarks, taskcode));
        }

        public void VerifyLicenseRegistration(string licensereqid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.VerifyLicenseRegistration(licensereqid, remarks, taskcode));
        }

        public void RejectLicenseRegistration(string licensereqid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectLicenseRegistration(licensereqid, remarks, taskcode));
        }

        public bool CheckReferenceNoExists(string referenceno)
        {
            return WrapOperationWithException(() => Channel.CheckReferenceNoExists(referenceno));
        }
    }
}