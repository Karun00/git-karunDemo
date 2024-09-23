using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
   public interface IPortConfigurationRepository
    {
        string GetWFApprovedCode(string portcode);
        PortConfiguration GetPortConfiguration(string portcode);
    }
}
