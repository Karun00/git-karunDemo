using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;


namespace IPMS.ServiceProxies.Clients
{
    public class UserClient : UserClientBase<IUserService>, IUserService
    {
        public List<SubCategoryVO> GetUserType()
        {
            return WrapOperationWithException(() => Channel.GetUserType());
        }
        public List<Role> GetRoles()
        {
            return WrapOperationWithException(() => Channel.GetRoles());
        }
        public List<UserMasterVO> GetUsersList()
        {
            return WrapOperationWithException(() => Channel.GetUsersList());
        }
        public int AddUser(User userData)
        {
            return WrapOperationWithException(() => Channel.AddUser(userData));
        }

        public User AddRoles(User userData)
        {
            return WrapOperationWithException(() => Channel.AddRoles(userData));
        }

        public User AddPorts(User userData)
        {
            return WrapOperationWithException(() => Channel.AddPorts(userData));
        }

        public int ModifyUser(User userData, string ipAddress, string machineName)
        {
            return WrapOperationWithException(() => Channel.ModifyUser(userData, ipAddress, machineName));
        }
        public User DeleteUserById(User userData)
        {
            return WrapOperationWithException(() => Channel.DeleteUserById(userData));
        }

        public User GetUserById(int userId)
        {
            return WrapOperationWithException(() => Channel.GetUserById(userId));
        }

        public User GetUserByName(string userName)
        {
            return WrapOperationWithException(() => Channel.GetUserByName(userName));
        }
        public List<UserMasterVO> GetEmployeesDetails(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetEmployeesDetails(searchValue));
        }
        public List<UserMasterVO> GetAgentDetails(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetAgentDetails(searchValue));
        }
        public List<UserMasterVO> GetToDetails(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetToDetails(searchValue));
        }
        public void ApproveUserRegistration(string userId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveUserRegistration(userId, remarks, taskCode));
        }

        public void VerifyUserRegistration(string userId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.VerifyUserRegistration(userId, remarks, taskCode));
        }

        public void RejectUserRegistration(string userId, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectUserRegistration(userId, remarks, taskCode));
        }

        public UserMasterVO GetUserDetailsById()
        {
            return WrapOperationWithException(() => Channel.GetUserDetailsById());
        }

        public UserMasterVO GetUserDetailsByIDView(int id)
        {
            return WrapOperationWithException(() => Channel.GetUserDetailsByIDView(id));
        }
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        public List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers()
        {
            return WrapOperationWithException(() => Channel.GetUsersByWorkmenServiceTypeUsers());
        }


        public User AddUserRegistration(User userData)
        {
            return WrapOperationWithException(() => Channel.AddUserRegistration(userData));
        }

        public UserMasterVO ResetUserPassword(int userid)
        {
            return WrapOperationWithException(() => Channel.ResetUserPassword(userid));
        }

        public List<UserMasterVO> GetUsersListForGrid(string userType, string searchText, string darmentUser, string referenceNo)
        {
            return WrapOperationWithException(() => Channel.GetUsersListForGrid(userType, searchText, darmentUser, referenceNo));
        }


        public int CheckUserExists(string UserTypeID, string UserName) {

            return WrapOperationWithException(() => Channel.CheckUserExists(UserTypeID, UserName));
        }

        public IEnumerable<PortVO> GetAllPortsDetails()
        {
            return WrapOperationWithException(() => Channel.GetAllPortsDetails());
        }


        //AnushaSapNumber
        public List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo)
        {
            return WrapOperationWithException(() => Channel.GetEmployeesListFetching(PortCode, ReferenceNo));
        }


        //AnushaSapNumber
        public List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo)
        {
            return WrapOperationWithException(() => Channel.GetAgentListDetailsFetch(PortCode, ReferenceNo));
        }



        //AnushaSapNumber
        public List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo)
        {
            return WrapOperationWithException(() => Channel.GetTerminalOperatorListDetailsFetch(PortCode, ReferenceNo));
        }

    }
}