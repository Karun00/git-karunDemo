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
    public class ServiceRequestServiceTest
    {
        protected IServiceRequestService serviceRequestService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        ServiceRequest newRequest;
        VesselCall vcall;

        Port newPort;
        Quay newQuay;
        Berth newBerth;
        SubCategory newSubCategory;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            serviceRequestService = new ServiceRequestService(unitOfWork);

       vcall = new VesselCall
         {
          VesselCallID=	4,
          VCN=	"VCN201400004",
          RecentAgentID=2,
          ETA=DateTime.Now,
          ETD=DateTime.Now,
          ETB=	null,
          ETUB=	null,
          ATA=	null,
          ATD=	null,
          ATB=	null,
          ATUB=	null,
          BreakWaterIn=	null,
          BreakWaterOut=null,
          PortLimitIn=null,
          PortLimitOut=	null,
          AnchorUp=	null,
          AnchorDown=null,
          FromPositionPortCode="DB",
          FromPositionQuayCode="BH",
          FromPositionBerthCode="DRJ",
          FromPositionBollardCode=null,
          ToPositionPortCode=null,
          ToPositionQuayCode=null,
          ToPositionBerthCode=null,
          ToPositionBollardCode=null,
          RecordStatus="A",
          CreatedBy=2,
          CreatedDate=DateTime.Now,
          ModifiedBy=2,
          ModifiedDate=DateTime.Now
         };

            newPort = new Port
            {
                PortCode = "DB",
                PortName = "Durban"
            };
            newQuay = new Quay
            {
                QuayCode = "BH",
                QuayName = "BHQuay"
            };
            newBerth = new Berth
            {

                BerthCode = "DRJ",
                BerthName = "DRJBerth"
            };

            unitOfWork.Repository<Port>().Insert(newPort);
            unitOfWork.Repository<Quay>().Insert(newQuay);
           unitOfWork.Repository<Berth>().Insert(newBerth);                   
           unitOfWork.Repository<VesselCall>().Insert(vcall);
       

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
