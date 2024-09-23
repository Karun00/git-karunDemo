using System;
using Core.Repository.Providers.EntityFramework;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.Collections.Generic;
using System.ServiceModel;

using IPMS.Domain.ValueObjects;



namespace IPMS.Services
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        AccountLoginModel UserLogin(string username, string password, string ipAddress);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<Module> GetModulesByUser();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<Role> GetRoles();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PortVO> GetPortsByUser();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<GroupedPendingTaskVO> GetPendingTask();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PendingTaskVO ApprovedPendingTask(PendingTaskVO pendingtask);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        PendingTaskVO RejectedPendingTask(PendingTaskVO pendingtask);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PendingTaskVO> GetWorkflowTask(PendingTaskVO pendingtask);
        //--
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<Module> GetModulesTreeView();

        //--By Mahesh : To get system notifications in Header...................

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<SystemNotificationVO> GetSystemNotifications();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string ChangePassword(AccountLoginModel passwordModel);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetUserPrivilegesWithControllerName(string controllerName, string username);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetPendingTaskCount();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetPortSessiontimeOut();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetMobilePortSessiontimeOut();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<PortVO> GetPortsByUserForMobile(string uname);
    }
}
