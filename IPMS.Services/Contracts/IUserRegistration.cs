using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Services
{
    [ServiceContract]
    interface IUserRegistration
    {

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<UserMasterVO> GetEmployeeDetails();

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<UserMasterVO> GetAgentDetails();

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //List<UserMasterVO> GetTerminalOperatorDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User AddUser(User userData);
    }
}
