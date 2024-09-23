using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
               ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class DryDockSchedulerService : ServiceBase, IDryDockSchedulerService
    {
        private IDryDockSchedulerRepository _dryDockSchedulerRepository;
        public DryDockSchedulerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dryDockSchedulerRepository = new DryDockSchedulerRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);

        }

        public DryDockSchedulerService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _dryDockSchedulerRepository = new DryDockSchedulerRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// GET Pending vessels for dry dock
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> GetPendingVesselForDryDock()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dryDockSchedulerRepository.GetPendingVesselForDryDock(_PortCode).MapToDto();
            });
        }

        ///// <summary>
        ///// GET Dock scheduled vessels
        ///// </summary>
        ///// <returns></returns>
        //public List<SuppDryDockVO> GetScheduledVesselForDryDock()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        return _dryDockSchedulerRepository.GetScheduledVesselForDryDock(_PortCode).MapToDTO();
        //    });
        //}

        /// <summary>
        /// Get Docks list
        /// </summary>
        /// <returns></returns>
        public List<BerthVO> GetDockList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dryDockSchedulerRepository.GetDockList(_PortCode).MapToListDto();
            });
        }

        public SuppDryDock AddScheduleDryDock(SuppDryDockVO data)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 //Berth berthDtl = _unitOfWork.Repository<Berth>().Query().Select().Where(t => t.BerthCode == suppDockdata.DockBerthCode).FirstOrDefault();

                 SuppDryDock suppDock = null;
                 suppDock = data.MapToEntity();

                 suppDock.ModifiedBy = _UserId;
                 suppDock.ModifiedDate = DateTime.Now;
                 suppDock.ScheduleStatus = "PLND";
                 suppDock.DockPortCode = data.DockPortCode;
                 suppDock.DockQuayCode = data.DockQuayCode;
                 suppDock.DockBerthCode = data.DockBerthCode;

                 suppDock.ObjectState = ObjectState.Modified;

                 if (suppDock.EnteredDockDateTime == DateTime.MinValue)
                     suppDock.EnteredDockDateTime = null;
                 if (suppDock.FinishedDockDateTime == DateTime.MinValue)
                     suppDock.FinishedDockDateTime = null;

                 _unitOfWork.Repository<SuppDryDock>().Update(suppDock);
                 _unitOfWork.SaveChanges();

                 return suppDock;
             });
        }

        public SuppDryDock UnPlannedScheduleDryDock(SuppDryDockVO data)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SuppDryDock suppDock = new SuppDryDock();


                 suppDock.ModifiedBy = _UserId;
                 suppDock.ModifiedDate = DateTime.Now;
                 suppDock.ScheduleStatus = "NEW";
                 suppDock.SuppDryDockID = data.SuppDryDockID;
                 //added by srinivas
                 suppDock.DockPortCode = data.DockPortCode;
                // suppDock.DockQuayCode = data.DockQuayCode;
                 //suppDock.DockBerthCode = data.DockBerthCode;
                 //added by srinivas
                 //_unitOfWork.ExecuteSqlCommand("Update SuppDryDock set ScheduleStatus = @p0, ModifiedBy = @p1, ModifiedDate = @p2,DockPortCode=@p3,DockQuayCode=@p4,DockBerthCode=@p5 where SuppDryDockID = @p6", suppDock.ScheduleStatus, suppDock.ModifiedBy, suppDock.ModifiedDate,DBNull.Value, DBNull.Value, DBNull.Value, suppDock.SuppDryDockID);
                 //added by srinivas
                 _unitOfWork.ExecuteSqlCommand("Update SuppDryDock set ScheduleStatus = @p0, ModifiedBy = @p1, ModifiedDate = @p2,DockPortCode=@p3,DockQuayCode=@p4,DockBerthCode=@p5 where SuppDryDockID = @p6", suppDock.ScheduleStatus, suppDock.ModifiedBy, suppDock.ModifiedDate, suppDock.DockPortCode, DBNull.Value, DBNull.Value, suppDock.SuppDryDockID);
                 //added by srinivas
                 return suppDock;
             });
        }

        public SuppScheduledDryDockVO ConfirmedScheduleDryDock(List<SuppScheduledDryDockVO> data)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SuppScheduledDryDockVO suppDocks = new SuppScheduledDryDockVO();
                 foreach (SuppScheduledDryDockVO suppDock in data)
                 {

                     suppDock.ModifiedBy = _UserId;
                     suppDock.ModifiedDate = DateTime.Now;
                     suppDock.ScheduleStatus = "CNFR";

                     _unitOfWork.ExecuteSqlCommand("Update SuppDryDock set ScheduleStatus = @p0, ModifiedBy = @p1, ModifiedDate = @p2 where SuppDryDockID = @p3", suppDock.ScheduleStatus, suppDock.ModifiedBy, suppDock.ModifiedDate, suppDock.id);
                 }
                 return suppDocks;
             });
        }

        /// <summary>
        /// GET Dock scheduled vessels
        /// </summary>
        /// <returns></returns>
        public List<SuppDryDockVO> GetScheduledVesselForDryDock(string quayCode,string dockCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _dryDockSchedulerRepository.GetScheduledVesselForDryDock(_PortCode, quayCode, dockCode).MapToDto();
            });
        }




    }
}
