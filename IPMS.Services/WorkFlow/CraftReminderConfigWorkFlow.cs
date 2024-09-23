using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPMS.Repository;
using System.Web.Script.Serialization;
using System.ComponentModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain;
using System.Web.UI;
using IPMS.Core.Repository.Exceptions;
using System.Globalization;

namespace IPMS.Services.WorkFlow
{
    public class CraftReminderConfigWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private CraftReminderConfig _craftreminderconfig;
        private const string _entityCode = EntityCodes.CraftReminderConfig;
        //private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private string _remarks;
       //rivate IWorkFlowEngine<CraftReminderConfigWorkFlow> wfEngine;
      //  private ICraftReminderConfigRepository _craftReminderConfigRepository;
      
        private CompanyVO vo = null;

        public CraftReminderConfigWorkFlow(IUnitOfWork unitOfWork, CraftReminderConfig request, string remarks)
        {
            _unitOfWork = unitOfWork;
             _craftreminderconfig = request;
            _remarks = remarks;
           // _accountRepository = new AccountRepository(unitOfWork);
           // _craftReminderConfigRepository = new CraftReminderConfigRepository(unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(unitOfWork);

            vo = new CompanyVO();
        }

        public Entity Entity
        {
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public int userid
        {
            get { return _craftreminderconfig.CreatedBy; }
        }
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_craftreminderconfig.Craft.PortCode);
                return portcodes;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(_craftreminderconfig.CraftReminderConfigID, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var wfportcode = _craftreminderconfig.Craft.PortCode;

            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode, _approvecode = ConfigName.ApprovedCode } equals new { taskcode = _portGeneralConfigurationRepository.GetWFApprovedCode(wfportcode), portcode = pc.PortCode, _approvecode = pc.ConfigName }
                               //  join sr in _unitOfWork.Repository<ServiceRequest>().Query().Select() on w.WorkflowInstanceId equals sr.WorkflowInstanceId
                               where e.EntityCode == entitycode
                                 && w.ReferenceID == referenceno
                               //&& sr.IsFinal == "Y" &&  sr.RecordStatus == "A"

                               select w).Count();

            return _entitycode;
        }

        public CraftReminderConfigWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }
        public void UpdateStatus() { }

        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, _craftreminderconfig); }
        }
      
            
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            //_craftreminderconfig.WorkflowInstanceId = workFlowInstanceId;

            //_craftreminderconfig.IsFinal = null; 
            //_unitOfWork.Repository<CraftReminderConfig>().Update(_craftreminderconfig);
            //_unitOfWork.SaveChanges();
        }
           
      public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "WFAK":
                    break;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }

    }
}

