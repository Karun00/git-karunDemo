using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace IPMS.Services.WorkFlow
{
    public class VesselRegistrationWorkFlow : ServiceBase, IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private Vessel _vesselRegistrationService;
        private IAccountRepository _accountRepository;
        private const string _entityCode = EntityCodes.VESLREG;
        private string _remarks;
        CompanyVO vo;


        public VesselRegistrationWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();

        }
        public VesselRegistrationWorkFlow(IUnitOfWork unitOfWork, Vessel vesselRegistrationService, string remarks)
        {
            _unitOfWork = unitOfWork;
            _vesselRegistrationService = vesselRegistrationService;
            _remarks = remarks;
            vo = new CompanyVO();
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public int userid
        {
            get { return _vesselRegistrationService.CreatedBy; }
        }

        public Entity Entity
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                var entity = (from e in _unitOfWork.Repository<Entity>().Query().Select()//.Where(e => e.EntityCode.Equals(_entityCode))
                              where e.EntityCode == _entityCode
                              select e).FirstOrDefault<Entity>();
                return entity;
            }
        }

        public string ReferenceId
        {
            get { return Convert.ToString(_vesselRegistrationService.IMONo, CultureInfo.InvariantCulture); }
        }

        public string Remarks
        {
            get { return _remarks; }
        }

        public string ReferenceData
        {

            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);

                //TODO: Suresh has to pass VesselType Subcategory name instead of code
                _vesselRegistrationService.VesselType = _unitOfWork.SqlQuery<SubCategory>("select * from SubCategory where SubCatCode = @p0 and SupCatCode = @p1", _vesselRegistrationService.VesselType.ToString(), SuperCategoryConstants.VESSELREG_VESSELTYPECODE).FirstOrDefault<SubCategory>().SubCatName;

                //TODO: Suresh has to pass VesselNationality Subcategory name instead of code
                _vesselRegistrationService.VesselNationality = _unitOfWork.SqlQuery<SubCategory>("select * from SubCategory where SubCatCode = @p0 and SupCatCode = @p1", _vesselRegistrationService.VesselNationality.ToString(), SuperCategoryConstants.VESSELREG_VESSELNATCODE).FirstOrDefault<SubCategory>().SubCatName;

                return Common.GetTokensDictionaryForReferenceData(entity, _vesselRegistrationService);

            }

        }

        public int GetRequestStatus(string pEntityCode, string pReferenceNo)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == pEntityCode
                                 && w.ReferenceID == pReferenceNo

                               select w).Count();

            return _entitycode;
        }


        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();

                portcodes.Add(_PortCode);

                return portcodes;
            }
        }



        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {

            string updateQuery = "update dbo.Vessel set WorkflowInstanceId = " + workFlowInstanceId.ToString(CultureInfo.InvariantCulture) + " where IMONo = '" + _vesselRegistrationService.IMONo.ToString(CultureInfo.InvariantCulture) + "'";
            _unitOfWork.ExecuteSqlCommand(updateQuery);

        }

        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    _vesselRegistrationService.IsFinal = "N";
                    vo.UserType = "EMP";
                    vo.UserTypeId = 0;
                    Create();
                    break;
                case "UPDT":
                    vo.UserType = "EMP";
                    vo.UserTypeId = 0;
                    Create();
                    break;
                case "WFSA":
                    _vesselRegistrationService.RecordStatus = "A";
                    _vesselRegistrationService.IsFinal = "Y";
                    vo.UserType = "EMP";
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "WFRE":
                    _vesselRegistrationService.RecordStatus = "I";
                    _vesselRegistrationService.IsFinal = "Y";
                    vo.UserType = "EMP";
                    vo.UserTypeId = 0;
                    UpdateStatus();
                    break;
                case "CLOS":
                    break;
            }
        }

        public void Create()
        {

            _unitOfWork.SaveChanges();

        }



        public void UpdateStatus()
        {
            int _usertypeid;
            if (Convert.ToInt32(_vesselRegistrationService.ModifiedBy, CultureInfo.InvariantCulture) == 0)
                _usertypeid = _vesselRegistrationService.CreatedBy;
            else
                _usertypeid = Convert.ToInt32(_vesselRegistrationService.ModifiedBy, CultureInfo.InvariantCulture);

            var usertype = _unitOfWork.Repository<User>().Find(_usertypeid).UserType;

            if (usertype == UserType.Agent)
            {
                vo.UserType = UserType.Agent;
                vo.UserTypeId = _usertypeid;
            }
            else if (usertype == UserType.TerminalOperator)
            {
                vo.UserType = UserType.TerminalOperator;
                vo.UserTypeId = _usertypeid;
            }
            else
            {
                vo.UserType = UserType.Employee;
                vo.UserTypeId = 0;
            }

            //if (_vesselRegistrationService.RecordStatus == "I")
            //{
            _unitOfWork.ExecuteSqlCommand("update dbo.Vessel set RecordStatus=@p0, ModifiedBy=@p1,ModifiedDate=GetDate(),IsFinal= @p2 where VesselID = @p3", _vesselRegistrationService.RecordStatus, _vesselRegistrationService.ModifiedBy, _vesselRegistrationService.IsFinal, _vesselRegistrationService.VesselID);
            //}
        }

        /*public void SetWorkFlowData(ref WorkflowInstance instance)
        {
            instance.ReferenceID = _notification.VCN;
            instance.WorkflowCode = _workFlowCode;
        }*/


        public int EntityID
        {
            //TODO: Write code here to get Entity for _entityCode defined above.
            get
            {
                Entity entity = _accountRepository.GetEntity(_entityCode);
                return entity.EntityID;
            }
        }

        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }

        //public int EntityID
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
}


