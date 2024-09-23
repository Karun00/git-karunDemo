using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using IPMS.Domain;


namespace IPMS.Repository
{
    public class DryDockSchedulerRepository : IDryDockSchedulerRepository
    {
        private IUnitOfWork _unitOfWork;
        public DryDockSchedulerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SuppDryDock> GetPendingVesselForDryDock(string portCode)
        {
            List<SuppDryDock> pendingVessels = new List<SuppDryDock>();

            pendingVessels = (from a in _unitOfWork.Repository<SuppDryDock>().Query().
                                  Include(t => t.ArrivalNotification).
                                  Include(t=> t.ArrivalNotification.Berth1).
                                  Include(t => t.ArrivalNotification.Vessel).
                                  Include(t => t.ArrivalNotification.Agent).
                                  Include(t => t.WorkflowInstance.SubCategory).Select()
                              where a.ScheduleStatus == ScheduleStatus.New && a.ArrivalNotification.PortCode == portCode && a.WorkflowInstance.WorkflowTaskCode == WFStatus.Confirmed
                              select a

                                 ).ToList();

            return pendingVessels;
        }

        //public List<SuppDryDock> GetScheduledVesselForDryDock(string portCode)
        //{
        //    List<SuppDryDock> pendingVessels = new List<SuppDryDock>();

        //    pendingVessels = (from a in _unitOfWork.Repository<SuppDryDock>().Query().
        //                          Include(t => t.ArrivalNotification).
        //                          Include(t => t.ArrivalNotification.Vessel).
        //                          Include(t => t.ArrivalNotification.Agent).
        //                          Include(t => t.WorkflowInstance.SubCategory).Select()
        //                      where a.ScheduleStatus != "NEW" && a.ArrivalNotification.PortCode
        //  == portCode && (a.WorkflowInstance.WorkflowTaskCode != "WFRE" || a.WorkflowInstance.WorkflowTaskCode!="REJ")
        //                      select a

        //                         ).ToList();

        //    return pendingVessels;
        //}

        public List<Berth> GetDockList(string portCode)
        {
            List<Berth> dryDocks = new List<Berth>();

            dryDocks = (from d in _unitOfWork.Repository<Berth>().Query().Select()
                        where d.BerthType == "DRDK" && d.PortCode == portCode && d.RecordStatus == "A"
                        select new Berth
                        {
                            PortCode = d.PortCode,
                            QuayCode = d.QuayCode,
                            BerthName = d.BerthName,
                            BerthCode = d.BerthCode,
                            Lengthm = d.Lengthm
                        }
                ).ToList();


            return dryDocks;

        }

        public List<SuppDryDock> GetScheduledVesselForDryDock(string portCode, string quayCode, string dockCode)
        {
            List<SuppDryDock> pendingVessels = new List<SuppDryDock>();

            pendingVessels = (from a in _unitOfWork.Repository<SuppDryDock>().Query().
                                  Include(t => t.Berth).
                                  Include(t => t.ArrivalNotification).
                                  Include(t => t.ArrivalNotification.Berth1).
                                  Include(t => t.ArrivalNotification.Vessel).
                                  Include(t => t.ArrivalNotification.Agent).
                                  Include(t => t.WorkflowInstance.SubCategory).Select()
                              where a.ScheduleStatus != ScheduleStatus.New && a.ArrivalNotification.PortCode
                              == portCode && a.WorkflowInstance.WorkflowTaskCode != WFStatus.Reject && a.DockPortCode == portCode && a.DockQuayCode == quayCode && a.DockBerthCode == dockCode
                              select a

                                 ).ToList();

            return pendingVessels;
        }
    }
}
