using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using System.Globalization;

namespace IPMS.Repository
{
    public class DeploymentPlanRepository : IDeploymentPlanRepository
    {
        private IUnitOfWork _unitOfWork;
      //  private readonly ILog log;

        public DeploymentPlanRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
          //  log = 
            LogManager.GetLogger(typeof(DeploymentPlanRepository));
        }

        /// <summary>
        /// To Get Deployment Plan Details
        /// </summary>
        /// <returns></returns>
        public List<DeploymentPlanVO> DeploymentPlanDetails(string portCode)
        {
            var deployplans = new List<DeploymentPlan>();
            //try
            //{
                deployplans = (from p in _unitOfWork.Repository<DeploymentPlan>().Query()
                                .Include(p => p.DeploymentBudgets)
                                //chandrima
                                .Include(p => p.FinancialYear)
                                .Include(p => p.Port)
                                //.Include(p => p.DockingPlanDocuments.Select(d => d.Document))                                
                                .Select()
                              // where p.PortCode == portcode
                                orderby p.DeploymentPlanID descending
                               select p).ToList<DeploymentPlan>();



                // return dockingplans.MapToDTOForDocking();
            //}
            //catch (Exception ex)
            //{
                //log.Error("Exception " + ex.Message);
            //}
            
            return deployplans.MapToDTOForDeploy();

         


        }

        /// <summary>
        /// To Get Planned Deployment Details
        /// </summary>
        /// <returns></returns>
        public List<PlannedDeploymentVO> PlannedDeploymentDetails(string portCode)
        {
            var planneddeployments = (from dp in _unitOfWork.Repository<DeploymentBudget>().Query()
                                      .Include(dp => dp.SubCategory)
                                      .Include(dp => dp.DeploymentPlan)
                                      .Include(dp => dp.DeploymentPlan.Port)
                                      .Select()
                               where dp.DeploymentPlan.PortCode == portCode
                               select new PlannedDeploymentVO
                               {
                                   DeploymentPlanID = dp.DeploymentPlanID,
                                   PortCode = dp.DeploymentPlan.Port.PortName,
                                   Budget = dp.Budget,
                                   DredgPlan = dp.DredgPlan,
                                   SubCatCode = dp.SubCategory.SubCatName,
                                   Jan = dp.Jan,
                                   Feb = dp.Feb,
                                   Mar = dp.Mar,
                                   Apr = dp.Apr,
                                   May = dp.May,
                                   Jun = dp.Jun,
                                   Jul = dp.Jul,
                                   Aug = dp.Aug,
                                   Sep = dp.Sep,
                                   Oct = dp.Oct,
                                   Nov = dp.Nov,
                                   Dec = dp.Dec,
                                   JanCraftID = dp.JanCraftID,
                                   FebCraftID = dp.FebCraftID,
                                   MarCraftID = dp.MarCraftID,
                                   AprCraftID = dp.AprCraftID,
                                   MayCraftID = dp.MayCraftID,
                                   JunCraftID = dp.JunCraftID,
                                   JulCraftID = dp.JulCraftID,
                                   AugCraftID = dp.AugCraftID,
                                   SepCraftID = dp.SepCraftID,
                                   OctCraftID = dp.OctCraftID,
                                   NovCraftID = dp.NovCraftID,
                                   DecCraftID = dp.DecCraftID
                                 
                               }).ToList();

            return planneddeployments;

        }

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        public List<PlannedDeploymentVO> GetDeploymentCraftNames()
        {
            var craftnames = (from cf in _unitOfWork.Repository<Craft>().Query().Include(cf => cf.SubCategory3).Select()
                              where cf.CraftType == "CRTD" && cf.RecordStatus == "A"
                              select new PlannedDeploymentVO
                              {
                                  CraftID = cf.CraftID,                                
                                  CraftName = cf.CraftName,
                                  DredgerColorCode = cf.DredgerColorCode
                              }).ToList();

            return craftnames;
        }

        /// <summary>
        /// To Get Financial Year Data
        /// </summary>
        /// <returns></returns>
        public List<FinancialYearVO> GetFinancialYears()
        {
           
          var  FinancialYears = (from fy in _unitOfWork.Repository<FinancialYear>().Query().Select()
                              select new FinancialYearVO
                              {
                                  FinancialYearID = fy.FinancialYearID,
                                  StartDate=fy.StartDate ,
                                  EndDate=fy.EndDate,
                                  FinancialYear = fy.StartDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + fy.StartDate.ToString("yyyy", CultureInfo.InvariantCulture) + " - " + fy.EndDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + fy.EndDate.ToString("yyyy", CultureInfo.InvariantCulture)
                              }).ToList();

            return FinancialYears.ToList();
        }

        /// <summary>
        ///  To Get PortCode
        /// </summary>
        /// <returns></returns>
        public List<PortVO> GetPortTypes()
        {
            List<PortVO> _Portcode = new List<PortVO>();
            _Portcode = (from c in _unitOfWork.Repository<Port>().Query()
                                  .Select()
                              select new PortVO
                              {
                                  PortCode = c.PortCode,
                                  PortName = c.PortName,
                              }).ToList();
            return _Portcode.ToList();
        }

        /// <summary>
        ///  To Get Craft Color
        /// </summary>
        /// <returns></returns>
        public List<PortGeneralConfigsVO> GetCraftColors(string portCode)
        {
            List<PortGeneralConfigsVO> _craftcolor = new List<PortGeneralConfigsVO>();
            _craftcolor = (from c in _unitOfWork.Repository<PortGeneralConfig>().Query()
                                  .Select()
                           where c.ConfigName=="DredgingColors" && c.PortCode==portCode
                         select new PortGeneralConfigsVO
                         {
                            ConfigValue=c.ConfigValue
                         }).ToList();
            return _craftcolor.ToList();
        }
       
    }
}
