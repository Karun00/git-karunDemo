using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using IPMS.Domain.DTOS;

namespace IPMS.Repository
{
    public class CraftRepository : ICraftRepository
    {
        private IUnitOfWork _unitOfWork;
        private ISubCategoryRepository _subcategoryRepository;

        public CraftRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
        }

        IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }

        /// <summary>
        /// TO Get Craft Data
        /// </summary>
        /// <returns></returns>
        public List<CraftVO> GetCraftList(string portCode)
        {
            var craftList = (from t in _unitOfWork.Repository<Craft>().Queryable()
                             .Include(t => t.CraftReminderConfigs)
                             .Include(t => t.CraftReminderConfigs.Select(p => p.SubCategory4))
                             .Include(t => t.SubCategory3)
                             .Include(t => t.SubCategory1)
                             .Include(t => t.SubCategory5)
                             where t.PortCode == portCode
                             orderby t.CraftID descending
                             select t).ToList<Craft>();

            return craftList.ListMapToDtoList();
        }

        /// <summary>
        /// To Add Craft data
        /// </summary>
        /// <param name="craftData"></param>
        /// <returns></returns>
        public CraftVO AddCraft(CraftVO craftData, string portCode, int userId)
        {
            if (craftData != null)
            {
                if (!string.IsNullOrWhiteSpace(craftData.CraftName))
                {
                    craftData.CraftName = new string(CharsToTitleCase(craftData.CraftName).ToArray());
            }
            }
            Craft craft = null;
            craft = craftData.MapToEntity();

            craft.ObjectState = ObjectState.Added;
            craft.CreatedBy = userId;
            craft.CreatedDate = DateTime.Now;
            craft.ModifiedBy = userId;
            craft.ModifiedDate = DateTime.Now;
            craft.PortCode = portCode;
            craft.RecordStatus = "A";
            _unitOfWork.Repository<Craft>().Insert(craft);
            _unitOfWork.SaveChanges();
            craftData = craft.MapToDto();
            return craftData;
        }

        /// <summary>
        /// To Modify Craft
        /// </summary>
        /// <param name="craftData"></param>
        /// <returns></returns>
        public CraftVO ModifyCraft(CraftVO craftData, string portCode, int userId)
        {
            if (craftData != null)
        {
                if (!string.IsNullOrWhiteSpace(craftData.CraftName))
            {
                    craftData.CraftName = new string(CharsToTitleCase(craftData.CraftName).ToArray());
                }
            }

            Craft craft = null;
            craft = craftData.MapToEntity();

            craft.ObjectState = ObjectState.Modified;
            craft.ModifiedBy = userId;
            craft.ModifiedDate = DateTime.Now;
            craft.PortCode = portCode;
            _unitOfWork.Repository<Craft>().Update(craft);
            _unitOfWork.SaveChanges();
            craftData = craft.MapToDto();
            return craftData;
        }

        /// <summary>
        /// To Get Craft reference Data
        /// </summary>
        /// <returns></returns>
        public CraftReferenceVO GetCraftReferences()
        {
            CraftReferenceVO VO = new CraftReferenceVO();
            VO.PortOfRegistry = _subcategoryRepository.PortOfRegistry().MapToDto();
            VO.CraftType = _subcategoryRepository.CraftType().MapToDto();
            VO.CraftCommissionStatus = _subcategoryRepository.CraftCommissionStatus().MapToDto();
            VO.PropulsionType = _subcategoryRepository.PropulsionType().MapToDto();
            VO.ClassificationSociety = _subcategoryRepository.ClassificationSociety().MapToDto();
            VO.CraftNationality = _subcategoryRepository.CraftNationality().MapToDto();
            VO.FuelType = _subcategoryRepository.FuelType().MapToDto();
            VO.EngineType = _subcategoryRepository.EngineType().MapToDto();
            return VO;
        }
    }
}
