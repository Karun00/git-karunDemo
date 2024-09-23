using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ILicensingRequestRepository
    {
        LicenseRequest GetLicensingRequestDetailsByid(int value);
        LicenseRequest GetLicensingRequestDetailsByrefid(string licrefId);
        List<LicenseRequestVO> GetLicensingRequestlist(string port);
        List<LicenseRequestVO> GetApprovedBunkers(string port, string lictype, string wrkflowcode);
        List<LicenseRequestVO> GetWasteDeclarations(string port, string lictype, string wrkflowcode);
        bool CheckReferenceNoExists(string referenceno);
    }
}
