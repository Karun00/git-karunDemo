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
using IPMS.Data.Context;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class AgentServiceTest
    {
        [SetUp]
        public void Init()
        {
        }
        [Test]
        public void Test_CreateAgentWithActualDb()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TnpaContext()))
            {
                Agent newAgent = new Agent();
                newAgent.RegisteredName = "sadfsadf";
                newAgent.RegistrationNumber = "REGNO1";
                newAgent.IncomeTaxNumber = "ITNUMBER";
                newAgent.VATNumber = "VAT111";

                newAgent.AuthorizedContactPerson = new AuthorizedContactPerson();
                newAgent.AuthorizedContactPerson.FirstName = "safsadf";
                newAgent.AuthorizedContactPerson.CellularNo = "1212121";
                newAgent.AuthorizedContactPerson.SurName = "sadfsadf";
                newAgent.AuthorizedContactPerson.IdentityNo = "AASAA";
                newAgent.AuthorizedContactPerson.Designation = "SE";
                newAgent.AuthorizedContactPerson.EmailID = "asdf@asdf.com";
                newAgent.AuthorizedContactPerson.RecordStatus = "A";
                newAgent.AuthorizedContactPerson.CreatedBy = 1;
                newAgent.AuthorizedContactPerson.CreatedDate = DateTime.Now;

                newAgent.BBBEEStatus = "ADPE";

                newAgent.Address = new Address();
                newAgent.Address.NumberStreet = "sadf";
                newAgent.Address.Suburb = "sadfsdf";
                newAgent.Address.TownCity = "sadfsadf";
                newAgent.Address.PostalCode = "232323";
                newAgent.Address.RecordStatus = "A";

                //newAgent.FromDate = DateTime.Now;
                //newAgent.ToDate = DateTime.Now;

                newAgent.Address.CreatedBy = 1;
                newAgent.Address.CreatedDate = DateTime.Now;

                newAgent.CreatedBy = 1;
                newAgent.CreatedDate = DateTime.Now;
                newAgent.QualifyBBBEECodes = "Y";
                newAgent.RecordStatus = "I";
                newAgent.SAASOA = "Y";
                newAgent.SARSTaxClearance = "Y";
                newAgent.SkillsDevLevyNumber = "sadfsafd";
                newAgent.TelephoneNo1 = "2323";
                newAgent.TelephoneNo2 = "3333";
                newAgent.TradingName = "asdfsadf";
                newAgent.VerifyBBBEEStatus = "Y";
                
                /*newAgent.AgentPorts = unitOfWork.Repository<Port>().Query().Select().ToList();

                AgentService service = new AgentService(unitOfWork);
                service.CreateAgent(newAgent);*/
            }
        }
    }
}
