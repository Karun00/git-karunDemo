using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IServiceTypeRepository
    {
        List<ServiceTypeVO> ServiceTypeDetails();
        ServiceTypeVO AddServiceType(ServiceTypeVO serviceTypeData, int userId);
        ServiceTypeVO ModifyServiceType(ServiceTypeVO serviceTypeData, int userId);
    }
}
