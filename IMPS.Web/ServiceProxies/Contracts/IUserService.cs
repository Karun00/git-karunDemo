using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IUserService : IDisposable
    {
        [OperationContract]
        List<SubCategoryVO> GetUserType();
        [OperationContract]
        List<Role> GetRoles();
        [OperationContract]
        List<UserMasterVO> GetUsersList();
        //[OperationContract]
        //List<UserMasterVO> GetEmployeesDetails();
        [OperationContract]
        List<UserMasterVO> GetEmployeesDetails(string searchValue);
        //[OperationContract]
        //List<UserMasterVO> GetAgentDetails();
        [OperationContract]
        List<UserMasterVO> GetAgentDetails(string searchValue);
        //[OperationContract]
        //List<UserMasterVO> GetToDetails();
        [OperationContract]
        List<UserMasterVO> GetToDetails(string searchValue);
        //[OperationContract]
        //List<UserMasterVO> AddUser(User userdata);
        [OperationContract]
        int AddUser(User userData);
        [OperationContract]
        int ModifyUser(User userData, string ipAddress, string machineName);
        [OperationContract]
        User DeleteUserById(User userData);
        [OperationContract]
        User GetUserById(int userId);
        [OperationContract]
        User GetUserByName(string userName);
        //[OperationContract]
        //Task<List<UserMasterVO>> AddUserAsync(User userdata);
        //[OperationContract]
        //Task<List<UserMasterVO>> GetUsersListAsync();
        //[OperationContract]
        //Task<List<UserMasterVO>> GetAgentDetailsAsync();
        //[OperationContract]
        //int ModifyUserAsync(User userdata);
        //[OperationContract]
        //Task<User> DelUserByIDAsync(User userdata);

        [OperationContract]
        User AddRoles(User userData);
        //[OperationContract]
        //Task<User> AddRolesAsync(User userdata);

        [OperationContract]
        User AddPorts(User userData);
        //[OperationContract]
        //Task<User> AddPortsAsync(User userdata);
        [OperationContract]
        void ApproveUserRegistration(string userId, string remarks, string taskCode);
        [OperationContract]
        void VerifyUserRegistration(string userId, string remarks, string taskCode);
        [OperationContract]
        void RejectUserRegistration(string userId, string remarks, string taskCode);
        [OperationContract]
        UserMasterVO GetUserDetailsById();
        [OperationContract]
        UserMasterVO GetUserDetailsByIDView(int id);
        
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers();


        [OperationContract]
        User AddUserRegistration(User userData);

        [OperationContract]
        UserMasterVO ResetUserPassword(int userid);

         [OperationContract]
        List<UserMasterVO> GetUsersListForGrid(string userType, string searchText, string darmentUser, string referenceNo);


         [OperationContract]
         int CheckUserExists(string UserTypeID, string UserName);
         //[OperationContract]
         //Task<List<UserMasterVO>> GetUsersListForGridAsync(string userType, string SearchText, string DarmentUser);

         [OperationContract]
         IEnumerable<PortVO> GetAllPortsDetails();





        //AnushaSapNumber
        [OperationContract]
        List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo);



        //AnushaSapNumber
        [OperationContract]
        List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo);


        [OperationContract]
        List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo);







    }
}