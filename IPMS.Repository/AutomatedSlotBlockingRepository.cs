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
    public class AutomatedSlotBlockingRepository : IAutomatedSlotBlockingRepository
    {
        private IUnitOfWork _unitOfWork;

        public AutomatedSlotBlockingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //XmlConfigurator.Configure();
            //LogManager.GetLogger(typeof(AutomatedSlotBlockingRepository));
        }

        #region GetAutomatedSlotBlockings
        /// <summary>
        /// This method is used to get all Blocking Slots
        /// </summary>
        /// <returns></returns>
        public List<AutomatedSlotBlockingVO> GetAutomatedSlotBlockings(string portCode)
        {
            var blockSlots = (from bs in _unitOfWork.Repository<AutomatedSlotBlocking>().Queryable().Where(a => a.PortCode == portCode)
                              orderby bs.AutomatedSlotBlockingId descending
                              select bs).ToList<AutomatedSlotBlocking>();

            return blockSlots.MapToDto(); 
        }
        #endregion


    }
}
