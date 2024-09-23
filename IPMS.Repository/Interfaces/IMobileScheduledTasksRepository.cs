using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public interface IMobileScheduledTasksRepository
    {
        ResourceAllocationVO GetScheduleTaskDetailsById(string resourceAllocationId);

        CompanyVO GetUserDetails(int UserId);

        Entity GetEntities(string entityCode);


    }
}
