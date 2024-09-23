using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Repository
{
    public class CraftReminderConfigRepository : ICraftReminderConfigRepository
    {
        private IUnitOfWork _unitOfWork;
        private ISubCategoryRepository _subcategoryRepository;
       // private readonly ILog log;

        public CraftReminderConfigRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            XmlConfigurator.Configure();
           // log = 
            LogManager.GetLogger(typeof(ServiceTypeRepository));
        }

        /// <summary>
        /// To Get CraftReminderConfig Details
        /// </summary>
        /// <param name="craftId"></param>
        /// <returns></returns>
        public List<CraftReminderConfig> GetCraftReminderConfigDetails(int craftId)
        {
            var CraftConfigDetails = (from crc in _unitOfWork.Repository<CraftReminderConfig>().Query()
                                          .Select()
                                      where crc.CraftID == craftId
                                      select crc).ToList();

            return CraftConfigDetails;
        }

        /// <summary>
        /// GetCraftReminderConfig By ConfigID
        /// </summary>
        /// <param name="craftReminderConfigId"></param>
        /// <returns></returns>
        public CraftReminderConfig GetCraftReminderConfigByConfigId(int craftReminderConfigId)
        {
            var CraftConfigDetails = (from crc in _unitOfWork.Repository<CraftReminderConfig>().Query().Include(t => t.Craft).Select()
                                      where crc.CraftReminderConfigID == craftReminderConfigId
                                      select crc).FirstOrDefault();

            return CraftConfigDetails;
        }


        public List<CraftVO> GetCraftReminderConfigById(int craftReminderConfigId, string portCode)
        {
           IEnumerable<CraftReminderConfig> ConfigDetails = (from crc in _unitOfWork.Repository<CraftReminderConfig>().Query().Select()
                                      where crc.CraftReminderConfigID == craftReminderConfigId 
                                      select crc);

            int craftID = ConfigDetails.FirstOrDefault().CraftID;

            var craftList = (from t in _unitOfWork.Repository<Craft>().Query()
                             .Include(t => t.CraftReminderConfigs)
                             .Include(t => t.CraftReminderConfigs.Select(p => p.SubCategory4))
                             .Include(t => t.SubCategory3)
                             .Include(t => t.SubCategory1)
                             .Include(t => t.SubCategory5).Select()
                             where t.CraftID == craftID && t.PortCode == portCode
                             select t).ToList<Craft>();
            craftList.FirstOrDefault().CraftReminderConfigs = ConfigDetails.ToList();

            return craftList.ListMapToDtoList();
        }

        /// <summary>
        /// Get CraftReminderReferences
        /// </summary>
        /// <returns></returns>
        public CraftReferenceVO GetCraftReminderReferences()
        {
            CraftReferenceVO VO = new CraftReferenceVO();
            VO.ParticularTypes = _subcategoryRepository.ParticularsTypes().MapToDto();
            VO.CalenderTypes = _subcategoryRepository.CalenderTypes().MapToDto();
            VO.FuelType = _subcategoryRepository.FuelType().MapToDto();
            VO.CraftType = _subcategoryRepository.CraftType().MapToDto();

            return VO;
        }

        /// <summary>
        /// To Add CraftReminderConfig
        /// </summary>
        /// <param name="data"></param>
        /// <param name="portCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CraftVO AddCraftReminderConfig(CraftReminderConfigVO data, string portCode, int userId)
        {
            CraftReminderConfig craftObj = null;

            craftObj = data.MapToEntity();

            craftObj.ObjectState = ObjectState.Added;
            craftObj.CreatedDate = DateTime.Now;
            craftObj.CreatedBy = userId;
            craftObj.ModifiedBy = userId;
            craftObj.ModifiedDate = DateTime.Now;
            craftObj.RecordStatus = craftObj.RecordStatus;
            craftObj.ReminderStatus = null;
            craftObj.ExitReminderConfig = null;

            _unitOfWork.Repository<CraftReminderConfig>().Insert(craftObj);
            _unitOfWork.SaveChanges();

            var craftList = (from t in _unitOfWork.Repository<Craft>().Query()
                            .Include(t => t.CraftReminderConfigs)
                            .Include(t => t.CraftReminderConfigs.Select(m => m.SubCategory4))
                            .Include(t => t.SubCategory3)
                            .Include(t => t.SubCategory1)
                            .Include(t => t.SubCategory5).Select()
                             where t.PortCode == portCode && t.CraftID == craftObj.CraftID
                             select t).FirstOrDefault();

            return craftList.MapToDto();
        }

        /// <summary>
        /// To Modify CraftReminderConfig
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CraftReminderConfigVO ModifyCraftReminderConfig(CraftReminderConfigVO data, int userId)
        {
            CraftReminderConfig craftObj = null;

            craftObj = data.MapToEntity();

            craftObj.ObjectState = ObjectState.Modified;
            craftObj.CreatedDate = DateTime.Now;
            craftObj.CreatedBy = userId;
            craftObj.ModifiedBy = userId;
            craftObj.ModifiedDate = DateTime.Now;
            craftObj.RecordStatus = craftObj.RecordStatus;
            craftObj.ReminderStatus = null;
            craftObj.ExitReminderConfig = null;

            _unitOfWork.Repository<CraftReminderConfig>().Update(craftObj);
            _unitOfWork.SaveChanges();

            data = craftObj.MapToDTO();

            return data;
        }
    }
}
