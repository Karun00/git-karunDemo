using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IEntityRepository
    {
        List<EntityVO> GetEntities();

        List<EntityVO> GetFeaturesEntity();

        Entity GetEntityByCode(string pEntityCode);

        List<EntityModulesVO> EntityDetails();

        List<EntityModulesVO> GetAllSubModules();

        Entity GetEntitiesNotification(string entityCode);

    }
}
