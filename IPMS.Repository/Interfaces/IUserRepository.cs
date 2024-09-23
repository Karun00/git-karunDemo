using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsersForRoleAndPortCode(string portCode, List<NotificationRole> role);
        List<User> GetUsersForRoleAndPortCodeByUserType(string portCode, List<NotificationRole> role, string userType, int userTypeId);
        List<User> GetUserDetailsForRoleAndPortCodeByUserType(string portCode, string userType, string roleCode, int userTypeId);

        Employee GetEmployee(int id);
        List<Role> RoleList();
        int GetUserIdByLoginname(string loginname);
        string GetUserType(string loginname);
        User GetUserById(int userid);
        User GetUserByName(string userName);
        UserMasterVO GetUserByID(int userId);
        UserMasterVO GetUserDetailsById(int userId);
        UserMasterVO GetUserDetailsByIDView(int id);
        List<UserMasterVO> GetUserDetailByID(int userId);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 17th Sep 2014
        /// Purpose : To get Workmen service type user details
        /// </summary>
        /// <returns></returns>
        List<UserMasterVO> GetUsersByWorkmenServiceTypeUsers();
        List<RoleVO> GetRoles();
        UserMasterVO ResetUserPassword(int userid, string newpwd, int _userid);
        UserMasterVO GetUserByUserID(int userid);
        CompanyVO GetUserDetails(int userId);
        IEnumerable<PortVO> GetAllPortsInfo();



        //Anusha
        List<EmployeeMasterDetails> GetEmployeesListFetching(string PortCode, string ReferenceNo, string portCode);


        //Anusha

     List<AgentDetailsVO> GetAgentListDetailsFetch(string PortCode, string ReferenceNo, string portCode);




        //Anusha
        List<TerminalOperatorVO> GetTerminalOperatorListDetailsFetch(string PortCode, string ReferenceNo, string portCode);

        //karun
       
        Object WithoutUpdatedJsonData1(Object obj, string modelname, object obj2);
    }
}
