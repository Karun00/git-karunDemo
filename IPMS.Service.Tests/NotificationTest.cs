using System.Collections.Generic;
using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using NUnit.Framework;

namespace IPMS.Service.Tests
{
    public class NotificationTest
    {
        [TestFixture]
        public class ServiceRequestServiceTest
        {
            protected IDataContextAsync context;
            protected IUnitOfWork unitOfWork;
            private ISupplymentaryServiceRepository _supplymentaryServiceRepository;

            private INotificationPublisher _notificationpublisher;


            [SetUp]
            public void Init()
            {
                context = new TnpaContext();
                unitOfWork = new UnitOfWork(context);
                _notificationpublisher = new NotificationPublisher(unitOfWork);
            }

            [Test]
            public void MessageQueueNotificationTest()
            {
                int noofrecords = 1;
                for (int i = 0; i < noofrecords; i++)
                {
                    //Template for New Service Request
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = UserType.Agent;
                    nextStepCompany.UserTypeId = 94;
                    _notificationpublisher.Publish(42, "6523", 683, nextStepCompany, "CT", WFStatus.NewRequest);

                    //Template for New User Registration Request
                    nextStepCompany.UserType = UserType.Agent;
                    nextStepCompany.UserTypeId = 109;
                    _notificationpublisher.Publish(16, "35", 683, nextStepCompany, "CT", WFStatus.Approved);
                }

            }

            [Test]
            public void ServiceRequestNotification()
            {
                int noofrecords = 1;
                for (int i = 0; i < noofrecords; i++)
               { 
                    //Template for New Service Request
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = UserType.Agent;
                    nextStepCompany.UserTypeId = 51;
                    _notificationpublisher.Publish(42, "16785", 351, nextStepCompany, "CT", WFStatus.NewRequest);
                   //// Publish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)

                    ////Template for Approve Service Request
                    //nextStepCompany.UserType = UserType.Agent;
                    //nextStepCompany.UserTypeId = 51;
                    //_notificationpublisher.Publish(42, "16774", 351, nextStepCompany, "CT", WFStatus.Approved);

                    //////Template for Reject Service Request
                    //nextStepCompany.UserType = UserType.Agent;
                    //nextStepCompany.UserTypeId = 51;
                    //_notificationpublisher.Publish(42, "16193", 351, nextStepCompany, "CT", WFStatus.Reject);
                }

            }

            [Test]
            public void VesselNotification()
            {
                int noofrecords = 1;
                for (int i = 0; i < noofrecords; i++)
                {
                    //Template for New Service Request
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = UserType.Employee;
                    nextStepCompany.UserTypeId = 2;
                    _notificationpublisher.Publish(33, "87564", 2, nextStepCompany, "CT", WFStatus.NewRequest);
                    //// Publish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)

                  
                }

            }

            [Test]
            public void ChangeAgentNotification()
            {
                int noofrecords = 1;
                for (int i = 0; i < noofrecords; i++)
                {
                    //Template for New Service Request
                    CompanyVO nextStepCompany = new CompanyVO();
                    nextStepCompany.UserType = UserType.Employee;
                    nextStepCompany.UserTypeId = 2;
                    _notificationpublisher.Publish(41, "31", 2, nextStepCompany, "DB", WFStatus.NewRequest);
                    //// Publish(int entityId, string reference, int userid, CompanyVO company, string portcode, string workFlowTaskCode)


                }

            }

            [Test]
            public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList()
            {
                _supplymentaryServiceRepository = new SupplymentaryServiceRepository(unitOfWork);
                var suppDetails = _supplymentaryServiceRepository.GetSupplymentaryServiceRequestList("CT", 51, 0, 0, "2017-06-12", "2017-08-11", "All", "All");
                return suppDetails;
            }


        }
    }
}
