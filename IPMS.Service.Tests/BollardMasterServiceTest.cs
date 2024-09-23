using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
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
    public class BollardMasterServiceTest
    {
        protected IBollardService bollardnService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        Berth newBerth;
        Bollard newBollard;
        Port newPort;
        Quay newQuay;
       // BollardMasterDetails newBollardMaster;
        

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            bollardnService = new BollardService(unitOfWork);
           
            newBollard = new Bollard
            {
                PortCode = "p",
                QuayCode = "b",
                BerthCode = "101PMB",
                BollardCode="BCD",
                BollardName="BoldName",
                ShortName="Bchil",
                FromMeter=100,
                ToMeter=150,
                Continuous="A",
                Description="oks",
                RecordStatus="A",
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                ModifiedBy = 1,
                ModifiedDate = DateTime.Now
            };

            newBerth = new Berth
                {

                    BerthCode = "101PMB",
                    BerthName = "BerthBay",
                    BerthType="S",
                    CreatedBy = 1,
                    CreatedDate=DateTime.Now,
                    ModifiedBy=1,
                    ModifiedDate=DateTime.Now
                };
            newPort = new Port
            {
                PortCode = "p",
                PortName = "PortofBay"
            };
            newQuay = new Quay
            {
                QuayCode = "b",
                QuayName = "q"
            };

            unitOfWork.Repository<Bollard>().Insert(newBollard);
           unitOfWork.Repository<Berth>().Insert(newBerth);
           unitOfWork.Repository<Port>().Insert(newPort);
           unitOfWork.Repository<Quay>().Insert(newQuay);

       

            unitOfWork.SaveChanges();
        }

       

        //[Test]
        //public void SaveBollardTest()
        //{
        //    var actualResult = bollardnService.AddBollard(newBollard);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("BoldName", actualResult.BollardName);
        //}
        //[Test]
        //public void UpdateBerthTest()
        //{
        //    newBollard.BollardName = "BoldName";
        //    var actualResult = bollardnService.ModifyBollard(newBollard);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("BoldName", actualResult.BollardName);
        //}
        //[Test]
        //public void BollardMasterDetailsTest()
        //{
        //    var actualResult = bollardnService.GetBollardDetails();
        //    var expectedResult = 1;
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(expectedResult, actualResult.Count());
        //}

    }
}
