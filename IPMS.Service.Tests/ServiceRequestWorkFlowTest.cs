using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.Models;
using IPMS.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Data.Context;
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Globalization;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class ServiceRequestWorkFlowTest
    {

        protected TnpaContext context;
        protected IUnitOfWork _unitOfWork;
        private const string _entityCode = EntityCodes.Service_Request;
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void Create_All_Service_MovementTypes()
        {
            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ServiceRequest_VO serv = new ServiceRequest_VO();

            var _vesselcall = (from t in _unitOfWork.Repository<VesselCall>().Query().Select()
                               where t.ATUB == null && t.RecordStatus == "A"
                               select t).ToList<VesselCall>();

            if (_vesselcall.Count > 0)
            {
                foreach (var vcal in _vesselcall)
                {
                    var servicerequests = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Select()
                                           where t.VCN == vcal.VCN && t.MovementType == MovementTypes.ARRIVAL && t.RecordStatus == "A"
                                           select t).ToList<ServiceRequest>();
                    if (servicerequests.Count() == 0)
                    {
                        ServiceRequestService SRS = new ServiceRequestService();
                        SRS._LoginName = "agent1";
                        SRS._UserId = 2;
                        SRS._PortCode = "DB";
                        serv.VCN = vcal.VCN;
                        serv.MovementDateTime = DateTime.Now.AddHours(-5).ToString();
                        serv.MovementType = MovementTypes.ARRIVAL;
                        serv.SideAlongSideCode = "PSID";
                        serv.NoMainEngine = true;
                        serv.OwnSteam = true;
                        serv.Comments = "Arrival service Request Created";
                        serv.RecordStatus = "A";
                        serv.WorkflowInstanceId = null;
                        ServiceRequest_VO add_ServiceRequest = SRS.AddServiceRequest(serv);
                    }

                }
                foreach (var vcal in _vesselcall)
                {
                    var servicerequests = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Select()
                                           where t.VCN == vcal.VCN && t.MovementType == MovementTypes.SHIFTING && t.RecordStatus == "A"
                                           select t).ToList<ServiceRequest>();
                    if (servicerequests.Count() == 0)
                    {
                        ServiceRequestService SRS = new ServiceRequestService();
                        SRS._LoginName = "agent1";
                        SRS._UserId = 2;
                        SRS._PortCode = "DB";
                        serv.VCN = vcal.VCN;
                        serv.MovementDateTime = DateTime.Now.AddHours(-4).ToString();
                        serv.MovementType = MovementTypes.SHIFTING;
                        serv.SideAlongSideCode = "PSID";
                        serv.NoMainEngine = true;
                        serv.OwnSteam = true;
                        serv.Comments = "Shifting service Request Created";
                        serv.RecordStatus = "A";
                        serv.WorkflowInstanceId = null;

                        ServiceRequestShifting shiftinglist = new ServiceRequestShifting();
                        shiftinglist.DraftFWD = 12;
                        shiftinglist.DraftAFT = 12;
                        shiftinglist.FromPositionBerthCode = "B1";
                        shiftinglist.FromPositionPortCode = "DB";
                        shiftinglist.FromPositionQuayCode = "BF";
                        shiftinglist.FromPositionBollardCode = "BD";
                        shiftinglist.ToPositionPortCode = "DB";
                        shiftinglist.ToPositionQuayCode = "BF";
                        shiftinglist.ToPositionBerthCode = "B1";
                        shiftinglist.ToPositionBollardCode = "BD";
                        shiftinglist.ToPortCode = "DB";
                        shiftinglist.ToQuayCode = "BF";
                        shiftinglist.ToBerthCode = "B1";


                        serv.ServiceRequestShifting = shiftinglist.MapToDto();

                        ServiceRequest_VO add_ServiceRequest = SRS.AddServiceRequest(serv);
                    }

                }
                foreach (var vcal in _vesselcall)
                {
                    var servicerequests = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Select()
                                           where t.VCN == vcal.VCN && t.MovementType == MovementTypes.WARPING && t.RecordStatus == "A"
                                           select t).ToList<ServiceRequest>();
                    if (servicerequests.Count() == 0)
                    {
                        ServiceRequestService SRS = new ServiceRequestService();
                        SRS._LoginName = "agent1";
                        SRS._UserId = 2;
                        SRS._PortCode = "DB";
                        serv.VCN = vcal.VCN;
                   serv.MovementDateTime = DateTime.Now.AddHours(-3).ToString();
                        serv.MovementType = MovementTypes.WARPING;
                        serv.SideAlongSideCode = "PSID";
                        serv.NoMainEngine = true;
                        serv.OwnSteam = true;
                        serv.Comments = "Warping service Request Created";
                        serv.RecordStatus = "A";
                        serv.WorkflowInstanceId = null;

                        ServiceRequestWarping warpinglist = new ServiceRequestWarping();

                        warpinglist.FromPositionBerthCode = "B1";
                        warpinglist.FromPositionPortCode = "DB";
                        warpinglist.FromPositionQuayCode = "BF";
                        warpinglist.FromPositionBollardCode = "BD";
                        warpinglist.ToPositionPortCode = "DB";
                        warpinglist.ToPositionQuayCode = "BF";
                        warpinglist.ToPositionBerthCode = "B1";
                        warpinglist.ToPositionBollardCode = "BD";

                        serv.ServiceRequestWarping = warpinglist.MapToDto();

                        ServiceRequest_VO add_ServiceRequest = SRS.AddServiceRequest(serv);
                    }

                }
                foreach (var vcal in _vesselcall)
                {
                    var servicerequests = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Select()
                                           where t.VCN == vcal.VCN && t.MovementType == MovementTypes.SAILING && t.RecordStatus == "A"
                                           select t).ToList<ServiceRequest>();
                    if (servicerequests.Count() == 0)
                    {
                        ServiceRequestService SRS = new ServiceRequestService();
                        SRS._LoginName = "agent1";
                        SRS._UserId = 2;
                        SRS._PortCode = "DB";
                        serv.VCN = vcal.VCN;
                        serv.MovementDateTime = DateTime.Now.AddHours(-2).ToString();
                        serv.MovementType = MovementTypes.SAILING;
                        serv.SideAlongSideCode = "PSID";
                        serv.NoMainEngine = true;
                        serv.OwnSteam = true;
                        serv.Comments = "Sailing service Request Created";
                        serv.RecordStatus = "A";
                        serv.WorkflowInstanceId = null;

                        ServiceRequestSailing sailinglist = new ServiceRequestSailing();

                        Document document = new Document();

                        document.RecordStatus = "A";
                        document.DocumentName = "DOC1";
                        document.DocumentPath = "DOC1";
                        document.DocumentType = "DOC1";
                        document.Data = null;
                        document.FileName = "DOC1";
                        document.CreatedBy = 1;
                        document.CreatedDate = DateTime.Now;
                        document.ModifiedBy = 1;
                        document.ModifiedDate = DateTime.Now;


                        _unitOfWork.Repository<Document>().Insert(document);
                        _unitOfWork.SaveChanges();

                        sailinglist.DocumentID = document.DocumentID;
                        sailinglist.MarineRevenueCleared = "True";

                        sailinglist.Document = null;

                        serv.ServiceRequestSailing = sailinglist.MapToDto();

                        ServiceRequest_VO add_ServiceRequest = SRS.AddServiceRequest(serv);
                    }

                }
            }
        }

        [Test]
        public void CreateServiceRequest_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            ServiceRequest_VO serv = new ServiceRequest_VO();

            string remarks = "";

            var servicerequest = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                  select t).ToList<ServiceRequest>();

            if (servicerequest.Count == 0)
            {
                // serv.ServiceRequestID = 6;
                serv.VCN = "VCN201400003";
                serv.MovementDateTime = DateTime.Now.ToString();
                serv.MovementType = "ARMV";
                serv.SideAlongSideCode = "PSID";
                serv.NoMainEngine = true;
                serv.OwnSteam = true;
                serv.Comments = "Arrival service Request Created";
                serv.RecordStatus = "A";
                serv.CreatedBy = 2;
                serv.CreatedDate = DateTime.Now;
                serv.ModifiedBy = 2;
                serv.ModifiedDate = DateTime.Now;
                serv.WorkflowInstanceId = null;


                ServiceRequestService service = new ServiceRequestService(new UnitOfWork(new TnpaContext()));
                //   service.AddServiceRequest(serv);
                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == Convert.ToString(serv.ServiceRequestID, CultureInfo.InvariantCulture)
                               select wfi).ToList();
                if (wfidata.Count == 0)
                {
                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, serv.MapToEntity(), remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, "DB", 2);
                    wf.Process(servicerequestWorkFlow, "NEW");
                }

            }
            else
            {
                foreach (var servs in servicerequest)
                {
                    var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                                   where wfi.ReferenceID == Convert.ToString(servs.ServiceRequestID, CultureInfo.InvariantCulture)
                                   select wfi).ToList();
                    if (wfidata.Count == 0)
                    {
                        ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servs, remarks);
                        WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>();
                        wf.Process(servicerequestWorkFlow, "NEW");
                    }
                }
            }


        }
        [Test]
        public void ApproveServiceRequest_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Approve";
            ServiceRequest an = new ServiceRequest();

            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 14);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();

            foreach (var servdata in pt)
            {
                an.ServiceRequestID = Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture);

                var servicerequess = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                      where t.ServiceRequestID == Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture) 
                                      select t).FirstOrDefault<ServiceRequest>();
                if (servicerequess != null)
                {
                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicerequess, remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, "DB", 14);
                    wf.Process(servicerequestWorkFlow, "WFSA");
                }
            }
        }
        [Test]
        public void ConfirmServiceRequest_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Conf";
            ServiceRequest an = new ServiceRequest();
            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 2);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();

            foreach (var servdata in pt)
            {
                an.ServiceRequestID = Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture);

                var servicerequest = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                      where t.ServiceRequestID == Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture)
                                      select t).FirstOrDefault<ServiceRequest>();
                if(servicerequest !=null)
                {
                ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicerequest, remarks);
                WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, "DB", 2);
                wf.Process(servicerequestWorkFlow, "WFCO");
            }
            }
        }

        [Test]
        public void RejectServiceRequest_WithWorkFlow()
        {


            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
            string remarks = "Reject";
            ServiceRequest an = new ServiceRequest();
            var portcode = new SqlParameter("@portcode", "DB");
            var userid = new SqlParameter("@userid", 14);

            var pt = _unitOfWork.SqlQuery<PendingTaskVO>("Select * from  dbo.udf_pendingtask (@portcode, @userid)", portcode, userid).ToList();

            foreach (var servdata in pt)
            {
                an.ServiceRequestID = Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture);

                var servicerequest = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                      where t.ServiceRequestID == Convert.ToInt32(servdata.ReferenceID, CultureInfo.InvariantCulture)
                                      select t).FirstOrDefault<ServiceRequest>();
                if (servicerequest != null)
                {
                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servicerequest, remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, "DB", 14);
                    wf.Process(servicerequestWorkFlow, "WFRE");
                }
            }


        }

        [Test]
        public void CancelServiceRequest_WithWorkFlow()
        {

            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);


            string remarks = "";

            var servicerequest = (from t in _unitOfWork.Repository<ServiceRequest>().Query().Include(t => t.ServiceRequestSailings).Include(t => t.ServiceRequestWarpings).Include(t => t.ServiceRequestShiftings).Include(t => t.ServiceRequestShiftings.Select(p => p.Berth)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard)).Include(t => t.ServiceRequestShiftings.Select(p => p.Bollard1)).Include(t => t.ArrivalNotification).Include(t => t.ArrivalNotification.Vessel).Include(t => t.VesselCallMovements).Include(t => t.SubCategory).Include(t => t.SubCategory1).Select()
                                  where t.VCN == "VCN201400003" && t.MovementType == MovementTypes.ARRIVAL && t.RecordStatus == "A"
                                  select t).ToList<ServiceRequest>();

            foreach (var servs in servicerequest)
            {
                var wfidata = (from wfi in _unitOfWork.Repository<WorkflowInstance>().Query().Select()
                               where wfi.ReferenceID == Convert.ToString(servs.ServiceRequestID, CultureInfo.InvariantCulture)
                               select wfi).ToList();
                if (wfidata.Count > 0)
                {
                    ServiceRequestWorkFlow servicerequestWorkFlow = new ServiceRequestWorkFlow(_unitOfWork, servs, remarks);
                    WorkFlowEngine<ServiceRequestWorkFlow> wf = new WorkFlowEngine<ServiceRequestWorkFlow>(_unitOfWork, "DB", 2);
                    wf.Process(servicerequestWorkFlow, "WFCA");
                }
            }

        }

        [Test]
        public void emailtemplate()
        {
            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);

            
            var serviceRequestDetails = (from sr in _unitOfWork.Repository<ServiceRequest>().Query().Select()
                                         where sr.ServiceRequestID == 1
                                         select sr).FirstOrDefault<ServiceRequest>();

            ServiceRequest _serviceRequest = serviceRequestDetails;


            var arrivalDetails = (from a in _unitOfWork.Repository<ArrivalNotification>().Query().Select().AsEnumerable<ArrivalNotification>()
                                  join v in _unitOfWork.Repository<Vessel>().Query().Select().AsEnumerable<Vessel>()
                                  on a.VesselID equals v.VesselID
                                  join pb in _unitOfWork.Repository<Berth>().Query().Select().AsEnumerable<Berth>()
                                  on new { pa = a.PreferredPortCode, pb = a.PreferredQuayCode, pc = a.PreferredBerthCode }
                                  equals new { pa = pb.PortCode, pb = pb.QuayCode, pc = pb.BerthCode }
                                  join ab in _unitOfWork.Repository<Berth>().Query().Select().AsEnumerable<Berth>()
                                  on new { aa = a.AlternatePortCode, bb = a.AlternateQuayCode, cc = a.AlternateBerthCode }
                                  equals new { aa = ab.PortCode, bb = ab.QuayCode, cc = ab.BerthCode }
                                  where a.VCN == serviceRequestDetails.VCN
                                  select new ArrivalNotificationDetails
                                  {
                                      PortCode = a.PortCode,
                                      VesselName = v.VesselName,
                                      PreferredBerthName = pb.BerthName,
                                      AlternateBerthName = ab.BerthName,
                                      VCN = a.VCN,
                                      ETA = a.ETA,
                                      ETD = a.ETD,
                                      IMONo = v.IMONo
                                  }
           ).FirstOrDefault<ArrivalNotificationDetails>();

            ArrivalNotificationDetails _arrivalNotification = arrivalDetails;

            var entitydata = (from e in _unitOfWork.Repository<Entity>().Query().Select()
                              where e.EntityID == 20
                              select e).FirstOrDefault<Entity>();

            Entity entityDetails = entitydata;

            Dictionary<string, string> messageTemplatePlaceHolders = Common.GetTokensDictionary(entityDetails, _serviceRequest);
       
         List<User> usersToBeNotified = (from u in _unitOfWork.Repository<User>().Query().Select()
                                   where u.UserID == 1
                                   select u).ToList<User>();


         NotificationTemplate notificationTemplate = (from nt in _unitOfWork.Repository<NotificationTemplate>().Query().Select()
                                                      where nt.NotificationTemplateCode == "TSRE"
                                                      select nt).FirstOrDefault();

            foreach (User user in usersToBeNotified)
            {

                ProcessEmail(user, messageTemplatePlaceHolders, notificationTemplate);
            }
        
        
        }


        public bool ProcessEmail(User user, Dictionary<string, string> messageTemplatePlaceHolders, NotificationTemplate notificationTemplate)
        {
            try
            {

               
                    string emailBody = notificationTemplate.EmailTemplate;
                    foreach (var placeHoder in messageTemplatePlaceHolders)
                    {
                        emailBody = emailBody.Replace(placeHoder.Key, placeHoder.Value);
                    }
                    emailBody = emailBody.Replace("[UserName]", user.UserName);
                    emailBody = emailBody.Replace("[NAME]", "IPMS ADMIN");
                 

                  
               
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

       



    }
}
