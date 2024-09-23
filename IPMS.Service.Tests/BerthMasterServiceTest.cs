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
    public class BerthMasterServiceTest
    {
        protected IBerthService berthnService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        Berth newBerth;
        //BerthMasterDetails newBerthMaster;
        Port newPort;
        Quay newQuay;
        SubCategory newSubCategory;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            berthnService = new BerthService(unitOfWork);


            newBerth = new Berth
                {
                    //BerthID = 1,
                    PortCode="p",
                    BerthCode = "101PMB",
                    BerthName = "BerthBay",
                    ShortName="SBS",
                 //   PortID=1,
                  //  QuayID=1,
                    BerthType="S",
                    QuayCode = "b",
                    FromMeter=100,
                    ToMeter=300,
                  //  LengthM=200,
                  //  DraftM=100,
                  //  Status = "A",
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
            newSubCategory = new SubCategory
            {

                SubCatCode = "S",
                SubCatName = "Berthtypes"
            };

           unitOfWork.Repository<Berth>().Insert(newBerth);
           unitOfWork.Repository<Port>().Insert(newPort);
           unitOfWork.Repository<Quay>().Insert(newQuay);
           unitOfWork.Repository<SubCategory>().Insert(newSubCategory);
       

            unitOfWork.SaveChanges();
        }

       

        //[Test]
        //public void SaveBerthTest()
        //{
        //    var actualResult = berthnService.AddBerth(newBerth);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("BerthBay", actualResult.BerthName);
        //  //  Assert.AreEqual(200, actualResult.LengthM);
        //}
        //[Test]
        //public void UpdateBerthTest()
        //{
        //    newBerth.BerthName = "Port of SB";
        //    var actualResult = berthnService.ModifyBerth(newBerth);
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual("Port of SB", actualResult.BerthName);
        //}
        //[Test]
        //public void BerthMasterDetailsTest()
        //{
        //    var actualResult = berthnService.GetBerthsDetails();
        //    var expectedResult = 1;
        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(expectedResult, actualResult.Count());
        //}

    }
}
