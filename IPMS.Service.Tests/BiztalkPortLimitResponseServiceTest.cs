using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.ValueObjects;
using IPMS.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace IPMS.Service.Tests
{
    public class BiztalkPortLimitResponseServiceTest
    {
        protected TnpaContext context;
        protected IUnitOfWork _unitOfWork;
        [SetUp]
        public void Init()
        {
            context = new TnpaContext();
            _unitOfWork = new UnitOfWork(context);
        }
        [Test]
        public void Create_BiztalkPortLimitResponse()
        {

            PortLimitDataVO portLimitData = new PortLimitDataVO();
            List<PortLimitDataVO> portLimitDatalst = new List<PortLimitDataVO>();
            BiztalkPortLimitResponseService BPL = new BiztalkPortLimitResponseService();
            portLimitData.PortId = "ZADUR";
            portLimitData.mmsi = "357191000";
            portLimitData.area = "PortLimits";
            portLimitData.movement = "P_Arrival";
            portLimitData.datetime = "1/30/2015 9:19:17 AM";
            portLimitData.IMONo = "9051492";
            portLimitData.callsign = "H8MU";
            portLimitData.name = "MSC NILGUN";

            portLimitDatalst.Add(portLimitData);
            List<PortLimitDataVO> add_portLimitData = BPL.BiztalkPortLimitResponse(portLimitDatalst);
        }
    }
}
