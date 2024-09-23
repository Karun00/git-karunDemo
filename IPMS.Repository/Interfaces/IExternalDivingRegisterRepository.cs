using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IExternalDivingRegisterRepository
    {
        List<LicenseRequestVO> GetAllCompanies(string portCode);
        List<VesselVO> GetAllVessels(string portCode);
        List<ExternalDivingRegisterVO> AllExternalDivingRegisterDetails(string portCode);
        //ExternalDivingRegisterVO AddExternalDivingRegister(ExternalDivingRegisterVO entity, int _UserId);
        ExternalDivingRegisterVO ModifyExternalDivingRegister(ExternalDivingRegisterVO entity, int userId);
        ExternalDivingRegisterVO DeleteExternalDivingRegister(long id);
        ExternalDivingRegisterVO GetExternalDivingDetailsOnCompletion(string strExternalDivingRegisterId);
    }
}
