using System.Collections.Specialized;
using System.Configuration;
using System.Web.Security;
using WebMatrix.WebData;

namespace IPMS.Web.Adapters
{
   public interface ISecurityAdapter
    {
        void Initialize();
        bool Login(string username, string password, bool rememberMe);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool UserExists(string username);
        int GetUserId(string username);
    }
}
