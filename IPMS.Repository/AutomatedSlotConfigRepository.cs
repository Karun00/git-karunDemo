using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;

namespace IPMS.Repository
{
    public class AutomatedSlotConfigRepository:IAutomatedSlotConfigRepository
    {
        private IUnitOfWork _unitOfWork;

        public AutomatedSlotConfigRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public List<AutomatedSlotConfiguration> GetAutomatedSlotConfigurationDetails(string portId)
        {
            var automatedslotconfigurationlist = (from t in _unitOfWork.Repository<AutomatedSlotConfiguration>().Query().Include(t => t.SlotPriorityConfigurations).Select()
                                                  where t.PortCode == portId && t.RecordStatus == "A"
                                                  orderby t.EffectiveFrm descending
                                                    select new
                             {
                                 AutomatedSlotConfiguration = t,                               
                                 SlotPriorityConfigurations = t.SlotPriorityConfigurations.Where
                                   (
                                      p => p.RecordStatus == "A"
                                   )                               
                             }
                         );

                var AutomatedSlotConfigurations = automatedslotconfigurationlist.ToArray().Select(o => o.AutomatedSlotConfiguration).ToList<AutomatedSlotConfiguration>();
                                       
            return AutomatedSlotConfigurations;
        }



           //

        public List<SubCategoryCodeNameVO> PrioprtySeqList()
        {
            var PrioprtySeqLists = _unitOfWork.SqlQuery<SubCategoryCodeNameVO>("dbo.usp_GetProrityNo").ToList();
            return PrioprtySeqLists;
        }

   
        public List<SlotPriorityConfiguration> GetSlotPriorityDetails()
        {
            var slotpriorityconfigurationlist = _unitOfWork.Repository<SlotPriorityConfiguration>().Queryable().OrderByDescending(x => x.SlotCofiguratinid).ToList<SlotPriorityConfiguration>();
            return slotpriorityconfigurationlist;
        }
    }
}
