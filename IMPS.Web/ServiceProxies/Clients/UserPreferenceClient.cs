using System.Collections.Generic;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.Models;

namespace IPMS.ServiceProxies.Clients
{
    public class UserPreferenceClient : UserClientBase<IUserPreferenceService>, IUserPreferenceService
    {
        public  List<UserPreferenceVO> GetUserPreferenceDetails()
        {
            return WrapOperationWithException(() => Channel.GetUserPreferenceDetails());
        }
        //public List<UserPreferenceVO> GetUserPreferenceDetailsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetUserPreferenceDetailsAsync());
        //}

        public List<UserPreferenceVO> GetUserPreferenceDetailsByUser()
        {
            return WrapOperationWithException(() => Channel.GetUserPreferenceDetailsByUser());
        }
        //public List<UserPreferenceVO> GetUserPreferenceDetailsByUserAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetUserPreferenceDetailsByUserAsync());
        //}

        public UserPreferenceVO AddUserPreference(UserPreferenceVO data)
        {
            return WrapOperationWithException(() => Channel.AddUserPreference(data));
        }
        //public UserPreferenceVO AddUserPreferenceAsync(UserPreferenceVO data)
        //{
        //    return WrapOperationWithException(() => Channel.AddUserPreferenceAsync(data));
        //}

    }
}