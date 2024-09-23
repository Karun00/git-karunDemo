using System;
using Core.Repository.Providers.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IUserPreferenceService
    {
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserPreferenceVO> GetUserPreferenceDetails();
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserPreferenceVO> GetUserPreferenceDetailsByUser();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        UserPreferenceVO AddUserPreference(UserPreferenceVO data);
    }
}
