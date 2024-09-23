using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IModuleService 
   {
       [OperationContract]
       [FaultContract(typeof(Exception))]
        List<ModuleVO> GetModules();

       [OperationContract]
       [FaultContract(typeof(Exception))]
       ModuleVO PostModuleData(ModuleVO moduleData);

          [OperationContract]
       [FaultContract(typeof(Exception))]
       List<ModuleVO> GetParentModules();

          [OperationContract]
          [FaultContract(typeof(Exception))]
          ModuleVO ModifyModule(ModuleVO moduleData);


          [OperationContract]
          [FaultContract(typeof(Exception))]
          IEnumerable<UserRole> GetUserRoles(string username);


   }
}
