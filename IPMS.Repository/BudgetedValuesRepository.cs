using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Repository
{
    public class BudgetedValuesRepository : IBudgetedValuesRepository
    {
        private IUnitOfWork _unitOfWork;
        // private readonly ILog log;

        public BudgetedValuesRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //log = 
            LogManager.GetLogger(typeof(BudgetedValuesRepository));
        }

        #region FinanceYearDetails
        /// <summary>
        /// Get BudgetedValues details including with FinancialYear details
        /// </summary>
        /// <returns></returns>
        public List<FinancialYearVO> FinanceYearDetails()
        {
            List<FinancialYearVO> result = new List<FinancialYearVO>();

            var fydData = (from re in _unitOfWork.Repository<FinancialYear>().Query().Include(t => t.BudgetedValues).Select()
                           where re.RecordStatus == "A"
                           orderby re.FinancialYearID descending
                           select new FinancialYearVO
                           {
                               FinancialYearID = re.FinancialYearID,
                               StartDate = re.StartDate,
                               EndDate = re.EndDate,
                               IsCurrentFinancialYear = re.IsCurrentFinancialYear,
                               FinancialYear = re.StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture) + " to " + re.EndDate.ToString("MMM yyyy", CultureInfo.InvariantCulture),
                               BudgetedValuesVO = re.BudgetedValues.ToList().MapToDto()
                           }).ToList();

            foreach (var item in fydData)
            {
                var bvdatacount = item.BudgetedValuesVO.Count();

                if (bvdatacount > 0)
                {
                    item.BudgetedValuesFYDescription = "Budget values have been defined.";

                    foreach (var items in item.BudgetedValuesVO)
                    {
                        var portname = _unitOfWork.Repository<Port>().Query().Select().Where(x => x.PortCode == items.PortCode).FirstOrDefault().PortName;
                        items.PortName = portname;
                    }
                }
                else
                {
                    item.BudgetedValuesFYDescription = "Budget values are not defined.";
                    var portdata = (from re in _unitOfWork.Repository<Port>().Query()
                                    .Select()
                                    orderby re.PortName ascending

                                    select new BudgetedValuesVO
                                    {
                                        //BudgetedValuesID = null,
                                        FinancialYearID = item.FinancialYearID,
                                        PortCode = re.PortCode,
                                        PortName = re.PortName,
                                        VolumesContainers = null,
                                        VolumesRBCT = null,
                                        VolumesDryBulk = null,
                                        VolumesBreakBulk = null,
                                        MovementsContainers = null,
                                        MovementsRBCT = null,
                                        MovementsDryBulk = null,
                                        MovementsBreakBulk = null,
                                        STATContainers = null,
                                        STATRBCT = null,
                                        STATDryBulk = null,
                                        STATBreakBulk = null,

                                        TotalArrivals = null,
                                        TotalGT = null,
                                        TotalBerthingDelays = null,
                                        TotalPilotDelays = null,
                                        TotalTugAvailability = null,
                                        TotalTugDelays = null,
                                        TotalTugUtilization = null,

                                        CreatedBy = null,
                                        CreatedDate = null,
                                        ModifiedBy = null,
                                        ModifiedDate = null,
                                        RecordStatus = "A"
                                    }).ToList();

                    item.BudgetedValuesVO = portdata;
                }
            }

            result = fydData.ToList();
            return result;
        }
        #endregion

        #region InsertOrUpdateBudgetedValues
        /// <summary>
        /// To Add Budgeted Values Data
        /// </summary>
        /// <param name="stdData"></param>
        /// <returns></returns>
        public FinancialYearVO InsertOrUpdateBudgetedValues(FinancialYearVO data, int UserId)
        {
            if (data != null)
            {
            List<BudgetedValues> budgetedValues = data.BudgetedValuesVO.MapToEntity();
            
                foreach (var item in budgetedValues)
                {
                    if (item.BudgetedValuesID > 0)
                    {
                        item.ModifiedBy = UserId;
                        item.ModifiedDate = DateTime.Now;
                        item.ObjectState = ObjectState.Modified;

                        _unitOfWork.Repository<BudgetedValues>().Update(item);
                        _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        item.FinancialYearID = item.FinancialYearID;
                        item.RecordStatus = "A";
                        item.CreatedBy = UserId;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedBy = UserId;
                        item.ModifiedDate = DateTime.Now;

                        _unitOfWork.Repository<BudgetedValues>().Insert(item);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            return data;
        }
        #endregion

        #region GetFinancialYears
        /// <summary>
        /// This method is used for fetches the FinancialYear Details
        /// </summary>
        /// <returns></returns>
        public List<FinancialYear> GetFinancialYears()
        {
            var financialYears = _unitOfWork.Repository<FinancialYear>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active).OrderBy(x => x.FinancialYearID);
            return financialYears.ToList();
        }
        #endregion

        #region GetPortDetails
        /// <summary>
        /// To Get Port Details
        /// </summary>
        /// <returns></returns>
        public List<Port> GetPortDetails()
        {
            var ports = _unitOfWork.Repository<Port>().Queryable().Where(x => x.RecordStatus == RecordStatus.Active).OrderBy(x => x.PortName).ToList();
            return ports;
        }
        #endregion
    }
}

