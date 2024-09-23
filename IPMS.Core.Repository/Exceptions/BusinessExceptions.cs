using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace IPMS.Core.Repository.Exceptions
{
    public class BusinessExceptions : Exception, ISerializable, _Exception
    {
        public const string InvalidCredentials = "Invalid Login Credentials";
        public const string InternalServerErrorMessage = "Internal Server error occured. Please contact to administrator.";
        public const string NotAuthorizedUser = "You are not authorized user.";
        public const string InvalidUserName = "Invalid Login Credentials";
        public const string SessionTimeOut = "Your session has timed out. Please sign in again.";

        public BusinessExceptions()
        {
        }

        public BusinessExceptions(string message)
            : base(message)
        {
        }

        public BusinessExceptions(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
