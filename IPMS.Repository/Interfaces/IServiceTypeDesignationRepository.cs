using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IServiceTypeDesignationRepository
    {
        List<ServiceTypeVO> ServiceTypeDesignationDetails(string portCode);

        ServiceTypeVO AddServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData, int userId, string portCode);

        ServiceTypeVO ModifyServiceTypeDesignation(ServiceTypeVO serviceTypeDesignationData, int userId, string portCode);

        List<SubCategory> GetDesignations();

        List<SubCategory> GetCraftTypes(string portCode);

        List<ServiceType> GetServiceTypes();
    }
}
