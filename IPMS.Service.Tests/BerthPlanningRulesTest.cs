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
using IPMS.Domain.ValueObjects;
using IPMS.Services.WorkFlow;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;

namespace IPMS.Service.Tests
{
    [TestFixture]
    public class BerthPlanningRulesTest
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
        public void BerthingRules() { 
        
        // VCN is passed suitable berths 
            string VCN = "";
        // Get Vessel Information from VCN

        
        
        
        }



        public class BerthInformation
        {
            public decimal Length;
            public decimal Draft;            
            public List<String> CargoTypes;

        
        
        
        }


        public class VesselInformation { 
        
       
        
        
        }


    }
}
