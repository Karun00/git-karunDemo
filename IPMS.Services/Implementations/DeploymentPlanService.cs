using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                   ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DeploymentPlanService : ServiceBase, IDeploymentPlanService
    {
        private IDeploymentPlanRepository _deploymentplanRepository;
       // private INotificationPublisher _notificationpublisher;
      //  private IPortRepository _portRepository;
        private ISubCategoryRepository _subcategoryRepository;     
      //  private IPortConfigurationRepository _portConfigurationRepository;

        public DeploymentPlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _deploymentplanRepository = new DeploymentPlanRepository(_unitOfWork);
           // _notificationpublisher = new NotificationPublisher(_unitOfWork);
          //  _portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);          
         //   _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

            public DeploymentPlanService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _deploymentplanRepository = new DeploymentPlanRepository(_unitOfWork);
         //   _portRepository = new PortRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);          
        //    _portConfigurationRepository = new PortConfigurationRepository(_unitOfWork);
//_notificationpublisher = new NotificationPublisher(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

            /// <summary>
            ///  To Get Deployment Plan Details
            /// </summary>
            /// <returns></returns>
            public List<DeploymentPlanVO> DeploymentPlanDetails()
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    return _deploymentplanRepository.DeploymentPlanDetails(_PortCode);
                });
            }

            /// <summary>
            /// To Get Deployment Plan Reference Data While initialization  
            /// </summary>
            /// <returns></returns>
            public DeploymentPlanVO GetDeploymentPlanReferenceVO()
            {
                return ExecuteFaultHandledOperation(() =>
                    {
                        DeploymentPlanVO VO = new DeploymentPlanVO();
                        VO.FinancialYears = _deploymentplanRepository.GetFinancialYears();
                        VO.DredgingColors = _subcategoryRepository.GetDredgingColors().MapToDto();
                        VO.DredgingTypes = _subcategoryRepository.GetDredgingTypes().MapToDto();
                        VO.PortTypes = _deploymentplanRepository.GetPortTypes();
                        VO.CraftColors = _deploymentplanRepository.GetCraftColors(_PortCode);
                        return VO;
                    });
            }

           
           

            ///// <summary>
            ///// To Get Financial Year Data
            ///// </summary>
            ///// <returns></returns>
            //public List<FinancialYearVO> GetFinancialYear()
            //{
            //    return ExecuteFaultHandledOperation(() =>
            //    {
            //        return _deploymentplanRepository.GetFinancialYear();

            //    });
            //}
            /// <summary>
            ///  To Get Deployment Types
            /// </summary>
            /// <returns></returns>
            public List<SubCategoryVO> GetDeploymentPlanTypes()
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    SubCategoryRepository repository = new SubCategoryRepository(_unitOfWork);
                    List<SubCategory> subcategories = repository.GetDeploymentTypes();
                    return subcategories.MapToDto();
                });
            }

            /// <summary>
            ///  To Get Planned Deployment Details
            /// </summary>
            /// <returns></returns>
            public List<PlannedDeploymentVO> PlannedDeploymentDetails()
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    return _deploymentplanRepository.PlannedDeploymentDetails(_PortCode);
                });
            }


            /// <summary>
            /// To Get Craft Details
            /// </summary>
            /// <returns></returns>
            public List<PlannedDeploymentVO> GetDeploymentCraftNames()
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    return _deploymentplanRepository.GetDeploymentCraftNames();

                });
            }

            /// <summary>
            /// Add Deployment Plan
            /// </summary>
            /// <param name="request"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public DeploymentPlanVO AddDeploymentPlan(DeploymentPlanVO deploymentdata)
            {
                return EncloseTransactionAndHandleException(() =>
                {
                    string name = _LoginName;
                    deploymentdata.CreatedBy = _UserId;
                    deploymentdata.CreatedDate = DateTime.Now;
                    deploymentdata.ModifiedBy = _UserId;
                    deploymentdata.ModifiedDate = DateTime.Now;
                  //  deploymentdata.PortCode = _PortCode;
                    DeploymentPlan deploymentplan = new DeploymentPlan();
                    deploymentplan = DeploymentPlanMapExtension.MapToEntity(deploymentdata);
                    //List<DeploymentBudget> deploymentbudgets = deploymentplan.DeploymentBudgets.ToList();
                    //deploymentplan.DeploymentBudgets = null;

                    List<PlannedDeploymentVO> _DeploymentBudget = new List<PlannedDeploymentVO>();
                    _DeploymentBudget = deploymentdata.DeploymentBudget;

                    deploymentplan.DeploymentBudgets = null;
                    deploymentplan.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<DeploymentPlan>().Insert(deploymentplan);
                    _unitOfWork.SaveChanges();

                    List<DeploymentBudget> _DeploymentBudgetList = new List<DeploymentBudget>();
                    foreach (var deploymentbudget in _DeploymentBudget)
                    {
                        DeploymentBudget obj = new DeploymentBudget();
                        //obj.BerthCargoID = 0;
                        //obj.DeploymentBudgetID = 0;
                        obj.DeploymentPlanID = deploymentplan.DeploymentPlanID;
                        obj.DredgPlan = deploymentbudget.DredgPlan;
                        obj.Budget = deploymentbudget.Budget;
                        obj.DredgingType = deploymentbudget.SubCatCode;
                        obj.Apr = deploymentbudget.Apr;
                        obj.AprCraftID = deploymentbudget.AprCraftID;
                        obj.May = deploymentbudget.May;
                        obj.MayCraftID = deploymentbudget.MayCraftID;
                        obj.Jun = deploymentbudget.Jun;
                        obj.JunCraftID = deploymentbudget.JunCraftID;
                        obj.Jul = deploymentbudget.Jul;
                        obj.JulCraftID = deploymentbudget.JulCraftID;
                        obj.Aug = deploymentbudget.Aug;
                        obj.AugCraftID = deploymentbudget.AugCraftID;
                        obj.Sep = deploymentbudget.Sep;
                        obj.SepCraftID = deploymentbudget.SepCraftID;
                        obj.Oct = deploymentbudget.Oct;
                        obj.OctCraftID = deploymentbudget.OctCraftID;
                        obj.Nov = deploymentbudget.Nov;
                        obj.NovCraftID = deploymentbudget.NovCraftID;
                        obj.Dec = deploymentbudget.Dec;
                        obj.DecCraftID = deploymentbudget.DecCraftID;
                        obj.Jan = deploymentbudget.Jan;
                        obj.JanCraftID = deploymentbudget.JanCraftID;
                        obj.Feb = deploymentbudget.Feb;
                        obj.FebCraftID = deploymentbudget.FebCraftID;
                        obj.Mar = deploymentbudget.Mar;
                        obj.MarCraftID = deploymentbudget.MarCraftID;

                        obj.RecordStatus = deploymentdata.RecordStatus;
                        obj.CreatedBy = deploymentdata.CreatedBy;
                        obj.CreatedDate = deploymentdata.CreatedDate;
                        obj.ModifiedBy = deploymentdata.ModifiedBy;
                        obj.ModifiedDate = deploymentdata.ModifiedDate;

                        _DeploymentBudgetList.Add(obj);
                    }
                    _unitOfWork.Repository<DeploymentBudget>().InsertRange(_DeploymentBudgetList);
                    _unitOfWork.SaveChanges();


                    return deploymentdata;
                });
            }
            /// <summary>
            /// Update Deployment Plan
            /// </summary>
            /// <param name="request"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public DeploymentPlanVO ModifyDeploymentPlan(DeploymentPlanVO deploymentdata)
            {
                return EncloseTransactionAndHandleException(() =>
             {
                 // deploymentdata.PortCode = _PortCode;              
                 DeploymentPlan deploymentplan = new DeploymentPlan();
                 deploymentplan = DeploymentPlanMapExtension.MapToEntity(deploymentdata);
                 deploymentplan.ModifiedBy = _UserId;
                 deploymentplan.ModifiedDate = DateTime.Now;
                 deploymentplan.ObjectState = ObjectState.Modified;
                 _unitOfWork.Repository<DeploymentPlan>().Update(deploymentplan);
                 _unitOfWork.SaveChanges();

                 var brt = _unitOfWork.ExecuteSqlCommand(" delete dbo.DeploymentBudget where DeploymentPlanID = @p0", deploymentdata.DeploymentPlanID);

                 List<PlannedDeploymentVO> _DeploymentBudget = new List<PlannedDeploymentVO>();
                 _DeploymentBudget = deploymentdata.DeploymentBudget;

                 List<DeploymentBudget> _DeploymentBudgetList = new List<DeploymentBudget>();
                 foreach (var deploymentbudget in _DeploymentBudget)
                     {
                         DeploymentBudget obj = new DeploymentBudget();
                         //obj.DeploymentBudgetID = 0;
                         obj.DeploymentPlanID = deploymentplan.DeploymentPlanID;
                         obj.DredgPlan = deploymentbudget.DredgPlan;
                         obj.Budget = deploymentbudget.Budget;
                         obj.DredgingType = deploymentbudget.SubCatCode;
                         obj.Apr = deploymentbudget.Apr;
                         obj.AprCraftID = deploymentbudget.AprCraftID;
                         obj.May = deploymentbudget.May;
                         obj.MayCraftID = deploymentbudget.MayCraftID;
                         obj.Jun = deploymentbudget.Jun;
                         obj.JunCraftID = deploymentbudget.JunCraftID;
                         obj.Jul = deploymentbudget.Jul;
                         obj.JulCraftID = deploymentbudget.JulCraftID;
                         obj.Aug = deploymentbudget.Aug;
                         obj.AugCraftID = deploymentbudget.AugCraftID;
                         obj.Sep = deploymentbudget.Sep;
                         obj.SepCraftID = deploymentbudget.SepCraftID;
                         obj.Oct = deploymentbudget.Oct;
                         obj.OctCraftID = deploymentbudget.OctCraftID;
                         obj.Nov = deploymentbudget.Nov;
                         obj.NovCraftID = deploymentbudget.NovCraftID;
                         obj.Dec = deploymentbudget.Dec;
                         obj.DecCraftID = deploymentbudget.DecCraftID;
                         obj.Jan = deploymentbudget.Jan;
                         obj.JanCraftID = deploymentbudget.JanCraftID;
                         obj.Feb = deploymentbudget.Feb;
                         obj.FebCraftID = deploymentbudget.FebCraftID;
                         obj.Mar = deploymentbudget.Mar;
                         obj.MarCraftID = deploymentbudget.MarCraftID;

                         obj.RecordStatus = deploymentdata.RecordStatus;
                         obj.CreatedBy = deploymentdata.CreatedBy;
                         obj.CreatedDate = deploymentdata.CreatedDate;
                         obj.ModifiedBy = _UserId;
                         obj.ModifiedDate = DateTime.Now;

                         _DeploymentBudgetList.Add(obj);
                     }
                     _unitOfWork.Repository<DeploymentBudget>().InsertRange(_DeploymentBudgetList);
                     _unitOfWork.SaveChanges();              

                 return deploymentdata;
             });
               
            }


    }
}
