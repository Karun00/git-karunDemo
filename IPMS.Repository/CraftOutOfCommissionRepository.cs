using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using IPMS.Domain.DTOS;
using log4net;
using log4net.Config;
using System.Globalization;
using System.Globalization;



namespace IPMS.Repository
{
    public class CraftOutOfCommissionRepository : ICraftOutOfCommissionRepository
    {
        private IUnitOfWork _unitOfWork;
        // private readonly ILog log;

        public CraftOutOfCommissionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //  log =
            LogManager.GetLogger(typeof(ServiceTypeRepository));
        }

        ///// <summary>
        ///// To Add Craft Out of Commissions Data
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="_UserId"></param>
        ///// <returns></returns>
        //public CraftOutOfCommissionVO AddCraftOutOfComm(CraftOutOfCommissionVO entity, int _UserId)
        //{
        //    entity.CreatedBy = _UserId;
        //    entity.CreatedDate = DateTime.Now;
        //    entity.ModifiedBy = _UserId;
        //    entity.ModifiedDate = DateTime.Now;
        //    entity.OutOfCommissionDate = DateTime.Now;
        //    CraftOutOfCommission CraftOutOfCommissions = new CraftOutOfCommission();
        //    CraftOutOfCommissions = CraftOutOfCommissionMapExtension.MapToEntity(entity);
        //    _unitOfWork.Repository<CraftOutOfCommission>().Insert(CraftOutOfCommissions);
        //    var recordst = "I";
        //    var commstat = "OC";

        //    _unitOfWork.ExecuteSqlCommand("update Craft  set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftOutOfCommissions.CraftID);
        //    _unitOfWork.SaveChanges();
        //    return entity;
        //}

        ///// <summary>
        ///// To Modify Craft Out of Commissions Data
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="_UserId"></param>
        ///// <returns></returns>
        //public CraftOutOfCommissionVO ModifyCraftOutOfComm(CraftOutOfCommissionVO entity, int _UserId)
        //{
        //    entity.ModifiedBy = _UserId;
        //    entity.ModifiedDate = DateTime.Now;
        //    CraftOutOfCommission CraftOutOfCommissions = new CraftOutOfCommission();
        //    CraftOutOfCommissions = CraftOutOfCommissionMapExtension.MapToEntity(entity);
        //    CraftOutOfCommissions.ObjectState = ObjectState.Modified;
        //    _unitOfWork.Repository<CraftOutOfCommission>().Update(CraftOutOfCommissions);
        //    var recordst = CraftOutOfCommissions.RecordStatus;
        //    var commstat = CraftOutOfCommissions.CraftCommissionStatus;

        //    _unitOfWork.ExecuteSqlCommand("update Craft set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftOutOfCommissions.CraftID);
        //    _unitOfWork.SaveChanges();
        //    return entity;
        //}

        ///// <summary>
        ///// To Modify Craft Back to Commissions Data
        ///// </summary>
        ///// <param name="craftoutofcommdata"></param>
        ///// <param name="_UserId"></param>
        ///// <returns></returns>
        //public CraftOutOfCommissionVO ModifyCraftInComm(CraftOutOfCommissionVO craftincommdata, int _UserId)
        //{
        //    craftincommdata.ModifiedBy = _UserId;
        //    craftincommdata.ModifiedDate = DateTime.Now;
        //    craftincommdata.BackToCommissionDate = DateTime.Now;
        //    CraftOutOfCommission CraftInCommissions = new CraftOutOfCommission();
        //    CraftInCommissions = CraftOutOfCommissionMapExtension.MapToEntity(craftincommdata);
        //    CraftInCommissions.RecordStatus = "I";
        //    _unitOfWork.Repository<CraftOutOfCommission>().Update(CraftInCommissions);
        //    var recordst = "A";
        //    var commstat = "IC";

        //    _unitOfWork.ExecuteSqlCommand("update Craft set RecordStatus = @p0, CraftCommissionStatus = @p1 where CraftID = @p2", recordst, commstat, CraftInCommissions.CraftID);
        //    _unitOfWork.SaveChanges();
        //    return craftincommdata;
        //}

        /// <summary>
        /// To Get Craft Out of Commissions Details
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<CraftOutOfCommissionVO> CraftOutOfCommissionDetails(string portCode)
        {
            List<CraftOutOfCommissionVO> _CraftListList = new List<CraftOutOfCommissionVO>();
            _CraftListList = (from cc in _unitOfWork.Repository<CraftOutOfCommission>().Query().Select().AsEnumerable<CraftOutOfCommission>()
                              join c in _unitOfWork.Repository<Craft>().Query().Select().AsEnumerable<Craft>() on cc.CraftID equals c.CraftID
                              join sb in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>() on cc.Reason equals sb.SubCatCode
                              join csb in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>() on c.CraftType equals csb.SubCatCode
                              where cc.RecordStatus == "A" && c.PortCode == portCode
                              orderby cc.CraftOutOfCommissionID descending
                              select new CraftOutOfCommissionVO
                              {
                                  CraftOutOfCommissionID = cc.CraftOutOfCommissionID,
                                  CraftID = cc.CraftID,
                                  CraftCommissionStatus = cc.CraftCommissionStatus,
                                  ReasonName = sb.SubCatName,
                                  Reason = cc.Reason,
                                  Remarks = cc.Remarks,
                                  ExpectedDuration = cc.ExpectedDuration,
                                  RecordStatus = cc.RecordStatus,
                                  CraftCode = c.CraftCode,
                                  CraftName = c.CraftName,
                                  CraftType = c.CraftType,
                                  CraftTypeName = csb.SubCatName,
                                  IMONo = c.IMONo,
                                  CreatedBy = cc.CreatedBy,
                                  CreatedDate = cc.CreatedDate,
                                  ModifiedBy = cc.ModifiedBy,
                                  ModifiedDate = cc.ModifiedDate,
                                  OutOfCommissionDate = cc.OutOfCommissionDate,
                                  BackToCommissionDate = cc.BackToCommissionDate

                              }).ToList();
            return _CraftListList.ToList();
        }

        /// <summary>
        /// To Get Craft Back to Commissions Details
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<CraftOutOfCommissionVO> CraftInCommissionDetails(string portCode)
        {
            List<CraftOutOfCommissionVO> _CraftInCommList = new List<CraftOutOfCommissionVO>();

            _CraftInCommList = (from cc in _unitOfWork.Repository<CraftOutOfCommission>().Query().Select().AsEnumerable<CraftOutOfCommission>()
                                join c in _unitOfWork.Repository<Craft>().Query().Select().AsEnumerable<Craft>() on cc.CraftID equals c.CraftID
                                join sb in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>() on cc.Reason equals sb.SubCatCode
                                join csb in _unitOfWork.Repository<SubCategory>().Query().Select().AsEnumerable<SubCategory>() on c.CraftType equals csb.SubCatCode
                                where cc.RecordStatus == "A" && cc.CraftCommissionStatus == "OC" && c.PortCode == portCode
                                select new CraftOutOfCommissionVO
                                {
                                    CraftOutOfCommissionID = cc.CraftOutOfCommissionID,
                                    CraftID = cc.CraftID,
                                    CraftCommissionStatus = cc.CraftCommissionStatus,
                                    ReasonName = sb.SubCatName,
                                    Reason = cc.Reason,
                                    Remarks = cc.Remarks,
                                    ExpectedDuration = cc.ExpectedDuration,
                                    CraftCode = c.CraftCode,
                                    CraftName = c.CraftName,
                                    CraftTypeName = csb.SubCatName,
                                    CraftType = c.CraftType,
                                    IMONo = c.IMONo,
                                    CreatedBy = cc.CreatedBy,
                                    CreatedDate = cc.CreatedDate,
                                    ModifiedBy = cc.ModifiedBy,
                                    ModifiedDate = cc.ModifiedDate,
                                    OutOfCommissionDate = cc.OutOfCommissionDate,
                                    BackToCommissionDate = cc.BackToCommissionDate,
                                    RecordStatus = cc.RecordStatus
                                }).ToList();
            return _CraftInCommList.ToList();
        }

        /// <summary>
        /// To Get Craft Details
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<CraftVO> CraftsDetails(string portCode)
        {
            List<CraftVO> _CraftListList = new List<CraftVO>();
            _CraftListList = (from c in _unitOfWork.Repository<Craft>().Query().Select().AsEnumerable<Craft>()
                              where c.CraftCommissionStatus == "IC" && c.PortCode == portCode
                              orderby c.CraftName
                              select new CraftVO
                              {
                                  CraftID = c.CraftID,
                                  CraftName = c.CraftName,
                              }).ToList();
            return _CraftListList.ToList();
        }

        /// <summary>
        /// To Get Craft Details based on CraftID
        /// </summary>
        /// <param name="craftId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<CraftVO> CraftsDetailsWithCraftId(int craftId, string portCode)
        {
            List<CraftVO> _CraftListList = new List<CraftVO>();
            if (craftId > 0)
            {
                _CraftListList = (from c in _unitOfWork.Repository<Craft>().Queryable().AsEnumerable<Craft>()
                                  where c.CraftID == craftId && c.PortCode == portCode
                                  select new CraftVO
                                  {
                                      CraftID = c.CraftID,
                                      CraftCode = c.CraftCode,
                                      CraftName = c.CraftName,
                                      CraftType = c.CraftType,
                                      CraftCommissionStatus = c.CraftCommissionStatus,
                                      IMONo = c.IMONo,
                                  }).OrderBy(x => x.CraftName).ToList();
            }
            else
            {
                _CraftListList = (from c in _unitOfWork.Repository<Craft>().Queryable().AsEnumerable<Craft>()
                                  where c.RecordStatus == "A" && c.CraftCommissionStatus == "IC" && c.PortCode == portCode
                                  select new CraftVO
                                  {
                                      CraftID = c.CraftID,
                                      CraftCode = c.CraftCode,
                                      CraftName = c.CraftName,
                                      CraftType = c.CraftType,
                                      CraftCommissionStatus = c.CraftCommissionStatus,
                                      IMONo = c.IMONo,
                                  }).OrderBy(x => x.CraftName).ToList();
            }
            return _CraftListList.ToList();
        }

        /// <summary>
        /// To Get Reason for Out of Commission Details
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <returns></returns>
        public List<SubCategoryVO> ReasonForOutOfCommissionDetails(string reasonCode)
        {
            List<SubCategoryVO> _ReasonforOutofCommList = new List<SubCategoryVO>();

            if (reasonCode == "undefined" || string.IsNullOrWhiteSpace(reasonCode))
            {
                _ReasonforOutofCommList = (from c in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                           where (c.RecordStatus == "A") && (c.SupCatCode == "RFOC")
                                           select new SubCategoryVO
                                           {
                                               SubCatCode = c.SubCatCode,
                                               SubCatName = c.SubCatName,
                                           }).OrderBy(x => x.SubCatName).ToList();
            }
            else
            {
                _ReasonforOutofCommList = (from c in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                           where (c.RecordStatus == "A") && (c.SubCatCode == reasonCode)
                                           select new SubCategoryVO
                                           {
                                               SubCatCode = c.SubCatCode,
                                               SubCatName = c.SubCatName,
                                           }).OrderBy(x => x.SubCatName).ToList();
            }
            return _ReasonforOutofCommList.ToList();
        }

        /// <summary>
        /// To Get Craft Commission Status Details
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<SubCategoryVO> CommissionStatusDetails(string status)
        {
            List<SubCategoryVO> _CommStatusList = new List<SubCategoryVO>();

            if (status == "undefined" || string.IsNullOrWhiteSpace(status))
            {
                _CommStatusList = (from c in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                   where (c.RecordStatus == "A") && (c.SupCatCode == "COMM")
                                   select new SubCategoryVO
                                   {
                                       SubCatCode = c.SubCatCode,
                                       SubCatName = c.SubCatName,
                                   }).OrderBy(x => x.SubCatName).ToList();
            }
            else
            {
                _CommStatusList = (from c in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                   where (c.RecordStatus == "A") && (c.SubCatCode == status)
                                   select new SubCategoryVO
                                   {
                                       SubCatCode = c.SubCatCode,
                                       SubCatName = c.SubCatName,
                                   }).OrderBy(x => x.SubCatName).ToList();
            }
            return _CommStatusList.ToList();
        }


        public Entity GetEntities(string entityCode)
        {
            var entity = _unitOfWork.Repository<Entity>().Queryable().Where(x => x.EntityCode == entityCode).FirstOrDefault<Entity>();
            return entity;
        }


        public CompanyVO GetUserDetails(int userId)
        {
            var users = (from u in _unitOfWork.Repository<User>().Query().Select()
                         where u.UserID == userId
                         select new CompanyVO
                         {
                             UserType = u.UserType,
                             UserTypeId = u.UserTypeID
                         }).FirstOrDefault();
            return users;
        }

        public CraftOutOfCommissionVO GetCraftOutCommissionDetailsById(string craftOutOfCommissionId)
        {
            var craftout = (from co in _unitOfWork.Repository<CraftOutOfCommission>().Query().Select()
                            join c in _unitOfWork.Repository<Craft>().Query().Select() on co.CraftID equals c.CraftID
                            join sb in _unitOfWork.Repository<SubCategory>().Query().Select() on co.Reason equals sb.SubCatCode
                            join sbs in _unitOfWork.Repository<SubCategory>().Query().Select() on co.CraftCommissionStatus equals sbs.SubCatCode
                            join csb in _unitOfWork.Repository<SubCategory>().Query().Select() on c.CraftType equals csb.SubCatCode
                            where co.CraftOutOfCommissionID == Convert.ToInt32(craftOutOfCommissionId, CultureInfo.InvariantCulture)
                            select new CraftOutOfCommissionVO
                              {
                                  CraftOutOfCommissionID = co.CraftOutOfCommissionID,
                                  CraftName = c.CraftName,
                                  CraftCode = c.CraftCode,
                                  CraftType = csb.SubCatName,
                                  IMONo = c.IMONo,
                                  Reason = sb.SubCatName,
                                  CraftCommissionStatus = sbs.SubCatName,
                                  ExpectedDuration = co.ExpectedDuration,
                                  Remarks = co.Remarks,
                                  CreatedBy = co.CreatedBy,
                                  ModifiedBy = co.ModifiedBy

                              }).FirstOrDefault<CraftOutOfCommissionVO>();

            return craftout;
        }
    }
}

     