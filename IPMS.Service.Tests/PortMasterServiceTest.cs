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
    public class PortMasterServiceTest
    {
        protected IPortService portnService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        Port newPort;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            portnService = new PortService(unitOfWork);

            newPort = new Port
            {
                PortCode = "p",
                PortName = "PortofBay",
                InternationalCharacter = "IND",
                GeographicLocation = "loc",
                RecordStatus = "A",
                ContactNo = "9090909090",
                Fax = "9090909898",
                Email = "ma@gmail.com"
            };

            unitOfWork.Repository<Port>().Insert(newPort);

            unitOfWork.SaveChanges();
        }



        [Test]
        public void SavePortTest()
        {
            var actualResult = portnService.AddPort(newPort);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual("PortofBay", actualResult.PortName);
            //  Assert.AreEqual(200, actualResult.LengthM);
        }
        [Test]
        public void UpdatePortTest()
        {
            newPort.PortName = "Port of SB";
            var actualResult = portnService.ModifyPort(newPort);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual("Port of SB", actualResult.PortName);
            //  Assert.AreEqual(200, actualResult.LengthM);
        }
        [Test]
        public void BerthMasterDetailsTest()
        {
            var actualResult = portnService.GetPorts();
            var expectedResult = 1;
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult.Count());
        }

    }
}
