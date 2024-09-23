using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class MarineRevenuePostingMapExtension
    {

        public static RevenuePostingSectionsVO MapToDto(this List<MarineRevenuePostingVO> revenuePostings)
        {
            RevenuePostingSectionsVO revenuepostingvo = new RevenuePostingSectionsVO();
            if (revenuePostings != null)
            {
            revenuepostingvo.PortDuesDetails = revenuePostings.FindAll(t => t.MovementName == "PORT DUES");
            revenuepostingvo.BerthDuesDetails = revenuePostings.FindAll(t => t.MovementName == "BERTH DUES");
            revenuepostingvo.RefuseRemovalDetails = revenuePostings.FindAll(t => t.MovementName == "REFUSE REMOVAL");
            revenuepostingvo.PortDuesDetailsView = revenuePostings.FindAll(t => t.MovementName == "PORTDUESVIEW").OrderBy(item => item.ResourceAllocationID).ToList();
            revenuepostingvo.ArrivalDetails = revenuePostings.FindAll(t => t.MovementName == "Arrival");
            revenuepostingvo.ShiftingDetails = revenuePostings.FindAll(t => t.MovementName == "Shifting");
            revenuepostingvo.WarpingDetails = revenuePostings.FindAll(t => t.MovementName == "Warping");
            revenuepostingvo.SailingDetails = revenuePostings.FindAll(t => t.MovementName == "Sailing");
            //revenuepostingvo.DryDockDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKSERVICES");
            revenuepostingvo.DryDockDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKSERVICES").OrderBy(item => item.MeterSerialNo).ToList();

            revenuepostingvo.DryDock12HrsDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCK12HRSSERVICES");
            revenuepostingvo.SupplimantoryDetails = revenuePostings.FindAll(t => t.ServiceName == "SUPPLIMANT");
            revenuepostingvo.DrydockMislaniousDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKMISCSERVICE");

            revenuepostingvo.DisplayInfo = revenuePostings.FindAll(t => t.MovementName == "DISPLAYONLY");
            }

            return revenuepostingvo;
        }

        public static RevenuePostingSectionsVO MapToDtoView(this List<MarineRevenuePostingVO> revenuePostings, string agentRegisterName, string accountNo)
        {
            RevenuePostingSectionsVO revenuepostingvo = new RevenuePostingSectionsVO();
            if (revenuePostings != null)
            {
                revenuepostingvo.RegisteredName = agentRegisterName;
                revenuepostingvo.AccountNo = accountNo;
            revenuepostingvo.PortDuesDetails = revenuePostings.FindAll(t => t.MovementName == "PORT DUES");
            revenuepostingvo.BerthDuesDetails = revenuePostings.FindAll(t => t.MovementName == "BERTH DUES");
            revenuepostingvo.RefuseRemovalDetails = revenuePostings.FindAll(t => t.MovementName == "REFUSE REMOVAL");               
            revenuepostingvo.PortDuesDetailsView = revenuePostings.FindAll(t => t.MovementName == "PORTDUESVIEW").OrderBy(item => item.ResourceAllocationID).ToList();             
            revenuepostingvo.ArrivalDetails = revenuePostings.FindAll(t => t.MovementName == "Arrival");
            revenuepostingvo.ShiftingDetails = revenuePostings.FindAll(t => t.MovementName == "Shifting");
            revenuepostingvo.WarpingDetails = revenuePostings.FindAll(t => t.MovementName == "Warping");
            revenuepostingvo.SailingDetails = revenuePostings.FindAll(t => t.MovementName == "Sailing");

            revenuepostingvo.DryDockDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKSERVICES").OrderBy(item => item.MeterSerialNo).ToList();

            //var DRYSER = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKSERVICES");
            //revenuepostingvo.DryDockDetails = DRYSER.OrderBy(t => t.MeterSerialNo).ToList();

            revenuepostingvo.DryDock12HrsDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCK12HRSSERVICES");
            revenuepostingvo.SupplimantoryDetails = revenuePostings.FindAll(t => t.ServiceName == "SUPPLIMANT");
            revenuepostingvo.DrydockMislaniousDetails = revenuePostings.FindAll(t => t.MovementName == "DRYDOCKMISCSERVICE");
            }
            return revenuepostingvo;
        }

    }
}
