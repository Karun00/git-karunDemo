using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;


namespace IPMS.ServiceProxies.Clients
{
    public class AccountClient : UserClientBase<IAccountService>, IAccountService
    {

        public AccountLoginModel UserLogin(string username, string password,string ipAddress)
        {
            return WrapOperationWithException(() => Channel.UserLogin(username, password, ipAddress));
        }

        public IEnumerable<Module> GetModulesByUser()
        {
            return WrapOperationWithException(() => Channel.GetModulesByUser());
        }

        public IEnumerable<Role> GetRoles()
        {
            return WrapOperationWithException(() => Channel.GetRoles());
        }

        public IEnumerable<PortVO> GetPortsByUser()
        {
            return WrapOperationWithException(() => Channel.GetPortsByUser());
        }

        public IEnumerable<GroupedPendingTaskVO> GetPendingTask()
        {
            return WrapOperationWithException(() => Channel.GetPendingTask());
        }

        public PendingTaskVO ApprovedPendingTask(PendingTaskVO pendingtask)
        {
            return WrapOperationWithException(() => Channel.ApprovedPendingTask(pendingtask));
        }

        public PendingTaskVO RejectedPendingTask(PendingTaskVO pendingtask)
        {
            return WrapOperationWithException(() => Channel.RejectedPendingTask(pendingtask));
        }

        public IEnumerable<PendingTaskVO> GetWorkflowTask(PendingTaskVO pendingtask)
        {
            return WrapOperationWithException(() => Channel.GetWorkflowTask(pendingtask));
        }

        public IEnumerable<Module> GetModulesTreeView()
        {
            return WrapOperationWithException(() => Channel.GetModulesTreeView());
        }
        //--By Mahesh : To get system notifications in Header...................

        public IEnumerable<SystemNotificationVO> GetSystemNotifications()
        {
            return WrapOperationWithException(() => Channel.GetSystemNotifications());
        }

        public string ChangePassword(AccountLoginModel passwordModel)
        {
            return WrapOperationWithException(() => Channel.ChangePassword(passwordModel));
        }

        public string GetUserPrivilegesWithControllerName(string controllerName, string username)
        {
            return WrapOperationWithException(() => Channel.GetUserPrivilegesWithControllerName(controllerName, username));
        }

        public int GetPendingTaskCount()
        {
            return WrapOperationWithException(() => Channel.GetPendingTaskCount());
        }

        public int GetPortSessiontimeOut()
        {
            return WrapOperationWithException(() => Channel.GetPortSessiontimeOut());
        }

        public int GetMobilePortSessiontimeOut()
        {
            return WrapOperationWithException(() => Channel.GetMobilePortSessiontimeOut());
        }

        public IEnumerable<PortVO> GetPortsByUserForMobile(string uname)
        {
            return WrapOperationWithException(() => Channel.GetPortsByUserForMobile(uname));
        }
    }
}
