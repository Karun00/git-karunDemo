using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPMS.Repository
{
    public interface IIncidentReportRepository
    {
        List<IncidentVO> GetIncidentReportList(string portCode);
    }
}
