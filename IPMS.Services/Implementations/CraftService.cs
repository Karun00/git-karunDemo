using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CraftService : ServiceBase, ICraftMasterService
    {
        private ICraftRepository _craftRepository;

        public CraftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _craftRepository = new CraftRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public CraftService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _craftRepository = new CraftRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        /// <summary>
        /// TO Get Craft Data
        /// </summary>
        /// <returns></returns>
        public List<CraftVO> GetCraftList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftRepository.GetCraftList(_PortCode);
            });
        }

        /// <summary>
        /// To Add Craft data
        /// </summary>
        /// <param name="craftData"></param>
        /// <returns></returns>
        public CraftVO AddCraft(CraftVO craftData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftRepository.AddCraft(craftData, _PortCode, _UserId);
            });
        }

        /// <summary>
        /// To Modify Craft
        /// </summary>
        /// <param name="craftData"></param>
        /// <returns></returns>
        public CraftVO ModifyCraft(CraftVO craftData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftRepository.ModifyCraft(craftData, _PortCode, _UserId);
            });
        }

        /// <summary>
        /// To Get Craft reference Data
        /// </summary>
        /// <returns></returns>
        public CraftReferenceVO GetCraftReferences()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _craftRepository.GetCraftReferences();
            });
        }
    }
}
