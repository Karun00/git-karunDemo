using System.Collections.Generic;
using IPMS.Domain.Models;
using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Linq;
using System.Data.SqlClient;
using System;

namespace IPMS.Repository
{
    public class ResourceAllocationConfigRuleRepository : IResourceAllocationConfigRuleRepository
    {
        private IUnitOfWork _unitOfWork;

        public ResourceAllocationConfigRuleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetresourceAllocationconfigurationruledetails
        /// <summary>
        /// Method to Get ResourceAllocationconfigurationruledetails
        /// </summary>
        /// <param name="PortID"></param>
        /// <returns></returns>
        public List<ResourceAllocationConfigRule> GetresourceAllocationconfigurationruledetails(string PortID)
        {
            var querytemp = (from t in _unitOfWork.Repository<ResourceAllocationConfigRule>().Query()
                                 .Include(t => t.ResourceGangConfigs)
                                 .Include(t => t.ResourceAllocationMovementTypeRules).Select()
                             where t.PortCode == PortID && t.RecordStatus == "A"
                             orderby t.EffectedFrom descending
                             select t).ToList<ResourceAllocationConfigRule>();
            return querytemp;
        }
        #endregion

        #region GetResourceAllocationConfigRulemovementtypedetails
        /// <summary>
        /// Method to Get ResourceAllocationConfigRule movement type details
        /// </summary>
        /// <returns></returns>
        public List<ServiceType> GetResourceAllocationConfigRulemovementtypedetails()
        {
            var querytemp = (from t in _unitOfWork.Repository<ServiceType>().Queryable()
                             where t.IsServiceType == "N"
                             select t).ToList<ServiceType>();
            return querytemp;
        }
        #endregion

        #region AddResourceAllocationConfigRule
        /// <summary>
        /// Method to Add ResourceAllocationConfigRule
        /// </summary>
        /// <param name="RACRdata"></param>
        /// <returns></returns>
        public ResourceAllocationConfigRuleVO AddResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRdata, int userid, string portcode)
        {
            if (RACRdata != null)
            {
                RACRdata.CreatedBy = userid;
                RACRdata.CreatedDate = DateTime.Now;
                RACRdata.ModifiedBy = userid;
                RACRdata.ModifiedDate = DateTime.Now;
                RACRdata.RecordStatus = "A";

                ResourceAllocationConfigRule resourceallocationconfigrule = null;
                resourceallocationconfigrule = RACRdata.MapToEntity();

                List<ResourceGangConfig> applgangconfigs = resourceallocationconfigrule.ResourceGangConfigs.ToList();
            //    List<ResourceAllocationMovementTypeRule> applmovementtyperules = resourceallocationconfigrule.ResourceAllocationMovementTypeRules.ToList();
                resourceallocationconfigrule.ObjectState = ObjectState.Added;

                resourceallocationconfigrule.ResourceGangConfigs = null;
                resourceallocationconfigrule.ResourceAllocationMovementTypeRules = null;

                resourceallocationconfigrule.PortCode = portcode;
                _unitOfWork.Repository<ResourceAllocationConfigRule>().Insert(resourceallocationconfigrule);

                if (applgangconfigs.Count > 0)
                {
                    foreach (var applgangconfig in applgangconfigs)
                    {
                        applgangconfig.ResourceAllocationConfigRuleID = resourceallocationconfigrule.ResourceAllocationConfigRuleID;
                    }
                    _unitOfWork.Repository<ResourceGangConfig>().InsertRange(applgangconfigs);
                }

                if (RACRdata.arrivalservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRdata.arrivalservicetype)
                    {
                        ResourceAllocationMovementTypeRule resourceAlloMovementTypeRule = new ResourceAllocationMovementTypeRule();
                        resourceAlloMovementTypeRule.ResourceAllocationConfigRuleID = resourceallocationconfigrule.ResourceAllocationConfigRuleID;
                        resourceAlloMovementTypeRule.PortCode = portcode;
                        resourceAlloMovementTypeRule.MovementType = "ARMV";
                        resourceAlloMovementTypeRule.ServiceTypeID = ArvSrvTyp;
                        resourceAlloMovementTypeRule.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(resourceAlloMovementTypeRule);
                    }
                }

                if (RACRdata.shiftingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRdata.shiftingservicetype)
                    {
                        ResourceAllocationMovementTypeRule resourceAlloMovementTypeRule = new ResourceAllocationMovementTypeRule();
                        resourceAlloMovementTypeRule.ResourceAllocationConfigRuleID = resourceallocationconfigrule.ResourceAllocationConfigRuleID;
                        resourceAlloMovementTypeRule.PortCode = portcode;
                        resourceAlloMovementTypeRule.MovementType = "SHMV";
                        resourceAlloMovementTypeRule.ServiceTypeID = ArvSrvTyp;
                        resourceAlloMovementTypeRule.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(resourceAlloMovementTypeRule);
                    }
                }

                if (RACRdata.sailingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRdata.sailingservicetype)
                    {
                        ResourceAllocationMovementTypeRule resourceAlloMovementTypeRule = new ResourceAllocationMovementTypeRule();
                        resourceAlloMovementTypeRule.ResourceAllocationConfigRuleID = resourceallocationconfigrule.ResourceAllocationConfigRuleID;
                        resourceAlloMovementTypeRule.PortCode = portcode;
                        resourceAlloMovementTypeRule.MovementType = "SGMV";
                        resourceAlloMovementTypeRule.ServiceTypeID = ArvSrvTyp;
                        resourceAlloMovementTypeRule.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(resourceAlloMovementTypeRule);
                    }
                }

                if (RACRdata.warpingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRdata.warpingservicetype)
                    {
                        ResourceAllocationMovementTypeRule resourceAlloMovementTypeRule = new ResourceAllocationMovementTypeRule();
                        resourceAlloMovementTypeRule.ResourceAllocationConfigRuleID = resourceallocationconfigrule.ResourceAllocationConfigRuleID;
                        resourceAlloMovementTypeRule.PortCode = portcode;
                        resourceAlloMovementTypeRule.MovementType = "WRMV";
                        resourceAlloMovementTypeRule.ServiceTypeID = ArvSrvTyp;
                        resourceAlloMovementTypeRule.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(resourceAlloMovementTypeRule);
                    }
                }

                //Save Changes
                _unitOfWork.SaveChanges();

                RACRdata = resourceallocationconfigrule.MapToDTO();
            }
            return RACRdata;
        }
        #endregion

        #region ModifyResourceAllocationConfigRule
        /// <summary>
        /// Method to update ResourceAllocationConfigRule
        /// </summary>
        /// <param name="RACRvo"></param>
        /// <returns></returns>
        public ResourceAllocationConfigRuleVO ModifyResourceAllocationConfigRule(ResourceAllocationConfigRuleVO RACRvo, int _UserId, string _PortCode)
        {
            if (RACRvo != null)
            {
                RACRvo.ModifiedBy = _UserId;
                RACRvo.ModifiedDate = DateTime.Now;
                RACRvo.RecordStatus = "A";

                ResourceAllocationConfigRule RACRentity = null;
                RACRentity = RACRvo.MapToEntity();

                List<ResourceGangConfig> resourcegang = RACRentity.ResourceGangConfigs.ToList();
               //List<ResourceAllocationMovementTypeRule> resourcemonvementtype = RACRentity.ResourceAllocationMovementTypeRules.ToList();

                RACRentity.ResourceGangConfigs = null;
                RACRentity.ResourceAllocationMovementTypeRules = null;
                RACRentity.PortCode = _PortCode;
                RACRentity.ObjectState = ObjectState.Modified;
                RACRentity.ModifiedBy = _UserId;
                RACRentity.ModifiedDate = DateTime.Now;
                _unitOfWork.Repository<ResourceAllocationConfigRule>().Update(RACRentity);

                _unitOfWork.ExecuteSqlCommand(" delete dbo.ResourceGangConfig where ResourceAllocationConfigRuleID =  @p0", RACRentity.ResourceAllocationConfigRuleID);
                _unitOfWork.ExecuteSqlCommand(" delete dbo.ResourceAllocationMovementTypeRule where ResourceAllocationConfigRuleID =  @p0", RACRentity.ResourceAllocationConfigRuleID);

                if (resourcegang.Count > 0)
                {
                    foreach (var gang in resourcegang)
                    {
                        gang.ResourceAllocationConfigRuleID = RACRentity.ResourceAllocationConfigRuleID;
                        gang.ObjectState = ObjectState.Added;
                    }
                    _unitOfWork.Repository<ResourceGangConfig>().InsertRange(resourcegang);
                }

                if (RACRvo.arrivalservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRvo.arrivalservicetype)
                    {
                        ResourceAllocationMovementTypeRule RAMTRentity = new ResourceAllocationMovementTypeRule();
                        RAMTRentity.ResourceAllocationConfigRuleID = RACRentity.ResourceAllocationConfigRuleID;
                        RAMTRentity.PortCode = _PortCode;
                        RAMTRentity.MovementType = "ARMV";
                        RAMTRentity.ServiceTypeID = ArvSrvTyp;
                        RAMTRentity.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(RAMTRentity);
                    }
                }

                if (RACRvo.shiftingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRvo.shiftingservicetype)
                    {
                        ResourceAllocationMovementTypeRule RAMTRentity = new ResourceAllocationMovementTypeRule();
                        RAMTRentity.ResourceAllocationConfigRuleID = RACRentity.ResourceAllocationConfigRuleID;
                        RAMTRentity.PortCode = _PortCode;
                        RAMTRentity.MovementType = "SHMV";
                        RAMTRentity.ServiceTypeID = ArvSrvTyp;
                        RAMTRentity.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(RAMTRentity);
                    }
                }

                if (RACRvo.sailingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRvo.sailingservicetype)
                    {
                        ResourceAllocationMovementTypeRule RAMTRentity = new ResourceAllocationMovementTypeRule();
                        RAMTRentity.ResourceAllocationConfigRuleID = RACRentity.ResourceAllocationConfigRuleID;
                        RAMTRentity.PortCode = _PortCode;
                        RAMTRentity.MovementType = "SGMV";
                        RAMTRentity.ServiceTypeID = ArvSrvTyp;
                        RAMTRentity.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(RAMTRentity);
                    }
                }

                if (RACRvo.warpingservicetype.Count > 0)
                {
                    foreach (var ArvSrvTyp in RACRvo.warpingservicetype)
                    {
                        ResourceAllocationMovementTypeRule RAMTRentity = new ResourceAllocationMovementTypeRule();
                        RAMTRentity.ResourceAllocationConfigRuleID = RACRentity.ResourceAllocationConfigRuleID;
                        RAMTRentity.PortCode = _PortCode;
                        RAMTRentity.MovementType = "WRMV";
                        RAMTRentity.ServiceTypeID = ArvSrvTyp;
                        RAMTRentity.ObjectState = ObjectState.Added;
                        _unitOfWork.Repository<ResourceAllocationMovementTypeRule>().Insert(RAMTRentity);
                    }
                }

                //Save Changes
                _unitOfWork.SaveChanges();
            }

            return RACRvo;
        }
        #endregion
    }
}
