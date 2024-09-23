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

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class AuditLogServiceTest
    {
        protected IAuditLogService auditlogService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        AuditTrailConfig newAuditTrailConfig;
        AuditTrail newAuditTrail;
        User newUser;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            auditlogService = new AuditLogService(unitOfWork);

            newAuditTrailConfig = new AuditTrailConfig
            {
                AuditTrailConfigID = 1,
                ControlerName = "AgentRegistration",
                ActionName = "AgentDetails",
                IsAuditTrailRequired = "Y",
                UserFriendlyDescription = "User has logged into agent registration page"
            };

            newAuditTrail = new AuditTrail
            {
                AuditTrailID = 1,
                AuditTrailConfigID = 1,
                EntryORExit = "Entry",
                UserID = 1
            };

            newUser = new User
            {
                UserID = 1,
                UserName = "admin"
            };

            unitOfWork.Repository<AuditTrailConfig>().Insert(newAuditTrailConfig);
            unitOfWork.Repository<AuditTrail>().Insert(newAuditTrail);
            unitOfWork.Repository<User>().Insert(newUser);

            unitOfWork.SaveChanges();
        }



        [Test]
        public void GetAuditLogsTest()
        {
            var actualResult = auditlogService.GetAuditLogs(null,null,null,null,null);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(1, actualResult[0].AuditTrailID);
            Assert.AreEqual("admin", actualResult[0].UserName);
            Assert.AreEqual("AgentRegistration", actualResult[0].ControlerName);
        }
    }
}
