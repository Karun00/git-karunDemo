using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.ValueObjects;
using IPMS.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class BiztalkAnchorageResponseServiceTest
    {
        protected IDataContextAsync context;
        protected IUnitOfWork _unitOfWork;
        // private const string _entityCode = EntityCodes.Service_Request;
        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            _unitOfWork = new UnitOfWork(context);

        }
        [Test]
        public void Create_BiztalkAnchorageResponse()
        {
            List<AnchorageDataVO> anchorageDatalst = new List<AnchorageDataVO>();
            AnchorageDataVO anchorageData = new AnchorageDataVO();
            
            BiztalkAnchorageResponseService BTS = new BiztalkAnchorageResponseService();
            anchorageData.PortId = "ZADUR";
            anchorageData.MMSI = "357382000";
            anchorageData.Area = "";
            anchorageData.Assignment = "Anchored";
            anchorageData.DateTime ="1/30/2015 9:20:26 AM";
            anchorageData.IMONo = "9395733";
            anchorageData.CallSign = "3FFY6";
            anchorageData.VesselName = "ANUKE ATMBER";
            anchorageDatalst.Add(anchorageData);

            List<AnchorageDataVO> add_AnchorageData = BTS.BiztalkAnchorageResponse(anchorageDatalst);
        }
    }
}
