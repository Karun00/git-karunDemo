using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ScheduledTaskMapExtension
    {

        public static List<String> MapToDTO(this List<ScheduledTasksViewVO> scheduledTasks)
        {
            List<ScheduledTasksViewVO> scheduledTaskVos = new List<ScheduledTasksViewVO>();
            List<string> strlist = new List<string>();
            foreach (var value in scheduledTasks)
            {
                switch (value.MovementType)
                {
                    case MovementTypes.ARRIVAL:
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Daylight != null)
                        {
                            strlist.Add(value.Daylight);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.FromBetrth != null)
                        {
                            strlist.Add(value.FromBetrth);
                        }
                        if (value.SpaceonBerth != null)
                        {
                            strlist.Add(value.SpaceonBerth);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }
                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                    case MovementTypes.SHIFTING:
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Daylight != null)
                        {
                            strlist.Add(value.Daylight);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.FromBetrth != null)
                        {
                            strlist.Add(value.FromBetrth);
                        }
                        if (value.SpaceonBerth != null)
                        {
                            strlist.Add(value.SpaceonBerth);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }
                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                    case MovementTypes.SAILING:
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Daylight != null)
                        {
                            strlist.Add(value.Daylight);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.FromBetrth != null)
                        {
                            strlist.Add(value.FromBetrth);
                        }
                        if (value.SpaceonBerth != null)
                        {
                            strlist.Add(value.SpaceonBerth);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }
                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                    case MovementTypes.WARPING:
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Daylight != null)
                        {
                            strlist.Add(value.Daylight);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.Warp != null)
                        {
                            strlist.Add(value.Warp);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }

                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                    case "WTST":
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.FromBetrth != null)
                        {
                            strlist.Add(value.FromBetrth);
                        }
                        if (value.Quantityintons != null)
                        {
                            strlist.Add(value.Quantityintons);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }

                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                    case "FCST":
                        if (value.VCN != null)
                        {
                            strlist.Add(value.VCN);
                        }
                        if (value.VesselName != null)
                        {
                            strlist.Add(value.VesselName);
                        }
                        if (value.LOA != null)
                        {
                            strlist.Add(value.LOA);
                        }
                        if (value.Draft != null)
                        {
                            strlist.Add(value.Draft);
                        }
                        if (value.VesselType != null)
                        {
                            strlist.Add(value.VesselType);
                        }
                        if (value.GRT != null)
                        {
                            strlist.Add(value.GRT);
                        }
                        if (value.IMDG != null)
                        {
                            strlist.Add(value.IMDG);
                        }
                        if (value.Movement != null)
                        {
                            strlist.Add(value.Movement);
                        }
                        if (value.FromBetrth != null)
                        {
                            strlist.Add(value.FromBetrth);
                        }
                        if (value.MovementTime != null)
                        {
                            strlist.Add(value.MovementTime);
                        }
                        if (value.Tidal != null)
                        {
                            strlist.Add(value.Tidal);
                        }
                        if (value.OwnSteam != null)
                        {
                            strlist.Add(value.OwnSteam);
                        }
                        if (value.Nomainengine != null)
                        {
                            strlist.Add(value.Nomainengine);
                        }
                        if (value.SideAlongSide != null)
                        {
                            strlist.Add(value.SideAlongSide);
                        }
                        if (value.IsTidal != null)
                        {
                            strlist.Add(value.IsTidal);
                        }
                        break;
                }
            }
            return strlist;
        }
    }
}
