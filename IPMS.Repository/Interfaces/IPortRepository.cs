using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IPortRepository
    {
        List<PortVO> GetPorts();
        List<PortVO> GetPortzs();
        List<PortVO> GetLoginPort(string loginPort);
        //List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(int UserID, string fromDate, string toDate, string PortCode);
        List<PortVO> GetPortsByUser(int userId);

        List<PortCodeNameVO> GetAllExceptLoginPort(string loginPort);

        Port GetPortDetailsByPortCode(string portCode);
    }
}
