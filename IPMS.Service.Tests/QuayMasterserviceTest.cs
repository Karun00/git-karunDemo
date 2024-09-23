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
    class QuayMasterserviceTest
    {
        protected IQuayService quayService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;


        Quay newQuay;
        //QuayMasterDetails newQuayMaster;
        Port newPort;


        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            quayService = new QuayService(unitOfWork);
            newPort = new Port
                {
                 
                    PortCode = "RB",
                    PortName = "Richards Bay",
                    RecordStatus = "A",
                    CreatedBy = 1
                };
            newQuay = new Quay
            {
                PortCode = "RB",
                QuayCode = "Q102",
                QuayName = "quay",
                ShortName = "q2",
                QuayLength = 1002,
                RecordStatus = "A"
            };

            List<QuayMasterDetails> objQuayMasterDetails = new List<QuayMasterDetails>
            {
                new QuayMasterDetails(){QuayCode = "Q102",QuayName = "quay",ShortName = "q2",PortName = "Richards Bay",QuayLength = 1002,RecordStatus ="A",CreatedBy=1,}
            };

            unitOfWork.Repository<Quay>().Insert(newQuay);
            unitOfWork.Repository<Port>().Insert(newPort);
           // unitOfWork.Repository<QuayMasterDetails>().Insert(newQuayMaster);
            unitOfWork.SaveChanges();


        }


        //[Test]
        //public void AddQuayTest()
        //{
        //    var actualResult = quayService.ModifyQuay(newQuay);
        //    var expectedResult = 0;
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("Q102", actualResult.QuayCode);
        //}
        //[Test]
        //public void UpdateQuayTest()
        //{
        //    newQuay.QuayName = "b";
        //    var actualResult = quayService.ModifyQuay(newQuay);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("b", actualResult.QuayName);
        //    //  Assert.AreEqual(200, actualResult.LengthM);
        //}
        //[Test]
        //public void QuayDetailsTest()
        //{
        //    var actualResult = quayService.QuayDetails();
        //   //  var expectedResult = 0;
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(0, actualResult.Count());
        //}
        //[Test]
        //public void ModifyQuayTest()
        //{
        //    newQuay.QuayName = "quay";
        //    var actualResult = quayService.AddQuay(newQuay);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("quay", actualResult.QuayName);
        //    //  Assert.AreEqual(200, actualResult.LengthM);
        //}
    }
}
       