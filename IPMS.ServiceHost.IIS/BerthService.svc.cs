using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IPMS.Domain.Models;
using IPMS.Services;

namespace IPMS.ServiceHost.IIS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BerthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BerthService.svc or BerthService.svc.cs at the Solution Explorer and start debugging.
 
 public class BerthService :IBerthService
    {
        public List<BerthMasterDetails> GetBerthsDetails()
        {
            return GetBerthsDetails();
        }

        public Berth AddBerth(Berth berthdata)
        {
            return AddBerth(berthdata);
        }

        public Berth ModifyBerth(Berth berthdata)
        {
            return ModifyBerth(berthdata);
        }

        public List<SubCategory> GetBerthType()
        {
            return GetBerthType();
        }

        public Berth DelBerthByID(Berth berthdata)
        {
            return DelBerthByID(berthdata);
        }

        public List<Port> GetPortQuayDetails()
        {
            return GetPortQuayDetails();
        }
        
       
    }
}
