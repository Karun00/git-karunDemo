using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services;
using IPMS.Services.WorkFlow;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class ArrivalNotoficationWorkFlowTest
    {

        protected TnpaContext context;
        protected IUnitOfWork _unitOfWork;
        private const string _entityCode = EntityCodes.Arrival_Notification;
        private PortConfigurationRepository _portconfiguration;

        [SetUp]
        public void Init()
        { }

        [Test]
        public void AddArrivalNotification_EDI_Test()
        {
            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            ArrivalNotificationService srv = new ArrivalNotificationService(_unitOfWork);
            anvo.VCN = "";
            anvo.AnyAdditionalInfo = "EDI";
            srv._LoginName = "agent1";
            srv._UserId = 2;
            srv._PortCode = "CT";
            ArrivalNotificationVO addanvo = srv.AddArrivalNotification(anvo);

        }

        [Test]
        public void CreateArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            string remarks = "New";

            var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                       select t).ToList<ArrivalNotification>();

            if (arrivalnotification.Count == 0)
            {

                anvo.VCN = "";
                anvo.PortCode = "DB";
                anvo.AgentID = 2;
                anvo.VesselID = 6;
                anvo.VoyageIn = "jh";
                anvo.VoyageOut = "hjh";
                //anvo.ETA = new DateTime(2014, 5, 1, 1, 5, 0);
                //anvo.ETD = new DateTime(2014, 5, 1, 1, 5, 0);
                anvo.ETA = "2014/12/12 12:00:00";
                anvo.ETD = "2014/12/12 12:00:00";
                anvo.ArrDraft = "12";
                anvo.DepDraft = "12";
                anvo.ReasonForVisit = "BUNK";
                anvo.IsTerminalOperator = "I";
                anvo.TerminalOperatorID = null;
                anvo.LastPortOfCall = "12121211";
                anvo.NextPortOfCall = "1212121212";
                anvo.NominationDate = null;
                anvo.AppliedForISPS = "I";
                anvo.AppliedDate = null;
                anvo.Clearance = "I";
                anvo.ISPSReferenceNo = "";
                anvo.PilotExemption = "I";
                anvo.ExemptionPilotID = null;
                anvo.PreferredPortCode = "DB";
                anvo.PreferredQuayCode = "BF";
                anvo.PreferredBerthCode = "B4";
                anvo.AlternatePortCode = "DB";
                anvo.AlternateQuayCode = "BF";
                anvo.AlternateBerthCode = "B1";
                anvo.PreferredSideDock = "STBO";
                anvo.PreferredSideAlternateBirth = "POSI";
                anvo.AlternateBerthKey = "DB.BF.B1";
                anvo.PreferedBerthKey = "DB.BF.B1";
                anvo.ReasonAlternateBirth = "ghj";
                
                //ArrivalCommodity aclvo = new ArrivalCommodity();
                ////aclvo.CommodityBerthKey = "DB.BF.B1";
                //aclvo.CargoType = "ATM";
                //aclvo.Package = "DC";
                //aclvo.UOM = "MT";
                //aclvo.Quantity = 22;
                //aclvo.RecordStatus = "A";
                //aclvo.CreatedBy = 1;
                ////aclvo.CreatedDatedatetime = DateTime.Now.ToString();
                //aclvo.ModifiedBy = 1;
                ////aclvo.ModifiedDatedatetime = DateTime.Now.ToString();

                //_unitOfWork.Repository<ArrivalCommodity>().Insert(aclvo);
                //_unitOfWork.SaveChanges();

                //aclvo.ArrivalNotification.ArrivalCommodities = null;
                //anvo.ArrivalCommodities = aclvo.MapToDTO();

                anvo.Tidal = "I";
                anvo.BallastWater = "I";
                anvo.WasteDeclaration = "I";
                anvo.DaylightRestriction = "I";
                anvo.DaylightSpecifyReason = "hjj";
                anvo.ExceedPortLimitations = "I";
                anvo.ExceedSpecifyReason = "hjg";
                anvo.AnyAdditionalInfo = "EDI";
                anvo.PlanDateTimeOfBerth = new DateTime(2014, 7, 1, 0, 0, 0);
                anvo.PlanDateTimeToVacateBerth = new DateTime(2014, 7, 2, 1, 5, 0);
                anvo.PlanDateTimeToStartCargo = new DateTime(2014, 7, 3, 1, 5, 0);
                anvo.PlanDateTimeToCompleteCargo = new DateTime(2014, 7, 4, 1, 5, 0);
                anvo.AnyDangerousGoodsonBoard = "I";
                anvo.DangerousGoodsClass = null;
                anvo.UNNo = "2545446";
                anvo.LoadDischargeDate = new DateTime(2014, 6, 29, 0, 0, 0);
                anvo.IMDGNetQty = 200;
                anvo.CellNo = "254334534";
                anvo.CargoDescription = "";
                anvo.PlannedDurationDate = null;
                anvo.PlannedDurationToDate = null;
                anvo.ReasonForLayup = "";
                anvo.BunkersRequired = "";
                anvo.BunkersMethod = "";
                anvo.BunkerService = 2;
                anvo.DistanceFromStern = 0;
                anvo.TonsMT = 0;
                anvo.AnyImpInfo = "";
                anvo.RecordStatus = "A";
                anvo.CreatedBy = 2;
                anvo.CreatedDate = DateTime.Now;
                anvo.ModifiedBy = 2;
                anvo.ModifiedDate = DateTime.Now;
                anvo.WorkflowInstanceId = null;

              
                ArrivalNotificationService service = new ArrivalNotificationService(new UnitOfWork(new TnpaContext()));
                ArrivalNotificationVO add_arrivalNotification = service.AddArrivalNotification(anvo);

                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == anvo.VCN
                               select wfi).ToList();
                if (wfidata.Count == 0)
                {
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, anvo.MapToEntity(), remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, anvo.PortCode, anvo.CreatedBy);
                    wf.Process(arrivalNotificationWorkFlow, "NEW");
                }

            }
            else
            {
                foreach (var arrivalnotificationdata in arrivalnotification)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == arrivalnotificationdata.VCN
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        ArrivalNotificationVO anvo1 = new ArrivalNotificationVO();
                        anvo1 = arrivalnotificationdata.MapToDto();
                        var brt = _unitOfWork.ExecuteSqlCommand(" delete dbo.ArrivalNotification where VCN = @p0", arrivalnotificationdata.VCN);
                        ArrivalNotificationService ANS = new ArrivalNotificationService();
                        ANS._LoginName = "agent1";
                        ANS._PortCode = anvo1.PortCode;
                        ANS._UserId = 2;
                        anvo1.VCN = "";
                        ArrivalNotificationVO add_arrivalNotification = ANS.AddArrivalNotification(anvo1);
                    }
                }
            }
          

        }

        [Test]
        public void ArrivalNotification_Workflow_SaveInstance()
        {
            context = new TnpaContext();
            //_unitOfWork = new UnitOfWork(context);

            using (_unitOfWork = new UnitOfWork(context))
            {

                ArrivalNotificationVO anvo = new ArrivalNotificationVO();

                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           select t).ToList<ArrivalNotification>();

                foreach (var arrivalnotificationdata in arrivalnotification)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == arrivalnotificationdata.VCN
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        #region Params
                        var wfi = new WorkflowInstance();

                        wfi.WorkflowInstanceId = 0;
                        wfi.EntityID = 14;// EntityCodes.Arrival_Notification;
                        wfi.PortCode = arrivalnotificationdata.PortCode;
                        wfi.ReferenceID = arrivalnotificationdata.VCN;
                        wfi.WorkflowTaskCode = "NEW";
                        wfi.UserTypeId = 0;
                        wfi.UserType = "";
                        wfi.RecordStatus = "A";
                        wfi.CreatedBy = 1;
                        wfi.CreatedDate = DateTime.Now;
                        wfi.ModifiedBy = 1;
                        wfi.ModifiedDate = DateTime.Now;

                        var procedure = new WorkflowInstance.WorkflowInstance_Dml_Proc(wfi);
                        var result = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(procedure);

                        var wfp = new WorkflowProcess();
                        wfp.WorkflowProcessId = 0;
                        wfp.WorkflowInstanceId = wfi.WorkflowInstanceId;
                        wfp.RoleId = 2;
                        wfp.ReferenceData = "{'VCN':'VCN201400001','PortCode':'DB','VesselName':'TIRTA SAMUDRA XXI','PreferredBerthName':'','AlternateBerthName':'','IMONo':'9577927','ETA':'7/11/2014 1:30:00 AM','ETD':'7/11/2014 3:30:00 AM'}";
                        wfp.Remarks = "New Arrival Notification";
                        wfp.FromTaskCode = "NEW";
                        wfp.ToTaskCode = "NEW";
                        wfp.RecordStatus = "A";
                        wfp.CreatedBy = 1;
                        wfp.CreatedDate = DateTime.Now;
                        wfp.ModifiedBy = 1;
                        wfp.ModifiedDate = DateTime.Now;

                        var subprocedure = new WorkflowProcess.WorkflowProcess_Dml_Proc(wfp);
                        var sresult = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(subprocedure);

                        #endregion
                    }
                }
            }

        }

        [Test]
        public void ApproveArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Approved";
            ArrivalNotification an = new ArrivalNotification();
            _portconfiguration = new PortConfigurationRepository(_unitOfWork);
            int _userid = 4;
            string _portcode = "DB";

            var portcode = new SqlParameter("@portcode", _portcode);
            var userid = new SqlParameter("@userid", _userid);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();
          

            foreach (var arrivalnotificationdata in pt)
            {
                an.VCN = arrivalnotificationdata.ReferenceID;

                if (arrivalnotificationdata.EntityCode == EntityCodes.Arrival_Notification) //Only ArrivalNotifications
                {

                    var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                               where t.VCN == arrivalnotificationdata.ReferenceID
                                               select t).FirstOrDefault<ArrivalNotification>();
                    ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                    WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotification.PortCode, _userid);
                    wf.Process(arrivalNotificationWorkFlow, _portconfiguration.GetPortConfiguration(arrivalnotification.PortCode).ApproveCode);
                }
           }

          
        }

        [Test]
        public void ResubmitArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Resubmit";
            ArrivalNotification an = new ArrivalNotification();

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                an.VCN = arrivalnotificationdata.ReferenceID;
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();
                ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork,"DB", 2);
                wf.Process(arrivalNotificationWorkFlow, "VRES");
               
            }


        }

        [Test]
        public void CreateIMDGArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            string remarks = "New IMDG";

            var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                       select t).ToList<ArrivalNotification>();

            if (arrivalnotification.Count == 0)
            {

                anvo.VCN = "VCN201400001";
                anvo.PortCode = "DB";
                anvo.AgentID = 2;
                anvo.VesselID = 6;
                anvo.VoyageIn = "jh";
                anvo.VoyageOut = "hjh";
                //anvo.ETA = new DateTime(2014, 5, 1, 1, 5, 0);
                //anvo.ETD = new DateTime(2014, 5, 1, 1, 5, 0);
                anvo.ETA = "2014/12/12 12:00:00";
                anvo.ETD = "2014/12/12 12:00:00";
                anvo.ArrDraft = "12";
                anvo.DepDraft = "12";
                anvo.ReasonForVisit = "BUNK";
                anvo.IsTerminalOperator = "I";
                anvo.TerminalOperatorID = null;
                anvo.LastPortOfCall = "12121211";
                anvo.NextPortOfCall = "1212121212";
                anvo.NominationDate = null;
                anvo.AppliedForISPS = "I";
                anvo.AppliedDate = null;
                anvo.Clearance = "I";
                anvo.ISPSReferenceNo = "";
                anvo.PilotExemption = "I";
                anvo.ExemptionPilotID = null;
                anvo.PreferredPortCode = "DB";
                anvo.PreferredQuayCode = "BF";
                anvo.PreferredBerthCode = "B4";
                anvo.AlternatePortCode = "DB";
                anvo.AlternateQuayCode = "BF";
                anvo.AlternateBerthCode = "B1";
                anvo.PreferredSideDock = "STBO";
                anvo.PreferredSideAlternateBirth = "POSI";
                anvo.ReasonAlternateBirth = "ghj";
                anvo.Tidal = "I";
                anvo.BallastWater = "I";
                anvo.WasteDeclaration = "I";
                anvo.DaylightRestriction = "I";
                anvo.DaylightSpecifyReason = "hjj";
                anvo.ExceedPortLimitations = "I";
                anvo.ExceedSpecifyReason = "hjg";
                anvo.AnyAdditionalInfo = "hjghj";
                anvo.PlanDateTimeOfBerth = new DateTime(2014, 7, 1, 0, 0, 0);
                anvo.PlanDateTimeToVacateBerth = new DateTime(2014, 7, 2, 1, 5, 0);
                anvo.PlanDateTimeToStartCargo = new DateTime(2014, 7, 3, 1, 5, 0);
                anvo.PlanDateTimeToCompleteCargo = new DateTime(2014, 7, 4, 1, 5, 0);
                anvo.AnyDangerousGoodsonBoard = "I";
                anvo.DangerousGoodsClass = null;
                anvo.UNNo = "2545446";
                anvo.LoadDischargeDate = new DateTime(2014, 6, 29, 0, 0, 0);
                anvo.IMDGNetQty = 200;
                anvo.CellNo = "254334534";
                anvo.CargoDescription = "";
                anvo.PlannedDurationDate = null;
                anvo.PlannedDurationToDate = null;
                anvo.ReasonForLayup = "";
                anvo.BunkersRequired = "";
                anvo.BunkersMethod = "";
                anvo.BunkerService = 0;
                anvo.DistanceFromStern = 0;
                anvo.TonsMT = 0;
                anvo.AnyImpInfo = "";
                anvo.RecordStatus = "A";
                anvo.CreatedBy = 2;
                anvo.CreatedDate = DateTime.Now;
                anvo.ModifiedBy = 2;
                anvo.ModifiedDate = DateTime.Now;
                anvo.WorkflowInstanceId = null;


                ArrivalNotificationService service = new ArrivalNotificationService(new UnitOfWork(new TnpaContext()));
               
                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == anvo.VCN
                               select wfi).ToList();
                if (wfidata.Count == 0)
                {
                    IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, anvo.MapToEntity(), remarks);
                    WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, anvo.PortCode, anvo.CreatedBy);
                    wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                }

            }
            else
            {
                foreach (var arrivalnotificationdata in arrivalnotification)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == arrivalnotificationdata.VCN
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotificationdata, remarks);
                        WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotificationdata.PortCode, arrivalnotificationdata.CreatedBy);
                        wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                    }
                }
            }
           

        }
        
        [Test]
        public void ApproveIMDGArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Approved IMDG";           

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();
                IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork,"DB", 2);
                wf.Process(arrivalNotificationWorkFlow, "WFSA");

            }


        }

        [Test]
        public void ResubmitIMDGArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Resubmit IMDG";
          

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();

                IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork,"DB", 2);
                wf.Process(arrivalNotificationWorkFlow, "VRES");

            }


        }

        [Test]
        public void CreateISPSArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            string remarks = "New ISPS";

            var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                       select t).ToList<ArrivalNotification>();

            if (arrivalnotification.Count == 0)
            {

                anvo.VCN = "VCN201400001";
                anvo.PortCode = "DB";
                anvo.AgentID = 2;
                anvo.VesselID = 6;
                anvo.VoyageIn = "jh";
                anvo.VoyageOut = "hjh";
                //anvo.ETA = new DateTime(2014, 5, 1, 1, 5, 0);
                //anvo.ETD = new DateTime(2014, 5, 1, 1, 5, 0);
                anvo.ETA = "2014/12/12 12:00:00";
                anvo.ETD = "2014/12/12 12:00:00";
                anvo.ArrDraft = "12";
                anvo.DepDraft = "12";
                anvo.ReasonForVisit = "BUNK";
                anvo.IsTerminalOperator = "I";
                anvo.TerminalOperatorID = null;
                anvo.LastPortOfCall = "12121211";
                anvo.NextPortOfCall = "1212121212";
                anvo.NominationDate = null;
                anvo.AppliedForISPS = "I";
                anvo.AppliedDate = null;
                anvo.Clearance = "I";
                anvo.ISPSReferenceNo = "";
                anvo.PilotExemption = "I";
                anvo.ExemptionPilotID = null;
                anvo.PreferredPortCode = "DB";
                anvo.PreferredQuayCode = "BF";
                anvo.PreferredBerthCode = "B4";
                anvo.AlternatePortCode = "DB";
                anvo.AlternateQuayCode = "BF";
                anvo.AlternateBerthCode = "B1";
                anvo.PreferredSideDock = "STBO";
                anvo.PreferredSideAlternateBirth = "POSI";
                anvo.ReasonAlternateBirth = "ghj";
                anvo.Tidal = "I";
                anvo.BallastWater = "I";
                anvo.WasteDeclaration = "I";
                anvo.DaylightRestriction = "I";
                anvo.DaylightSpecifyReason = "hjj";
                anvo.ExceedPortLimitations = "I";
                anvo.ExceedSpecifyReason = "hjg";
                anvo.AnyAdditionalInfo = "hjghj";
                anvo.PlanDateTimeOfBerth = new DateTime(2014, 7, 1, 0, 0, 0);
                anvo.PlanDateTimeToVacateBerth = new DateTime(2014, 7, 2, 1, 5, 0);
                anvo.PlanDateTimeToStartCargo = new DateTime(2014, 7, 3, 1, 5, 0);
                anvo.PlanDateTimeToCompleteCargo = new DateTime(2014, 7, 4, 1, 5, 0);
                anvo.AnyDangerousGoodsonBoard = "I";
                anvo.DangerousGoodsClass = null;
                anvo.UNNo = "2545446";
                anvo.LoadDischargeDate = new DateTime(2014, 6, 29, 0, 0, 0);
                anvo.IMDGNetQty = 200;
                anvo.CellNo = "254334534";
                anvo.CargoDescription = "";
                anvo.PlannedDurationDate = null;
                anvo.PlannedDurationToDate = null;
                anvo.ReasonForLayup = "";
                anvo.BunkersRequired = "";
                anvo.BunkersMethod = "";
                anvo.BunkerService = 0;
                anvo.DistanceFromStern = 0;
                anvo.TonsMT = 0;
                anvo.AnyImpInfo = "";
                anvo.RecordStatus = "A";
                anvo.CreatedBy = 2;
                anvo.CreatedDate = DateTime.Now;
                anvo.ModifiedBy = 2;
                anvo.ModifiedDate = DateTime.Now;
                anvo.WorkflowInstanceId = null;


                ArrivalNotificationService service = new ArrivalNotificationService(new UnitOfWork(new TnpaContext()));

                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == anvo.VCN
                               select wfi).ToList();
                if (wfidata.Count == 0)
                {
                    //IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, anvo.MapToEntity(), remarks);
                    //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, anvo.PortCode, anvo.CreatedBy);
                    //wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                }

            }
            else
            {
                foreach (var arrivalnotificationdata in arrivalnotification)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == arrivalnotificationdata.VCN
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        //IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotificationdata, remarks);
                        //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotificationdata.PortCode, arrivalnotificationdata.CreatedBy);
                        //wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                    }
                }
            }


        }
        
        [Test]
        public void ApproveISPSArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Approved ISPS";

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();
                //IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, "DB", 2);
                //wf.Process(arrivalNotificationWorkFlow, "WFSA");

            }


        }

        [Test]
        public void ResubmitISPSArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Resubmit ISPS";


            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();

                //IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, "DB", 2);
                //wf.Process(arrivalNotificationWorkFlow, "VRES");

            }


        }
         
        [Test]
        public void CreatePHArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            string remarks = "New PH";

            var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                       select t).ToList<ArrivalNotification>();

            if (arrivalnotification.Count == 0)
            {

                anvo.VCN = "VCN201400001";
                anvo.PortCode = "DB";
                anvo.AgentID = 2;
                anvo.VesselID = 6;
                anvo.VoyageIn = "jh";
                anvo.VoyageOut = "hjh";
                //anvo.ETA = new DateTime(2014, 5, 1, 1, 5, 0);
                //anvo.ETD = new DateTime(2014, 5, 1, 1, 5, 0);
                anvo.ETA = "2014/12/12 12:00:00";
                anvo.ETD = "2014/12/12 12:00:00";
                anvo.ArrDraft = "12";
                anvo.DepDraft = "12";
                anvo.ReasonForVisit = "BUNK";
                anvo.IsTerminalOperator = "I";
                anvo.TerminalOperatorID = null;
                anvo.LastPortOfCall = "12121211";
                anvo.NextPortOfCall = "1212121212";
                anvo.NominationDate = null;
                anvo.AppliedForISPS = "I";
                anvo.AppliedDate = null;
                anvo.Clearance = "I";
                anvo.ISPSReferenceNo = "";
                anvo.PilotExemption = "I";
                anvo.ExemptionPilotID = null;
                anvo.PreferredPortCode = "DB";
                anvo.PreferredQuayCode = "BF";
                anvo.PreferredBerthCode = "B4";
                anvo.AlternatePortCode = "DB";
                anvo.AlternateQuayCode = "BF";
                anvo.AlternateBerthCode = "B1";
                anvo.PreferredSideDock = "STBO";
                anvo.PreferredSideAlternateBirth = "POSI";
                anvo.ReasonAlternateBirth = "ghj";
                anvo.Tidal = "I";
                anvo.BallastWater = "I";
                anvo.WasteDeclaration = "I";
                anvo.DaylightRestriction = "I";
                anvo.DaylightSpecifyReason = "hjj";
                anvo.ExceedPortLimitations = "I";
                anvo.ExceedSpecifyReason = "hjg";
                anvo.AnyAdditionalInfo = "hjghj";
                anvo.PlanDateTimeOfBerth = new DateTime(2014, 7, 1, 0, 0, 0);
                anvo.PlanDateTimeToVacateBerth = new DateTime(2014, 7, 2, 1, 5, 0);
                anvo.PlanDateTimeToStartCargo = new DateTime(2014, 7, 3, 1, 5, 0);
                anvo.PlanDateTimeToCompleteCargo = new DateTime(2014, 7, 4, 1, 5, 0);
                anvo.AnyDangerousGoodsonBoard = "I";
                anvo.DangerousGoodsClass = null;
                anvo.UNNo = "2545446";
                anvo.LoadDischargeDate = new DateTime(2014, 6, 29, 0, 0, 0);
                anvo.IMDGNetQty = 200;
                anvo.CellNo = "254334534";
                anvo.CargoDescription = "";
                anvo.PlannedDurationDate = null;
                anvo.PlannedDurationToDate = null;
                anvo.ReasonForLayup = "";
                anvo.BunkersRequired = "";
                anvo.BunkersMethod = "";
                anvo.BunkerService = 0;
                anvo.DistanceFromStern = 0;
                anvo.TonsMT = 0;
                anvo.AnyImpInfo = "";
                anvo.RecordStatus = "A";
                anvo.CreatedBy = 2;
                anvo.CreatedDate = DateTime.Now;
                anvo.ModifiedBy = 2;
                anvo.ModifiedDate = DateTime.Now;
                anvo.WorkflowInstanceId = null;


                ArrivalNotificationService service = new ArrivalNotificationService(new UnitOfWork(new TnpaContext()));

                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == anvo.VCN
                               select wfi).ToList();
                if (wfidata.Count == 0)
                {
                    //IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, anvo.MapToEntity(), remarks);
                    //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, anvo.PortCode, anvo.CreatedBy);
                    //wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                }

            }
            else
            {
                foreach (var arrivalnotificationdata in arrivalnotification)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == arrivalnotificationdata.VCN
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        //IMDGArrivalNotificationWorkFlow imdgarrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotificationdata, remarks);
                        //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotificationdata.PortCode, arrivalnotificationdata.CreatedBy);
                        //wf.Process(imdgarrivalNotificationWorkFlow, "NEW");
                    }
                }
            }


        }
        
        [Test]
        public void ApprovePHArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Approved PH";

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();
                //IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, "DB", 2);
                //wf.Process(arrivalNotificationWorkFlow, "WFSA");

            }


        }

        [Test]
        public void ResubmitPHArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Resubmit PH";


            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();


            foreach (var arrivalnotificationdata in pt)
            {
                var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                           where t.VCN == arrivalnotificationdata.ReferenceID
                                           select t).FirstOrDefault<ArrivalNotification>();

                //IMDGArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new IMDGArrivalNotificationWorkFlow(_unitOfWork, arrivalnotification, remarks);
                //WorkFlowEngine<IMDGArrivalNotificationWorkFlow> wf = new WorkFlowEngine<IMDGArrivalNotificationWorkFlow>(_unitOfWork, "DB", 2);
                //wf.Process(arrivalNotificationWorkFlow, "VRES");

            }


        }

        [Test]
        public void ModifyArrivalNotification_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ArrivalNotificationVO anvo = new ArrivalNotificationVO();

            string remarks = "Verify Updated";

            var arrivalnotification = (from t in _unitOfWork.Repository<ArrivalNotification>().Query().Include(t => t.Vessel).Include(t => t.ArrivalCommodities).Include(t => t.ArrivalIMDGTankers).Select()
                                       select t).ToList<ArrivalNotification>();
            foreach (var arrivalnotificationdata in arrivalnotification)
            {
                int count = GetRequestStatus(_entityCode, arrivalnotificationdata.VCN);
                if (count == 0)
                {


                    var step = (from wftask in _unitOfWork.Repository<WorkflowTask>().Query().Select()
                                join wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                on wftask.WorkflowTaskCode equals wfi.WorkflowTaskCode
                                join e in _unitOfWork.Repository<Entity>().Query().Select()
                                on wftask.EntityID equals e.EntityID
                                where e.EntityCode == _entityCode && wfi.ReferenceID == arrivalnotificationdata.VCN
                                select wftask).FirstOrDefault<WorkflowTask>();
                    if (step.WorkflowTaskCode != "NEW")
                    {
                        var taskcode = (from wftask in _unitOfWork.Repository<WorkflowTask>().Query().Select()
                                        join e in _unitOfWork.Repository<Entity>().Query().Select()
                                         on wftask.EntityID equals e.EntityID
                                        where e.EntityCode == _entityCode &&
                                        wftask.Step == step.NextStep
                                        select wftask).FirstOrDefault<WorkflowTask>();
                        ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalnotificationdata, remarks);
                        WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotificationdata.PortCode, 1);
                        wf.Process(arrivalNotificationWorkFlow, taskcode.WorkflowTaskCode);
                    }
                    else
                    {
                        ArrivalNotificationWorkFlow arrivalNotificationWorkFlow = new ArrivalNotificationWorkFlow(_unitOfWork, arrivalnotificationdata, remarks);
                        WorkFlowEngine<ArrivalNotificationWorkFlow> wf = new WorkFlowEngine<ArrivalNotificationWorkFlow>(_unitOfWork, arrivalnotificationdata.PortCode, 1);
                        wf.Process(arrivalNotificationWorkFlow, step.WorkflowTaskCode);
                    }



                }
            }


        }

        public int GetRequestStatus(string p_entitycode, string p_referenceno)
        {
            var _entitycode = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                               join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on e.EntityID equals w.EntityID
                               join sc in _unitOfWork.Repository<SubCategory>().Query().Select() on w.WorkflowTaskCode equals sc.SubCatCode
                               join pc in _unitOfWork.Repository<PortConfiguration>().Query().Select() on new { taskcode = w.WorkflowTaskCode, portcode = w.PortCode } equals new { taskcode = pc.ApproveCode, portcode = pc.PortCode }


                               where e.EntityCode == p_entitycode
                                 && w.ReferenceID == p_referenceno

                               select w).Count();

            return _entitycode;
        }


       


        [Test]
        public void GenerateVCN()
        {
            ArrivalNotificationService service = new ArrivalNotificationService(new UnitOfWork(new TnpaContext()));
            //string vcnno = service.GenerateVCN("VCN","DB");
        }

        [Test]
        public void SqlQueryWithElementTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork (new TnpaContext ()))
            {
                var ports = unitOfWork.SqlQuery<Port>("select * from port").ToList();
            }
        }

        [Test]
        public void SqlCommandTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                var ports = unitOfWork.ExecuteSqlCommand("update users  set firstname =  @p0 where UserID = @p1", "bp3","3");
            }
        }

        [Test]
        public void SqlStoredProcTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                var ports = unitOfWork.SqlQuery<PortN>("dbo.GetPortsWithAdditionalColumn @p0", "CT").ToList();
            }
        }

        [Test]
        public void SqlStoredProcTestPendingTask()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                var portcode = new SqlParameter("@portcode", "DB");
                var userid = new SqlParameter("@userid", 2);

                var pendingtask = unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();
            }
        }
    }

    public class PortN
    { 
        public string PortCode {get;set;}
        public string PortName {get;set;}
    }
}
