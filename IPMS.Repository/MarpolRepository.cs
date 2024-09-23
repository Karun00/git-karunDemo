using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace IPMS.Repository
{
    public class MarpolRepository : IMarpolRepository
    {
        private IUnitOfWork _unitOfWork;

        public MarpolRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        #region GetMarpolDetails
        /// <summary>
        /// This method is used to get all Marpol Details
        /// </summary>
        /// <returns></returns>
        public List<MarpolVO> GetMarpolDetails()
        {
            var marpols = (from mp in _unitOfWork.Repository<Marpol>().Queryable()
                           join sb in _unitOfWork.Repository<SubCategory>().Queryable() on mp.MarpolCode equals sb.SubCatCode
                           orderby mp.CreatedDate descending
                           select new MarpolVO
                           {
                               ClassCode = mp.ClassCode,
                               ClassName = mp.ClassName,
                               MarpolCode = mp.MarpolCode,
                               MarpolName = sb.SubCatName,
                               RecordStatus = mp.RecordStatus,
                               CreatedBy = mp.CreatedBy,
                               CreatedDate = mp.CreatedDate,
                               ModifiedBy = mp.ModifiedBy,
                               ModifiedDate = mp.ModifiedDate
                           }).ToList<MarpolVO>();

            return marpols;
        }
        #endregion


    }
}
