using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Services.WorkFlow
{
    class PortEntryPassWorkFlow: IWorkFlowEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private PermitRequest _permitrequest;
        private const string _entityCode = EntityCodes.PortEntryPassApplication;
        private IEntityRepository _entityRepository;
        private IWorkFlowTaskRepository _workFlowTaskRepository;
        private CompanyVO vo;

        private string _remarks;

        public PortEntryPassWorkFlow(IUnitOfWork unitOfWork, PermitRequest permitrequest, string remarks)
        {
            _unitOfWork = unitOfWork;
            _permitrequest = permitrequest;
            _remarks = remarks;
            _entityRepository = new EntityRepository(_unitOfWork);
            _workFlowTaskRepository = new WorkFlowTaskRepository(_unitOfWork);
            vo = new CompanyVO();
        }

        /// <summary>
        /// To get entity Details
        /// </summary>
        public Entity Entity
        {
            get
            {
                var entity = _entityRepository.GetEntityByCode(_entityCode);
                return entity;
            }
        }

        /// <summary>
        /// To get Request reference id
        /// </summary>
        public string ReferenceId
        {
            get { return _permitrequest.ReferenceNo; }
        }

        /// <summary>
        ///  to get Remarks
        /// </summary>
        public string Remarks
        {
            get { return _remarks; }
        }

        /// <summary>
        /// to get ReferenceData
        /// </summary>
        public string ReferenceData
        {
            get { return Common.GetTokensDictionaryForReferenceData(Entity, _permitrequest); }

        }

        /// <summary>
        /// to get GetRequestStatus
        /// </summary>
        /// <param name="entitycode"></param>
        /// <param name="referenceno"></param>
        /// <returns></returns>
        public int GetRequestStatus(string entitycode, string referenceno)
        {
            var _entitycode = _workFlowTaskRepository.GetRequestStatus(entitycode, referenceno);

            return _entitycode;
        }

        /// <summary>
        /// To get Port code List
        /// </summary>
        public List<string> PortCodes
        {
            get
            {
                List<string> portcodes = new List<string>();
                portcodes.Add(_permitrequest.PortCode);
                return portcodes;
            }
        }

         /// <summary>
        /// PortEntryPassWorkFlow
        /// </summary>
        public PortEntryPassWorkFlow()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            vo = new CompanyVO();
        }
        /// <summary>
        /// SetWorkFlow for each Port
        /// </summary>
        /// <param name="workFlowInstanceId"></param>
        /// <param name="portCode"></param>
        public void SetWorkFlowId(int workFlowInstanceId, string portCode)
        {
            _permitrequest.WorkflowInstanceId = workFlowInstanceId;
            _unitOfWork.Repository<PermitRequest>().Update(_permitrequest);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Create for Request
        /// </summary>
        public void Create()
        {
            //if (_arrivalnotification.VCN != null && _arrivalnotification.VCN != string.Empty &&)
            if (!string.IsNullOrEmpty(_permitrequest.ReferenceNo))
            {
                //UpdateStatus();
            }
            else
            {

                CodeGenerator codeGenerator = new CodeGenerator(_unitOfWork);
                _permitrequest.ReferenceNo = codeGenerator.ReferenceNo(_permitrequest.PortCode);

              //  List<PermitRequestAccessGates> PermitrequestAccessGateList = _permitrequest.PermitRequestAccessGates.ToList();
                List<PermitRequestArea> permitrequestAreaList = _permitrequest.PermitRequestAreas.ToList();
               // List<PermitRequestContractor> PermitrequestContractorList = _permitrequest.PermitRequestContractors.ToList();
                //List<PermitRequestDocument> permitrequestDocumentList = _permitrequest.PermitRequestDocuments.ToList();
                List<PermitRequestVerifyedDetail> permitrequestverifyDetailList = _permitrequest.PermitRequestVerifyedDetails.ToList();
              //  List<VehiclePermit> VehicalPermitList = _permitrequest.VehiclePermits.ToList();
              //  List<VisitorPermit> VisitorPermitList = _permitrequest.VisitorPermits.ToList();
               // List<WharfVehiclePermit> WharfVehiclePermitList = _permitrequest.WharfVehiclePermits.ToList();
               // List<PersonalPermit> PersonalPermitList = _permitrequest.PersonalPermits.ToList();
                List<IndividualPermitApplicationDetails> individualPermitApplicationDetailsList = _permitrequest.IndividualPermitApplicationDetails.ToList();
                List<IndividualPersonalPermit> individualPersonalPermitList = _permitrequest.IndividualPersonalPermits.ToList();
                List<PermitReason> permitReasonList = _permitrequest.PermitReasons.ToList();
                List<PermitRequestSubArea> permitRequestSubAreaList = _permitrequest.PermitRequestSubAreas.ToList();
                List<ContractorPermitApplicationDetails> contractorPermitApplicationDetailsList = _permitrequest.ContractorPermitApplicationDetails.ToList();
                List<ContractorPermitEmployeeDetails> contractorPermitEmployeeDetailsList = _permitrequest.ContractorPermitEmployeeDetails.ToList();


                if (permitrequestAreaList.Count > 0)
                {
                    foreach (var arealist in permitrequestAreaList)
                    {
                        arealist.PermitRequestID = _permitrequest.PermitRequestID;

                    }
                    _unitOfWork.Repository<PermitRequestArea>().InsertRange(permitrequestAreaList);

                }

                //if (permitrequestDocumentList.Count > 0)
                //{
                //    foreach (var documnets in permitrequestDocumentList)
                //    {
                //        documnets.PermitRequestID = _permitrequest.PermitRequestID;
                //        documnets.CreatedBy = _permitrequest.CreatedBy;
                //        documnets.CreatedDate = _permitrequest.CreatedDate;
                //        documnets.ModifiedBy = _permitrequest.ModifiedBy;
                //        documnets.ModifiedDate = _permitrequest.ModifiedDate;
                //        documnets.RecordStatus = "A";
                //    }
                //    _unitOfWork.Repository<PermitRequestDocument>().InsertRange(permitrequestDocumentList);
                //}
                if (permitrequestverifyDetailList.Count > 0)
                {
                    foreach (var verifydetail in permitrequestverifyDetailList)
                    {
                        verifydetail.permitrRequestID = _permitrequest.PermitRequestID;

                    }
                    _unitOfWork.Repository<PermitRequestVerifyedDetail>().InsertRange(permitrequestverifyDetailList);

                }

                if (individualPermitApplicationDetailsList.Count > 0)
                {
                    foreach (var details in individualPermitApplicationDetailsList)
                    {
                        details.PermitRequestID = _permitrequest.PermitRequestID;
                    }
                    _unitOfWork.Repository<IndividualPermitApplicationDetails>().InsertRange(individualPermitApplicationDetailsList);

                }


                if (individualPersonalPermitList.Count > 0)
                {
                    foreach (var permitdetails in individualPersonalPermitList)
                    {
                        permitdetails.IndividualPersonalPermitID = _permitrequest.PermitRequestID;
                    }
                    _unitOfWork.Repository<IndividualPersonalPermit>().InsertRange(individualPersonalPermitList);

                }

                if (permitReasonList.Count > 0)
                {
                    foreach (var resons in permitReasonList)
                    {
                        resons.PermitRequestID = _permitrequest.PermitRequestID;
                    }
                    _unitOfWork.Repository<PermitReason>().InsertRange(permitReasonList);

                }

                if (permitRequestSubAreaList.Count > 0)
                {
                    foreach (var subarea in permitRequestSubAreaList)
                    {
                        subarea.PermitRequestID = _permitrequest.PermitRequestID;
                    }
                    _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(permitRequestSubAreaList);

                }

                if (contractorPermitApplicationDetailsList.Count > 0)
                {
                    foreach (var  detailslist in contractorPermitApplicationDetailsList)
                    {
                        detailslist.PermitRequestID=_permitrequest.PermitRequestID;

                        detailslist.CreatedBy = _permitrequest.CreatedBy;
                        detailslist.CreatedDate = _permitrequest.CreatedDate;
                        detailslist.ModifiedBy = _permitrequest.ModifiedBy;
                        detailslist.ModifiedDate = _permitrequest.ModifiedDate;
                        detailslist.RecordStatus = "A";

                        _unitOfWork.Repository<ContractorPermitApplicationDetails>().InsertRange(contractorPermitApplicationDetailsList);
                    }
                }

                if (contractorPermitEmployeeDetailsList.Count > 0)
                {
                    foreach (var permitemployee in contractorPermitEmployeeDetailsList)
                    {
                        permitemployee.PermitRequestID = _permitrequest.PermitRequestID;

                        _unitOfWork.Repository<ContractorPermitEmployeeDetails>().InsertRange(contractorPermitEmployeeDetailsList);
                    }

                }


               // _unitOfWork.Repository<PermitRequest>().Insert(_permitrequest);

                //if (!string.IsNullOrEmpty(_permitrequest.ReferenceNo))
                //{

                //    _unitOfWork.ExecuteSqlCommand(" update dbo.PermitRequest SET RecordStatus = 'I', GeneratedVCN = @p0 WHERE VCN = @p1 ", _permitrequest.ReferenceNo, _permitrequest.ReferenceNo);
                //}

                codeGenerator.UpdateCode("VCN", _permitrequest.PortCode);

                _unitOfWork.SaveChanges();

            }
        }

        /// <summary>
        /// Execute DML
        /// </summary>
        /// <param name="workflowTaskCode"></param>
        public void ExecuteTask(string workflowTaskCode)
        {
            switch (workflowTaskCode)
            {
                case "NEW":
                    vo.UserType = UserType.Employee;
                    vo.UserTypeId = 0;
                    Create();
                    break;
                //case "UPDT":
                  //  vo.UserType = UserType.Employee;
                   // vo.UserTypeId = 0;
                    //UpdateStatus();
                  //  break;
                //case "VAP":
                 //   break;
                //case "WFSA": //Once Berthplanner Approve the AN Insert record(s) in VesselCall & VesselCallMovement tables
                 //   break;
                //case "VRES":
                    //vo.UserType = UserType.Agent;
                    //vo.UserTypeId = _arrivalnotification.AgentID;
                   // UpdateStatus();
                   // break;
                //case "VUPD":
                //    vo.UserType = UserType.Employee;
                //    vo.UserTypeId = 0;
                    //UpdateStatus();
                  //  break;
                //case "EXPI":
                //    break;
                //case "CLOS":
                //    break;
                //case "WFCA":
                   // Cancel();
                //    break;
                //case "WFRE":
                    //Reject();
                   // break;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// 

        public void UpdateStatus()
        {


            //  List<PermitRequestAccessGates> PermitrequestAccessGateList = _permitrequest.PermitRequestAccessGates.ToList();
            // List<PermitRequestArea> permitrequestAreaList = _permitrequest.PermitRequestAreas.ToList();
            // List<PermitRequestContractor> PermitrequestContractorList = _permitrequest.PermitRequestContractors.ToList();
            //List<PermitRequestDocument> permitrequestDocumentList = _permitrequest.PermitRequestDocuments.ToList();
            //List<PermitRequestVerifyedDetail> permitrequestverifyDetailList = _permitrequest.PermitRequestVerifyedDetails.ToList();
            //  List<VehiclePermit> VehicalPermitList = _permitrequest.VehiclePermits.ToList();
            //  List<VisitorPermit> VisitorPermitList = _permitrequest.VisitorPermits.ToList();
            // List<WharfVehiclePermit> WharfVehiclePermitList = _permitrequest.WharfVehiclePermits.ToList();
            // List<PersonalPermit> PersonalPermitList = _permitrequest.PersonalPermits.ToList();
            //List<IndividualPermitApplicationDetails> individualPermitApplicationDetailsList = _permitrequest.IndividualPermitApplicationDetails.ToList();
            //List<IndividualPersonalPermit> individualPersonalPermitList = _permitrequest.IndividualPersonalPermits.ToList();
            //List<PermitReason> permitReasonList = _permitrequest.PermitReasons.ToList();
            //List<PermitRequestSubArea> permitRequestSubAreaList = _permitrequest.PermitRequestSubAreas.ToList();
            // List<ContractorPermitApplicationDetails> contractorPermitApplicationDetailsList = _permitrequest.ContractorPermitApplicationDetails.ToList();
            //List<ContractorPermitEmployeeDetails> contractorPermitEmployeeDetailsList = _permitrequest.ContractorPermitEmployeeDetails.ToList();




            //if (permitrequestAreaList.Count > 0)
            //{
            //    foreach (var arealist in permitrequestAreaList)
            //    {
            //        arealist.PermitRequestID = _permitrequest.PermitRequestID;

            //    }
            //    _unitOfWork.Repository<PermitRequestArea>().InsertRange(permitrequestAreaList);

            //}

            //if (permitrequestDocumentList.Count > 0)
            //{
            //    foreach (var documnets in permitrequestDocumentList)
            //    {
            //        documnets.PermitRequestID = _permitrequest.PermitRequestID;
            //        //documnets.CreatedBy = _permitrequest.CreatedBy;
            //        //documnets.CreatedDate = _permitrequest.CreatedDate;
            //        //documnets.ModifiedBy = _permitrequest.ModifiedBy;
            //        //documnets.ModifiedDate = _permitrequest.ModifiedDate;
            //        //documnets.RecordStatus = "A";
            //    }
            //    _unitOfWork.Repository<PermitRequestDocument>().InsertRange(permitrequestDocumentList);
            //}
           // if (permitrequestverifyDetailList.Count > 0)
            //{
                //foreach (var verifydetail in permitrequestverifyDetailList)
                //{
                //    verifydetail.permitrRequestID = _permitrequest.PermitRequestID;

                //}
                //_unitOfWork.Repository<PermitRequestVerifyedDetail>().InsertRange(permitrequestverifyDetailList);

           // }

            //if (individualPermitApplicationDetailsList.Count > 0)
            //{
            //    foreach (var details in individualPermitApplicationDetailsList)
            //    {
            //        details.PermitRequestID = _permitrequest.PermitRequestID;
            //    }
            //    _unitOfWork.Repository<IndividualPermitApplicationDetails>().InsertRange(individualPermitApplicationDetailsList);

            //}


            //if (individualPersonalPermitList.Count > 0)
            //{
            //    foreach (var permitdetails in individualPersonalPermitList)
            //    {
            //        permitdetails.IndividualPersonalPermitID = _permitrequest.PermitRequestID;
            //    }
            //    _unitOfWork.Repository<IndividualPersonalPermit>().InsertRange(individualPersonalPermitList);

            //}

            //if (permitReasonList.Count > 0)
            //{
            //    foreach (var resons in permitReasonList)
            //    {
            //        resons.PermitRequestID = _permitrequest.PermitRequestID;
            //    }
            //    _unitOfWork.Repository<PermitReason>().InsertRange(permitReasonList);

            //}

            //if (permitRequestSubAreaList.Count > 0)
            //{
            //    foreach (var subarea in permitRequestSubAreaList)
            //    {
            //        subarea.PermitRequestID = _permitrequest.PermitRequestID;
            //    }
            //    _unitOfWork.Repository<PermitRequestSubArea>().InsertRange(permitRequestSubAreaList);

            //}

            //if (contractorPermitApplicationDetailsList.Count > 0)
            //{
            //    foreach (var  detailslist in contractorPermitApplicationDetailsList)
            //    {
            //        detailslist.PermitRequestID=_permitrequest.PermitRequestID;

            //        detailslist.CreatedBy = _permitrequest.CreatedBy;
            //        detailslist.CreatedDate = _permitrequest.CreatedDate;
            //        detailslist.ModifiedBy = _permitrequest.ModifiedBy;
            //        detailslist.ModifiedDate = _permitrequest.ModifiedDate;
            //        detailslist.RecordStatus = "A";

            //        _unitOfWork.Repository<ContractorPermitApplicationDetails>().InsertRange(contractorPermitApplicationDetailsList);
            //    }
            //}

            //if (contractorPermitEmployeeDetailsList.Count > 0)
            //{
            //    foreach (var permitemployee in contractorPermitEmployeeDetailsList)
            //    {
            //        permitemployee.PermitRequestID = _permitrequest.PermitRequestID;

            //        _unitOfWork.Repository<ContractorPermitEmployeeDetails>().InsertRange(contractorPermitEmployeeDetailsList);
            //    }


            //}
            //_unitOfWork.SaveChanges();


            //_permitrequest.ObjectState = ObjectState.Modified;
           // _unitOfWork.Repository<PermitRequest>().Update(_permitrequest);
           // _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// to Add Company details
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public CompanyVO GetCompanyDetails(int step)
        {
            return vo;
        }


    }

    }
     

