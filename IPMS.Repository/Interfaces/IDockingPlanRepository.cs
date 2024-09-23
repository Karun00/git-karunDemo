using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IDockingPlanRepository
    {
        List<DockingPlanVO> GetDockingPlanDetails(string portCode, int userId);

        List<DockingPlanVO> GetVesselNames(string searchValue, string portCode, string searchColumn);

        DockingPlanVO GetVesselsById(int vesselId);

        DockingPlan GetDockingPlanRequestDetailsById(string dockingPlanId);

        DockingPlan GetDockingPlanApproveId(string dockingplanid);

        List<DockingPlanVO> GetDockingPlan(int dockingPlanId);

        CompanyVO GetUserDetails(int userId);

    }
}
