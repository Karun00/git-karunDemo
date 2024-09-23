using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;


namespace IPMS.Repository
{
    public interface IAuditLogRepository
    {
        string UserActivityLogging(AuditTrailConfig auditTrailConfigDetails, AuditTrail auditTrailDetails);
    }
}
