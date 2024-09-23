using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IUserPreferenceService : IDisposable
    {
         

        [OperationContract]
        List<UserPreferenceVO> GetUserPreferenceDetails();

        //[OperationContract]
        //List<UserPreferenceVO> GetUserPreferenceDetailsAsync();

        [OperationContract]
        List<UserPreferenceVO> GetUserPreferenceDetailsByUser();

        //[OperationContract]
        //List<UserPreferenceVO> GetUserPreferenceDetailsByUserAsync();

        [OperationContract]
        UserPreferenceVO AddUserPreference(UserPreferenceVO data);
        
        //[OperationContract]
        //UserPreferenceVO AddUserPreferenceAsync(UserPreferenceVO data);
        

            
    }
}
