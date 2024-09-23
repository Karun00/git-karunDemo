using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BerthPlanningConfigurationsService : ServiceBase, IBerthPlanningConfigurationsService
    {
        public BerthPlanningConfigurationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public BerthPlanningConfigurationsService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }


        /// <summary>
        ///  To Get Berth Planning Configurations Details
        /// </summary>
        /// <returns></returns>
        public List<BerthPlanningConfigurationsVO> BerthPlanningConfigurationsDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<BerthPlanningConfigurationsVO> _BerthPlanningConfigsist = new List<BerthPlanningConfigurationsVO>();

                _BerthPlanningConfigsist = (from bp in _unitOfWork.Repository<BerthPlanningConfigurations>().Query().Select().
                                                AsEnumerable<BerthPlanningConfigurations>()
                                            select new BerthPlanningConfigurationsVO
                            {
                                BerthPlanConfigid = bp.BerthPlanConfigid,
                                Days = bp.Days,
                                Slot = bp.Slot,
                                PortCode = bp.PortCode,
                                RecordStatus = bp.RecordStatus,
                                CreatedBy = bp.CreatedBy,
                                CreatedDate = bp.CreatedDate,
                                ModifiedBy = bp.ModifiedBy,
                                ModifiedDate = bp.ModifiedDate,
                            }).ToList();

                return _BerthPlanningConfigsist;
            });
        }
        

        /// <summary>
        /// To Add Berth Planning Configurations Data
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        public BerthPlanningConfigurationsVO AddBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                berthplanconfigdata.CreatedBy = _UserId;
                berthplanconfigdata.CreatedDate = DateTime.Now;
                berthplanconfigdata.ModifiedBy = _UserId;
                berthplanconfigdata.ModifiedDate = DateTime.Now;

                BerthPlanningConfigurations BerthPlanningConfigs = new BerthPlanningConfigurations();
                BerthPlanningConfigs = BerthPlanningConfigurationsMapExtension.MapToEntity(berthplanconfigdata);
                BerthPlanningConfigs.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<BerthPlanningConfigurations>().Insert(BerthPlanningConfigs);
                _unitOfWork.SaveChanges();

                return berthplanconfigdata;
            });
        }



        /// <summary>
        ///  To Modify Berth Planning Configurations Data
        /// </summary>
        /// <param name="berthplanconfigdata"></param>
        /// <returns></returns>
        public BerthPlanningConfigurationsVO ModifyBerthPlanConfig(BerthPlanningConfigurationsVO berthplanconfigdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                berthplanconfigdata.ModifiedBy = _UserId;
                berthplanconfigdata.ModifiedDate = DateTime.Now;

                BerthPlanningConfigurations BerthPlanningConfigs = new BerthPlanningConfigurations();
                BerthPlanningConfigs = BerthPlanningConfigurationsMapExtension.MapToEntity(berthplanconfigdata);

                _unitOfWork.Repository<BerthPlanningConfigurations>().Update(BerthPlanningConfigs);
                _unitOfWork.SaveChanges();


                return berthplanconfigdata;
            });
        }
    }
}
