using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PrivilegeVO
    {
        /// <summary>
        /// Comma separated privileges ADD,DELETE,VIEW,EDIT,APPROVE etc.
        /// </summary>
        public string Privileges { get; set; }

        public bool HasPrivilege(string privilegeCode)
        {
            return Privileges.Contains(privilegeCode);
        }
        public bool HasAddPrivilege
        {
            get
            {
                return HasPrivilege("ADD");
            }
        }
        public bool HasViewPrivilege
        {
            get
            {
                return HasPrivilege("VIEW");
            }
        }
        public bool HasDeletePrivilege
        {
            get
            {
                return HasPrivilege("DEL");
            }
        }
        public bool HasEditPrivilege
        {
            get
            {
                return HasPrivilege("EDIT");
            }
        }
        public bool HasVerifyPrivilege
        {
            get
            {
                return HasPrivilege("VERF");
            }
        }
        public bool HasVerificationApprovePrivilege
        {
            get
            {
                return HasPrivilege("VERIFY");
            }
        }
        public bool HasVerificationRejectPrivilege
        {
            get
            {
                return HasPrivilege("VERIFYREJECT");
            }
        }
        public bool HasApprovalRejectPrivilege
        {
            get
            {
                return HasPrivilege("APPROVEREJECT");
            }
        }
        public bool HasApprovalApprovePrivilege
        {
            get
            {
                return HasPrivilege("APPROVE");
            }
        }
        public bool HasRequestToResubmitPrivilege
        {
            get
            {
                return HasPrivilege("REQUESTTORESUBMIT");
            }
        }
        public bool HasCancellationPrivilege
        {
            get
            {
                return HasPrivilege("CANCEL");
            }
        }
        public bool HasConfirmationPrivilege
        {
            get
            {
                return HasPrivilege("CONFIRM");
            }
        }
        public bool HasExportPrivilege
        {
            get
            {
                return HasPrivilege("EXPORT");
            }
        }
        public bool HasPrintPrivilege
        {
            get
            {
                return HasPrivilege("PRINT");
            }
        }
        public bool HasVcnClosePrivilege
        {
            get
            {
                return HasPrivilege("VcnClose");
            }
        }
        public List<string> SubCatCode
        {
            get;
            set;
        }

    }
}