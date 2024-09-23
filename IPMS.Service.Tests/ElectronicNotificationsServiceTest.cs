using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using IPMS.Notifications;
using IPMS.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPMS.Service.Tests
{
    [TestFixture]
    public class ElectronicNotificationsServiceTest
    {
        protected IElectronicNotificationsService electronicNotificationsService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        Notification notification;
        NotificationRole notificationRole;
        NotificationTemplate notificationTemplate;
        Role role;
        Entity entity;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            electronicNotificationsService = new ElectronicNotificationsService(unitOfWork);

            notification = new Notification
            {
                NotificationTemplateCode = "TEST",
                NotificationId = 1,
                RecordStatus = "A"
            };

            List<NotificationRole> objnotificationRole = new List<NotificationRole> { 
                   new NotificationRole(){  NotificationRoleID = 1, NotificationTemplateCode="TEST", RecordStatus="A", RoleID=1, }
            };

            notificationTemplate = new NotificationTemplate
            {
                NotificationTemplateCode = "TEST",
                NotificationTemplateName = "Test",
                RecordStatus = "A",
                EntityID = 1,
                NotificationRoles = objnotificationRole
            };

            notificationRole = new NotificationRole
            {
                NotificationTemplateCode = "TEST",
                NotificationRoleID = 1,
                RoleID = 1,
                RecordStatus = "A"
            };

            role = new Role
            {
                RoleID = 1,
                RoleName = "ADMIN",
                RecordStatus = "A"
            };

            entity = new Entity
            {
                EntityID = 1,
                EntityName = "ArrivalNotification",
                EntityCode = "AR",
                RecordStatus = "A"
            };

            unitOfWork.Repository<NotificationTemplate>().Insert(notificationTemplate);
            unitOfWork.Repository<NotificationRole>().Insert(notificationRole);
            unitOfWork.Repository<Notification>().Insert(notification);
            unitOfWork.Repository<Role>().Insert(role);
            unitOfWork.Repository<Entity>().Insert(entity);

            unitOfWork.SaveChanges();
        }

        [Test]
        public void GetRolesDetailsTest()
        {
            var actualResult = electronicNotificationsService.GetRolesDetails();
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult[0].RoleID);
            Assert.AreEqual("ADMIN", actualResult[0].RoleName);
        }

        [Test]
        public void GetEntityDetailsTest()
        {
            var actualResult = electronicNotificationsService.GetEntityDetails();
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult[0].EntityID);
            Assert.AreEqual("ArrivalNotification", actualResult[0].EntityName);
        }

        [Test]
        public void GetNotificationsTest()
        {
            var actualResult = electronicNotificationsService.GetNotifications();
            Assert.IsNotNull(actualResult);
            Assert.AreEqual("TEST", actualResult[0].NotificationTemplateCode);
            Assert.AreEqual(1, actualResult[0].Roles.Count);
        }

        [Test]
        public void AddNotificationTest()
        {
            var actualResult = electronicNotificationsService.AddNotification(notificationTemplate);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void ModifyNotificationTest()
        {
            var actualResult = electronicNotificationsService.ModifyNotification(notificationTemplate);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void DeleteNotificationTest()
        {
            var actualResult = electronicNotificationsService.DeleteNotification(notificationTemplate);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult);
        }

        [Test]
        public void Start_Service_Test()
        {
            unitOfWork = new UnitOfWork(new TnpaContext()); //new UnitOfWork(context);
            NotificationEngine ne = new NotificationEngine(unitOfWork);
            ne.Start();

        }
    }
}
