using Core.Repository;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using IPMS.Domain.ValueObjects;
using log4net.Config;
using log4net;
using System.Data.Entity;
using System;
using System.Globalization;
namespace IPMS.Repository
{
    public class FuelConsumptionDailyLogRepository : IFuelConsumptionDailyLogRepository
    {
        protected IUnitOfWork _unitOfWork;
        // private readonly ILog log;

        public FuelConsumptionDailyLogRepository(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = 
            LogManager.GetLogger(typeof(FuelConsumptionDailyLogRepository));
        }
        public List<CraftVO> GetCraftDetails(string searchValue)
        {
            var crafts = (from a in _unitOfWork.Repository<Craft>().Query().Select()
                          join ct in _unitOfWork.Repository<SubCategory>().Query().Select() on a.CraftType equals ct.SubCatCode
                          join ft in _unitOfWork.Repository<SubCategory>().Query().Select() on a.FuelType equals ft.SubCatCode
                          orderby a.CraftName ascending
                          where a.RecordStatus == "A" && a.CraftName.ToUpperInvariant().Contains(searchValue.ToUpperInvariant())
                          select new CraftVO
                          {
                              CraftID = a.CraftID,
                              CraftCode = a.CraftCode,
                              CraftName  = a.CraftName,
                              IMONo = a.IMONo,
                              CraftTypeName = ct.SubCatName,
                              FuelTypeName = ft.SubCatName
                          }).ToList();
            return crafts;
        }

        public List<FuelConsumptionDailyLog> GetAllFuelConsumptionDailyLogDetails(string portCode)
        {
            var query = (from p in _unitOfWork.Repository<FuelConsumptionDailyLog>().Queryable().Include(p => p.Craft).Include(p => p.Craft.SubCategory3).Include(p=>p.Craft.SubCategory5)
                         where p.RecordStatus == "A" && p.PortCode == portCode
                         select p).OrderByDescending(x=>x.FuelConsumptionDailyLogID).ToList<FuelConsumptionDailyLog>();
            return query;
            
        }

        public List<FuelConsumptionDailyLog> GetFuelConsumptionDailyLoggridDetails(int craftId)
        {
            var crftdtls = (from p in _unitOfWork.Repository<FuelConsumptionDailyLog>().Query().Include(p=>p.Craft).Select()
                            where p.CraftID == craftId
                            orderby p.FuelConsumptionDailyLogID ascending
                            select p).ToList<FuelConsumptionDailyLog>();
            return crftdtls;

        }




    }
}
