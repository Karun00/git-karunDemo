using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IPortGeneralConfigsRepository
    {
        List<PortGeneralConfig> GetAllPortGeneralConfigsDetails(string portCode);
        List<PortGeneralConfig> GetAllGroupNames(string GroupName, string portCode);
        string GetWFApprovedCode(string portcode);
        string GetPortConfiguration(string portcode,string configName);
        string GetReportPeriod(string portcode);
    }
}
