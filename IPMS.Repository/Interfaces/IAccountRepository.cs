using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<PortVO> GetPortsByUser(int userId);
        IEnumerable<Module> GetModulesByUser(int userId);
        IEnumerable<Role> GetRoles();
        IEnumerable<GroupedPendingTaskVO> GetPendingTask(int userId, string portCode);
        IEnumerable<Module> GetModulesTreeView(int userId);
        IEnumerable<SystemNotificationVO> GetSystemNotifications(int userId, string portCode);

        List<UserRole> GetUserRole(int userId);
        Entity GetEntity(string entityCode);
        int GetUserId(string loginName);
        AccountLoginModel UserLogin(string username, string password,string ipAddress);
        bool ChangePassword(string password, string newPassword, string userName, int userId, string portCode, string previousPasswordsCount);

        string GetUserPrivilegesWithControllerName(string controllerName, string username);


        int GetPendingTaskCount(int userId, string portCode);

    }
}
