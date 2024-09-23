using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SubCategoryService : ServiceBase, ISubCategoryService
    {
        private string _supcatcode;
        private ISubCategoryRepository _subcategoryRepository;

        public SubCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public SubCategoryService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

        #region AddSubCategory
        /// <summary>
        /// To Add Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        public SubCategoryVO AddSubCategory(SubCategoryVO subCategoryData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SubCategory subcategory = new SubCategory();
                subcategory = SubCategoryMapExtenstion.MapToEntity(subCategoryData);
                subcategory.CreatedBy = _UserId;
                subcategory.CreatedDate = System.DateTime.Now;
                subcategory.SupCatCode = subCategoryData.SupCatCode;
                subcategory.ModifiedBy = _UserId;
                subcategory.ModifiedDate = System.DateTime.Now;
                subcategory.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<SubCategory>().Insert(subcategory);
                _unitOfWork.SaveChanges();
                return subCategoryData;
            });
        }
        #endregion

        #region ModifySubCategory
        /// <summary>
        ///  To Modify Sub Category Data
        /// </summary>
        /// <param name="subCategoryData"></param>
        /// <returns></returns>
        public SubCategoryVO ModifySubCategory(SubCategoryVO subCategoryData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SubCategory subcategory = new SubCategory();
                subcategory = SubCategoryMapExtenstion.MapToEntity(subCategoryData);
                subcategory.ModifiedBy = _UserId;
                subcategory.ModifiedDate = System.DateTime.Now;
                subcategory.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SubCategory>().Update(subcategory);
                _unitOfWork.SaveChanges();
                return subCategoryData;
            });
        }
        #endregion

        #region GetSubCategoryID
        /// <summary>
        /// To Get Sub Category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubCategoryVO GetSubCategoryId(string id)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                var subcategoryid = (from a in _unitOfWork.Repository<SubCategory>().Query().Select()
                                     where a.SubCatCode == id
                                     select a).FirstOrDefault<SubCategory>();
                SubCategoryVO subcategory = SubCategoryMapExtenstion.MapToDto(subcategoryid);

                return subcategory;
            });
        }
        #endregion

        #region DeleteSubCategory
        /// <summary>
        /// To Delete Sub Category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SubCategoryVO DeleteSubCategory(long id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var subcategoryObj = _unitOfWork.Repository<SubCategory>().Find(id);
                subcategoryObj.RecordStatus = "I";
                subcategoryObj.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<SubCategory>().Update(subcategoryObj);
                _unitOfWork.SaveChanges();

                SubCategoryVO subcategory = SubCategoryMapExtenstion.MapToDto(subcategoryObj);
                return subcategory;
            });

        }
        #endregion

        #region SubCategoryDetails
        /// <summary>
        /// To Get Sub Category Details by id
        /// </summary>
        /// <param name="supcatId"></param>
        /// <returns></returns>
        public List<SubCategoryVO> SubCategoryDetails(string supcatId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var SubCategoryDetails = from q in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                         join p in _unitOfWork.Repository<SuperCategory>().Queryable().AsEnumerable<SuperCategory>()
                                         on q.SupCatCode equals p.SupCatCode
                                         where q.SupCatCode == supcatId
                                         select new SubCategoryVO
                                         {
                                             SubCatCode = q.SubCatCode,
                                             SubCatName = q.SubCatName,
                                             SupCatCode = p.SupCatCode,
                                             SupCatName = p.SupCatName,
                                             RecordStatus = q.RecordStatus,
                                             CreatedBy = q.CreatedBy,
                                             CreatedDate = q.CreatedDate
                                         };

                return SubCategoryDetails.OrderBy(x => x.SubCatName).ToList();
            });
        }
        #endregion

        #region AllSubCategoryDetails
        /// <summary>
        /// To Get All Sub Category Details
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> AllSubCategoryDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var AllSubCategoryDetails = from q in _unitOfWork.Repository<SubCategory>().Queryable().AsEnumerable<SubCategory>()
                                            join p in _unitOfWork.Repository<SuperCategory>().Queryable().AsEnumerable<SuperCategory>()
                                            on q.SupCatCode equals p.SupCatCode
                                            select new SubCategoryVO
                                            {
                                                SubCatCode = q.SubCatCode,
                                                SubCatName = q.SubCatName,
                                                SupCatCode = p.SupCatCode,
                                                SupCatName = p.SupCatName,
                                                RecordStatus = q.RecordStatus,
                                                CreatedBy = q.CreatedBy,
                                                CreatedDate = q.CreatedDate
                                            };

                return AllSubCategoryDetails.OrderBy(x => x.CreatedDate).ToList();
            });
        }
        #endregion

        #region GetSubCategoryWithSuperCatogory
        /// <summary>
        /// To Get Sub Category based on Super Category 
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetSubCategoryWithSuperCatogory()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var SubCategory = (from b in _unitOfWork.Repository<SubCategory>().Query().Include(b => b.SupCatCode).Select()
                                   where b.SupCatCode == _supcatcode
                                   select b).ToList();
                return SubCategory.MapToDto();
            });
        }
        #endregion

        #region SuperCategoryDetails
        /// <summary>
        /// To Get Super Category Details
        /// </summary>
        /// <returns></returns>
        public List<SuperCategoryVO> SuperCategoryDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var SuperCategoryDetails = from q in _unitOfWork.Repository<SuperCategory>().Queryable().AsEnumerable<SuperCategory>()
                                           select new SuperCategoryVO
                                           {
                                               SupCatCode = q.SupCatCode,
                                               SupCatName = q.SupCatName,
                                               RecordStatus = q.RecordStatus,
                                               CreatedBy = q.CreatedBy,
                                               CreatedDate = q.CreatedDate
                                           };

                return SuperCategoryDetails.OrderBy(x => x.SupCatName).ToList();
            });
        }
        #endregion

        #region GetCountriesList
        /// <summary>
        ///  To Get Countries List
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetCountriesList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var CountriesList = _subcategoryRepository.Pilot_Nationality().MapToDto();
                return CountriesList;
            });
        }
        #endregion


        #region GetSub cat Name
        /// <summary>
        ///  To GetSub Cat Name
        /// </summary>
        /// <returns></returns>
        public string GetSubCatName(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                string subcatName = _subcategoryRepository.GetSubCatName(code);
                return subcatName;
            });
        }
        #endregion

    
    }
}
