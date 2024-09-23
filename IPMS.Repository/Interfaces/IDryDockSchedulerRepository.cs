using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPMS.Repository
{
    public interface IDryDockSchedulerRepository
    {
        List<SuppDryDock> GetPendingVesselForDryDock(string portCode);

        List<Berth> GetDockList(string portCode);

        //List<SuppDryDock> GetScheduledVesselForDryDock(string portCode);

        List<SuppDryDock> GetScheduledVesselForDryDock(string portCode,string quayCode,string dockCode);

    }
}
