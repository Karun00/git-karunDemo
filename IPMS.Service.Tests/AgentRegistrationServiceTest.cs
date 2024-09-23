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
    public class AgentRegistrationServiceTest
    {
        protected IAgentService agentnService;
        protected IDataContextAsync context;
        protected IUnitOfWork unitOfWork;

        Port newPort;
        // ApplicantPortWorkflow newApplPortWorkFlow;
        Agent newAgent;
        AgentPort newAgentPort;
        AuthorizedContactPerson newAuthorizedContactPerson;
        Address newAddress;
         AgentDocument newAgentDocument;

        [SetUp]
        public void Init()
        {
            context = new IPMSFakeContext();
            unitOfWork = new UnitOfWork(context);
            agentnService = new AgentService(unitOfWork);
            newPort = new Port
                {
                    //  PortID = 1,
                    PortCode = "101PMB",
                    PortName = "Port of SaldanhaBay",
                    RecordStatus = "A",
                    CreatedBy = 1
                };
            newAgentPort = new AgentPort
            {
                AgentID = 0,
                PortCode = "101PMB",
                WFStatus = "N",
                VerifiedBy = 1,
                ApprovedBy = 1,
              //  RejectedBy = 1,
                RejectComments = "Test",
               // ReasonID = null

            };
            List<AgentPort> objAgentPort = new List<AgentPort>
            {
                new AgentPort(){AgentID = 0,PortCode = "101PMB",WFStatus = "A",VerifiedBy = 1,ApprovedBy = 1,RejectComments = "Test"}
            };

            List<AgentDocument> objAgentDocument = new List<AgentDocument>
            {
                new AgentDocument(){  AgentID = 0, CreatedBy = 2}
            };
            List<Address> objAddress = new List<Address>
            {
                new Address(){ AddressID = 0 , RecordStatus="N"}
            };
            List<AuthorizedContactPerson> objAuthorizedContactPerson = new List<AuthorizedContactPerson>
            {
                new AuthorizedContactPerson(){ AuthorizedContactPersonID = 0, Designation = "SE"}
            };

            newAgent = new Agent
             {
                 AgentID = 0,
                 RegisteredName = "Mariah",
              //   RegnNo = "Reg001",
                 VATNumber = "Vat001",
                 IncomeTaxNumber = "It001",
                 RecordStatus = "A",
                 CreatedBy = 1,
                 ModifiedBy = 1,
               //  AgentPort = objAgentPort,
                //AgentDocument = objAgentDocument,
                // AuthorizedContactPerson = objAuthorizedContactPerson,
                //Address = objAddress
             };

            newAuthorizedContactPerson = new AuthorizedContactPerson
            {
                AuthorizedContactPersonID = 1,
               // AgentID = 0,
                RecordStatus = "N",
                FirstName = "Test",
                SurName = "Test",
              //  MiddleName = "Test",
                EmailID = "test@navayuga.co.in"

            };

            newAddress = new Address
            {
                AddressID = 0,
                RecordStatus = "N",
                AddressType = "1",
                PostalCode = "55555"
            };

            newAgentDocument = new AgentDocument
            {
                DocumentID = 1,
                AgentID = 0,
                RecordStatus = "N",
                CreatedBy = 1,
              //  DocuType = 2

            };

            unitOfWork.Repository<Port>().Insert(newPort);
            unitOfWork.Repository<AgentPort>().Insert(newAgentPort);
            unitOfWork.Repository<Agent>().Insert(newAgent);
            unitOfWork.Repository<AuthorizedContactPerson>().Insert(newAuthorizedContactPerson);
            unitOfWork.Repository<Address>().Insert(newAddress);
            unitOfWork.Repository<AgentDocument>().Insert(newAgentDocument);

            unitOfWork.SaveChanges();
        }



        [Test]
        public void RegisterAgentTest()
        {
            var actualResult = agentnService.RegisterAgent(newAgent);
            //var expectedResult = "A";
            newAgent.RegisteredName = "Mariah";
            Assert.IsNotNull(actualResult);
           // Assert.AreEqual("Mariah", actualResult.RegisteredName);
          //  Assert.AreEqual(expectedResult, actualResult.Address.FirstOrDefault().Status);
        }


        [Test]
        public void GetAgentsTest()
        {
            var actualResult = agentnService.GetAgents("A");
           // var expectedResult = 0;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(0, actualResult[0].AgentID);
        }

        [Test]
        public void GetAgentTest()
        {
            var actualResult = agentnService.GetAgent(0);
            var expectedResult = 0;

            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult.AgentID);
            //Assert.AreEqual(1, actualResult.AgentDocument.FirstOrDefault().DocuType);
           // Assert.AreEqual("test@navayuga.co.in", actualResult.AuthorizedContactPerson.FirstOrDefault().Designation);
            //Assert.AreEqual("55555", actualResult.Address.FirstOrDefault().PostalCode);
        }

        //[Test]
        //public void PutRejectAgent()
        //{
        //    var actualResult = agentnService.PutRejectAgent(newAgent);

        //    Assert.IsNotNull(actualResult);
        //    Assert.AreEqual(0, actualResult[0].RecordStatus);

        //}


    }
}
