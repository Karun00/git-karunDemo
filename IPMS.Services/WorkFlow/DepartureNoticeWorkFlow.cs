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
    public class DepartureNoticeWorkFlow : IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private DepartureNotice _DepartureNotice;
        private const string _entityCode = EntityCodes.DepartureNotice;
        private string _remarks;

       // private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
       // private IWorkFlowEngine<DepartureNoticeWorkFlow> wfEngine;
       // private IDepartureNoticeRepository _DepartureNoticeRepository;
        //private IPortConfigurationRepository _portConfigurationRepository;

        public DepartureNoticeWorkFlow(IUnitOfWork unitOfWork, DepartureNotice request, string remarks)
        {
            _unitOfWork = unitOfWork;
            _DepartureNotice = request;
            _remarks = remarks;
           // wfEngine = new WorkFlowEngine<DepartureNoticeWorkFlow>();
           // _accountRepository = new AccountRepository(unitOfWork);
            //_DepartureNoticeRepository = new DepartureNoticeRepository(unitOfWork);
           // _portConfigurationRepository = new PortConfigurationRepository(unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(unitOfWork);
        }

        public Entity Entity
        {
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public int userid
        {
            get { return _DepartureNotice.CreatedBy; }
        }

        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_DepartureNotice.PortCode);
                return portcodes;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(_DepartureNotice.DepartureID,CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public int GetRequestStatus(string pentitycode, string preferenceno)
        {
            var entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortGeneralConfig>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode, _approvecode = ConfigName.ApprovedCode } equals new { taskcode = _portGeneralConfigurationRepository.GetWFApprovedCode(_DepartureNotice.PortCode), portcode = pc.PortCode, _approvecode = pc.ConfigName }
                               join sr in _unitOfWork.Repository<DepartureNotice>().Query().Select() on w.WorkflowInstanceId equals sr.WorkflowInstanceId
                               where e.EntityCode == pentitycode && w.ReferenceID == preferenceno && sr.IsFinal == "Y" && sr.RecordStatus == "A"

                               select w).Count();

            return entitycode;
        }

        public DepartureNoticeWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
        }

        public void UpdateStatus()
        {
            _DepartureNotice.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<DepartureNotice>().Update(_DepartureNotice);
            _unitOfWork.SaveChanges();
        }

        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, _DepartureNotice); }
        }

        public void Create()
        {
            _unitOfWork.Repository<DepartureNotice>().Insert(_DepartureNotice);

            _unitOfWork.SaveChanges();
        }

        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            _DepartureNotice.WorkflowInstanceId = workFlowInstanceId;

            _DepartureNotice.IsFinal = null; //It is Computed column, should not get any value before saving
            _unitOfWork.Repository<DepartureNotice>().Update(_DepartureNotice);
            _unitOfWork.SaveChanges();
        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    Create();
                    break;
                case "UPDT":
                    break;
                case "WFAK":
                    break;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            CompanyVO vo = new CompanyVO();
            vo.UserType = "EMP";
            vo.UserTypeId = 0;
            return vo;
        }
    }
}
