using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Config;
using System.Globalization;

namespace IPMS.Repository
{
    public class FuelReceiptRepository : IFuelReceiptRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        public FuelReceiptRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
          //  log = LogManager.GetLogger(typeof(ArrivalNotificationRepository));
        }

        /// <summary>
        ///  To get Fuel Receipt Details
        /// </summary>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceiptDetails(string portCode)
        {
            var fuelreceipts = new List<FuelRequisition>();
            //try
            //{
                fuelreceipts = (from p in _unitOfWork.Repository<FuelRequisition>().Queryable()
                                .Include(p=> p.FuelReceipts)
                                .Include(p => p.FuelReceipts.Select(w=>w.WorkflowInstance.SubCategory))
                                .Include(p => p.FuelReceipts.Select(b => b.Berth))
                                .Include(p => p.Craft)
                                .Include(p => p.Craft.SubCategory3)
                                .Include(p => p.SubCategory)
                                .Include(p => p.SubCategory1)
                                .Include(p => p.SubCategory2)   
                                .Include(p => p.WorkflowInstance)
                                .Include(p => p.WorkflowInstance.SubCategory)
                                where p.PortCode == portCode && p.WorkflowInstance.WorkflowTaskCode == "WFSA"
                                select p).OrderByDescending(x=>x.FuelRequisitionID).ToList<FuelRequisition>();



                return fuelreceipts.MapToDtoForFuelReceipt();
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Exception " + ex.Message);
                //}
                return fuelreceipts.MapToDtoForFuelReceipt();

        }

        /// <summary>
        ///  To get Fuel Receipt based on fuelrequestionid
        /// </summary>
        /// <param name="fuelRequestionId"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceipt(int fuelRequestionId)
        {
            var fuelreceipts = new List<FuelRequisition>();
            //try
            //{
                fuelreceipts = (from p in _unitOfWork.Repository<FuelRequisition>().Query()
                       .Include(p => p.FuelReceipts)
                       .Include(p => p.FuelReceipts.Select(w => w.WorkflowInstance.SubCategory))
                       .Include(p => p.FuelReceipts.Select(b => b.Berth))
                       .Include(p => p.Craft)
                       .Include(p => p.Craft.SubCategory3)
                       .Include(p => p.SubCategory)
                       .Include(p => p.SubCategory1)
                       .Include(p => p.SubCategory2)
                       .Include(p => p.WorkflowInstance)
                       .Include(p => p.WorkflowInstance.SubCategory).Select()
                                where p.FuelRequisitionID == fuelRequestionId
                                select p).ToList<FuelRequisition>();

                return fuelreceipts.MapToDtoForFuelReceipt();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception " + ex.Message);
            //}
            return fuelreceipts.MapToDtoForFuelReceipt();
        }

        /// <summary>
        ///  To get Fuel Receipt based on fuelreceiptid
        /// </summary>
        /// <param name="fuelReceiptId"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceiptFuelId(int fuelReceiptId)
        {
            var fuelreceipts = new List<FuelRequisition>();
            //try
            //{
                // var FuelReceiptID = fuelrequestionid;

                var fuelrequestionid = _unitOfWork.SqlQuery<FuelReceipt>("select * from FuelReceipt where FuelReceiptID = @p0", fuelReceiptId).FirstOrDefault<FuelReceipt>();

                fuelreceipts = (from p in _unitOfWork.Repository<FuelRequisition>().Query()
                       .Include(p => p.FuelReceipts)
                       .Include(p => p.FuelReceipts.Select(w => w.WorkflowInstance.SubCategory))
                       .Include(p => p.FuelReceipts.Select(b => b.Berth))
                       .Include(p => p.Craft)
                       .Include(p => p.Craft.SubCategory3)
                       .Include(p => p.SubCategory)
                       .Include(p => p.SubCategory1)
                       .Include(p => p.SubCategory2)
                       .Include(p => p.WorkflowInstance)
                       .Include(p => p.WorkflowInstance.SubCategory).Select()
                                where p.FuelRequisitionID == fuelrequestionid.FuelRequisitionID
                                select p).ToList<FuelRequisition>();

                return fuelreceipts.MapToDtoForFuelReceipt();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception " + ex.Message);
            //}
            return fuelreceipts.MapToDtoForFuelReceipt();
        }


        /// <summary>
        ///  To get Fuel Receipt based on fuelrequestionid
        /// </summary>
        /// <param name="fuelRequestionId"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelReceiptDetailsById(string fuelRequestionId)
        {
            var fuelreceipts = new List<FuelRequisition>();
            //try
            //{
                fuelreceipts = (from p in _unitOfWork.Repository<FuelRequisition>().Queryable()
                       .Include(p => p.FuelReceipts)
                       .Include(p => p.FuelReceipts.Select(w => w.WorkflowInstance.SubCategory))
                       .Include(p => p.FuelReceipts.Select(b => b.Berth))
                       .Include(p => p.Craft)
                       .Include(p => p.Craft.SubCategory3)
                       .Include(p => p.SubCategory)
                       .Include(p => p.SubCategory1)
                       .Include(p => p.SubCategory2)
                       .Include(p => p.WorkflowInstance)
                       .Include(p => p.WorkflowInstance.SubCategory)
                                where p.FuelRequisitionID == Convert.ToInt32(fuelRequestionId, CultureInfo.InvariantCulture)
                                select p).ToList<FuelRequisition>();

                return fuelreceipts.MapToDtoForFuelReceipt();
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception " + ex.Message);
            //}
            return fuelreceipts.MapToDtoForFuelReceipt();
        }

        public FuelReceipt GetFuelReceiptApproveId(string fuelReceiptId)
        {
            var andata = (from t in _unitOfWork.Repository<FuelReceipt>().Query().Select()
                          where t.FuelReceiptID == Convert.ToInt32(fuelReceiptId, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault<FuelReceipt>();
            return andata;
        }

        public FuelRequisitionVO GetFuelReceiptDetailsRequestById(string fuelReceiptId)
        {

            var fuelreceipts = from p in _unitOfWork.Repository<FuelReceipt>().Query()
                               .Include(p => p.Berth)
                                .Include(p => p.FuelRequisition)                      
                                .Include(p => p.FuelRequisition.Craft)
                                .Include(p => p.FuelRequisition.SubCategory1).Select()  
                               where p.FuelReceiptID == Convert.ToInt32(fuelReceiptId, CultureInfo.InvariantCulture)

                               select new FuelRequisitionVO
                               {
                                   RequisitionNo = p.FuelRequisition.FuelRequistionNo,
                                   CraftName = p.FuelRequisition.Craft.CraftName,
                                   OilType = p.FuelRequisition.SubCategory1.SubCatName,
                                   RequiredQuantity = p.FuelRequisition.Quantity,
                                   ReceiptDate = Convert.ToString(p.ReceiptDate, CultureInfo.InvariantCulture),
                                   RequiredDate = Convert.ToString(p.FuelRequisition.RequiredDate, CultureInfo.InvariantCulture),
                                   BerthName = p.Berth.BerthName,
                                   CreatedBy = p.CreatedBy,
                                   CreatedDate = p.CreatedDate,
                                   ModifiedBy = p.ModifiedBy,
                                   ModifiedDate = p.ModifiedDate
                               };


            return fuelreceipts.FirstOrDefault();
        }




    }
}
