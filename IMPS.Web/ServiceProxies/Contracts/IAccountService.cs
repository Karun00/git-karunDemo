using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IAccountService : IDisposable
    {
        [OperationContract]
        AccountLoginModel UserLogin(string username, string password,string ipAddress);

        [OperationContract]
        IEnumerable<Module> GetModulesByUser();

        [OperationContract]
        IEnumerable<Role> GetRoles();

        [OperationContract]
        IEnumerable<PortVO> GetPortsByUser();

        [OperationContract]
        IEnumerable<GroupedPendingTaskVO> GetPendingTask();

        [OperationContract]
        PendingTaskVO ApprovedPendingTask(PendingTaskVO pendingtask);

        [OperationContract]
        PendingTaskVO RejectedPendingTask(PendingTaskVO pendingtask);

        [OperationContract]
        IEnumerable<PendingTaskVO> GetWorkflowTask(PendingTaskVO pendingtask);
        //--
        [OperationContract]
        IEnumerable<Module> GetModulesTreeView();

        //--By Mahesh : To get system notifications in Header...................

        [OperationContract]
        IEnumerable<SystemNotificationVO> GetSystemNotifications();

        [OperationContract]
        string ChangePassword(AccountLoginModel passwordModel);

        [OperationContract]
        string GetUserPrivilegesWithControllerName(string controllerName, string username);

        [OperationContract]
        int GetPendingTaskCount();

        [OperationContract]
        int GetPortSessiontimeOut();

        [OperationContract]
        int GetMobilePortSessiontimeOut();

        [OperationContract]
        IEnumerable<PortVO> GetPortsByUserForMobile(string uname);
        
    }
}

