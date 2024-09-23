using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetSubCategories(string SupCatCode);

    }
}
