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
    public class SuperCategoryService : ServiceBase, ISuperCategoryService
    {
       

         public SuperCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
        }

         public SuperCategoryService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            // TODO: Complete member initialization
        }

        /// <summary>
        /// To Add Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
         public SuperCategoryVO AddSuperCategory(SuperCategoryVO supCatData)
         {
             return EncloseTransactionAndHandleException(() =>
             {
                 supCatData.CreatedBy = _UserId;
                 supCatData.CreatedDate = DateTime.Now;
                 supCatData.ModifiedBy = _UserId;
                 supCatData.ModifiedDate = DateTime.Now;                 
                 SuperCategory supcat = new SuperCategory();
                 supcat = SuperCategoryMapExtension.MapToEntity(supCatData);
                 supcat.ObjectState = ObjectState.Added;
                 _unitOfWork.Repository<SuperCategory>().Insert(supcat);
                 _unitOfWork.SaveChanges();
                 return supCatData;
             });
         }  

        /// <summary>
        /// To Modify Super Category Data
        /// </summary>
        /// <param name="supCatData"></param>
        /// <returns></returns>
         public SuperCategoryVO ModifySuperCategory(SuperCategoryVO supCatData)
         {
             return EncloseTransactionAndHandleException(() =>
             {                 
                 SuperCategory supcat = new SuperCategory();
                 supcat = SuperCategoryMapExtension.MapToEntity(supCatData);
                 supcat.ModifiedBy = _UserId;
                 supcat.ModifiedDate = DateTime.Now;
                 supcat.ObjectState = ObjectState.Modified;
                 _unitOfWork.Repository<SuperCategory>().Update(supcat);
                 _unitOfWork.SaveChanges();
                 return supCatData;
             });
         }

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

                 return SuperCategoryDetails.OrderByDescending(x => x.CreatedDate).ToList();

             });
         }
    }
}
