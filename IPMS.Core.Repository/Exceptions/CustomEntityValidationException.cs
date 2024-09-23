using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Core.Repository.Exceptions
{
    public class CustomEntityValidationException : Exception
    {
        public CustomEntityValidationException()
            : base() { }

        public CustomEntityValidationException(string message)
            : base(message) { }

        public CustomEntityValidationException(string format, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture,format, args)) { }

        public CustomEntityValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public CustomEntityValidationException(string format, Exception innerException, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture,format, args), innerException) { }

        protected CustomEntityValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
