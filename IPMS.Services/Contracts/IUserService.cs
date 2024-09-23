using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetUserType();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<Role> GetRoles();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetUsersList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int AddUser(User userData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ModifyUser(User userData,string ipAddress,string machineName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User DeleteUserById(User userData);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetEmployeesDetails(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetAgentDetails(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetToDetails(string searchValue);       

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User AddRoles(User userData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User AddPorts(User userData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User GetUserById(int userId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User GetUserByName(string userName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveUserRegistration(string userId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyUserRegistration(string userId, string remarks, string taskCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectUserRegistration(string userId, string remarks, string taskCode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        UserMasterVO GetUserDetailsById();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        UserMasterVO GetUserDetailsByIDView(int id);

        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        User AddUserRegistration(User userData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        UserMasterVO ResetUserPassword(int userid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<UserMasterVO> GetUsersListForGrid(string userType, string searchText, string darmentUser, string referenceNo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckUserExists(string UserTypeID, string UserName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PortVO> GetAllPortsDetails();






        //AnushaSapNumber
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo);

        //AnushaSapNumber
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo);



        //AnushaSapNumber
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo);





    }
}
