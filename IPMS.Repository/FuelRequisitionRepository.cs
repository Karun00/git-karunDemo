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
using IPMS.Domain;

namespace IPMS.Repository
{
    public class FuelRequisitionRepository : IFuelRequisitionRepository
    {
        private IUnitOfWork _unitOfWork;
      //  private readonly ILog log;

        public FuelRequisitionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
          //  log = LogManager.GetLogger(typeof(ArrivalNotificationRepository));
        }

        /// <summary>
        ///  To get Fuel Requisition Details
        /// </summary>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelRequisitionDetails(string portCode )
        {
           var fuelreceipts = new List<FuelRequisition>();
            //try
            //{
           fuelreceipts = (from p in _unitOfWork.Repository<FuelRequisition>().Queryable()
                            .Include(p => p.FuelReceipts)
                            .Include(p => p.FuelReceipts.Select(w => w.WorkflowInstance.SubCategory))
                            .Include(p => p.Craft)
                             .Include(p => p.Craft.SubCategory3)
                            .Include(p => p.SubCategory)//.DefaultIfEmpty()
                            .Include(p => p.SubCategory1)
                            .Include(p => p.SubCategory2)
                            .Include(p => p.WorkflowInstance)
                            .Include(p => p.WorkflowInstance.SubCategory)
                            .Include(p => p.User).DefaultIfEmpty()
                            where p.PortCode== portCode
                           select p).OrderByDescending(x => x.FuelRequisitionID).ToList<FuelRequisition>();
            return fuelreceipts.MapToDtoForFuelReceipt();
                ////}
                ////catch (Exception ex)
                ////{
                ////    log.Error("Exception " + ex.Message);
                ////}
                //return fuelreceipts.MapToDtoForFuelReceipt();
            //var fuels = (from p in _unitOfWork.Repository<FuelRequisition>().Query()
            //             .Include(p => p.FuelReceipts)
            //             .Include(p => p.Craft)
            //             .Include(p => p.Craft.SubCategory3)
            //             .Include(p => p.SubCategory)
            //             .Include(p => p.SubCategory1)
            //             .Include(p => p.WorkflowInstance.SubCategory)
            //             .Include(p => p.SubCategory2).Select()
            //             where p.RecordStatus == "A" && p.PortCode == portCode
            //             orderby p.FuelRequisitionID descending
            //             select new FuelRequisitionVO
            //             {
            //                 FuelRequisitionID = p.FuelRequisitionID,
            //                 PortCode = p.PortCode,
            //                 FuelRequistionNo = p.FuelRequistionNo,
            //                 CraftID = p.CraftID,
            //                 CraftCode = p.Craft.CraftCode,
            //                 CraftName = p.Craft.CraftName,
            //                 CraftType = p.Craft.SubCategory3.SubCatName,
            //                 IMONo = p.Craft.IMONo,
            //                 RequisitionDate = Convert.ToString(p.RequisitionDate),
            //                 OilTypeCode = p.OilTypeCode,
            //                 OilType = p.SubCategory1.SubCatName,
            //                 GradeCode = p.GradeCode,
            //                 // GradeName = p.SubCategory1.SubCatName,    
            //                 UOMCode = p.UOMCode,
            //                 // UOMName = p.SubCategory2.SubCatName,
            //                 Quantity = p.Quantity,
            //                 RequisitionStatus = p.WorkflowInstance.SubCategory.SubCatName,
            //                 RequiredDate = Convert.ToString(p.RequiredDate),
            //                 Remarks = p.Remarks,
            //                 WorkflowInstanceId = p.WorkflowInstanceId,
            //                 //FuelRequistionType=p.FuelRequistionType

            //             });
            //return fuels.ToList();

            //}
        }
    
       
        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetCraftNames(string portcode)
        {
            var craftnames = (from cf in _unitOfWork.Repository<Craft>().Queryable().Include(cf => cf.SubCategory3)
                              where cf.RecordStatus == RecordStatus.Active && cf.PortCode == portcode
                              select new FuelRequisitionVO
                                {
                                    CraftID = cf.CraftID,
                                    CraftCode = cf.CraftCode,
                                    CraftName = cf.CraftName,
                                    CraftType = cf.SubCategory3.SubCatName

                                }).OrderBy(x=>x.CraftName).ToList();

            return craftnames;
        }

        /// <summary>
        /// To Get Craft Details By CraftID
        /// </summary>
        /// <param name="CraftID"></param>
        /// <returns></returns>
        public FuelRequisitionVO GetCraftsByID(int CraftID)
        {
            var craftdetails = (from cf in _unitOfWork.Repository<Craft>().Query().Select()
                                join ct in _unitOfWork.Repository<SubCategory>().Query().Select() on cf.CraftType equals ct.SubCatCode
                                where cf.RecordStatus == "A" && cf.CraftID == CraftID
                                select new FuelRequisitionVO
                              {
                                  CraftID = cf.CraftID,
                                  CraftCode = cf.CraftCode,
                                  CraftName = cf.CraftName,
                                  CraftType = ct.SubCatName,
                                  IMONo = cf.IMONo,
                                  OwnersName = cf.OwnersName
                              }).FirstOrDefault();


            return craftdetails;
        }
       

        public FuelRequisition GetFuelRequisitionApproveid(string fuelrequisitionid)
        {
            var andata = (from t in _unitOfWork.Repository<FuelRequisition>().Query().Select()
                          where t.FuelRequisitionID == Convert.ToInt32(fuelrequisitionid, CultureInfo.InvariantCulture)
                          select t).FirstOrDefault<FuelRequisition>();
            return andata;
        }

        /// <summary>
        /// Get Fuel Requisition Details By FuelRequisition Id 
        /// </summary>
        /// <param name="FuelRequisitionID"></param>
        /// <returns></returns>
        public FuelRequisitionVO GetFuelRequisitionRequestDetailsByID(string fuelrequisitionid)
        {    

            var fuelreceipts = from p in _unitOfWork.Repository<FuelRequisition>().Query()
                                        .Include(p => p.Craft)
                                        .Include(p => p.SubCategory1).Select()
                               where p.FuelRequisitionID == Convert.ToInt32(fuelrequisitionid, CultureInfo.InvariantCulture)
                               select new FuelRequisitionVO
                               {
                                   RequisitionNo = p.FuelRequistionNo,
                                   CraftName = p.Craft.CraftName,
                                   OilType = p.SubCategory1.SubCatName,
                                   Quantity = p.Quantity,
                                   RequiredDate = Convert.ToString(p.RequiredDate, CultureInfo.InvariantCulture),
                                   CreatedBy = p.CreatedBy,
                                   CreatedDate = p.CreatedDate,
                                   ModifiedBy = p.ModifiedBy,
                                   ModifiedDate = p.ModifiedDate
                               };


            return fuelreceipts.FirstOrDefault();
        }

        /// <summary>
        ///  To get Fuel Requisition based on fuelrequisitionid
        /// </summary>
        /// <param name="fuelrequisitionid"></param>
        /// <returns></returns>
        public List<FuelRequisitionVO> GetFuelRequisition(int fuelrequisitionid)
        {
            var fuelrequisitions = (from p in _unitOfWork.Repository<FuelRequisition>().Query()
                                    .Include(p => p.Craft)
                                    .Include(p => p.Craft.SubCategory3)
                                    .Include(p => p.SubCategory)
                                    .Include(p => p.SubCategory1)
                                    .Include(p => p.SubCategory2)
                                    .Include(p => p.WorkflowInstance)
                                    .Include(p => p.User).Select()
                                    where p.FuelRequisitionID == fuelrequisitionid
                                    select new FuelRequisitionVO
                                    {
                                        FuelRequisitionID = p.FuelRequisitionID,
                                        PortCode = p.PortCode,
                                        FuelRequistionNo = p.FuelRequistionNo,
                                        CraftID = p.CraftID,
                                        CraftCode = p.Craft.CraftCode,
                                        CraftName = p.Craft.CraftName,
                                        CraftType = p.Craft.SubCategory3.SubCatName,
                                        IMONo = p.Craft.IMONo,
                                        RequisitionDate = Convert.ToString(p.RequisitionDate, CultureInfo.InvariantCulture),
                                        OilTypeCode = p.OilTypeCode,
                                        OilType = p.SubCategory1.SubCatName,
                                        GradeCode = p.GradeCode,
                                        UOMCode = p.UOMCode,
                                        Quantity = p.Quantity,
                                        RequiredDate = Convert.ToString(p.RequiredDate, CultureInfo.InvariantCulture),
                                        Remarks = p.Remarks,
                                        OwnersName = p.User != null ? p.User.FirstName : "",
                                        WorkflowInstanceId = p.WorkflowInstanceId,
                                        FuelRequistionType=p.FuelRequistionType
                                    });

            return fuelrequisitions.ToList();
        }


        public string GetUserNameByUserId(int userid)
        {
            string Username = "";
            
              var user = (from u in _unitOfWork.Repository<User>().Queryable()
                          where u.UserID == userid
                            select u).FirstOrDefault<User>();

              Username = user.FirstName;

              return Username;
        }
    }
}
                     
           



