using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Core.Repository.Providers.EntityFramework.Fakes;
using IPMS.Data.Mapping;



namespace IPMS.Service.Tests
{
    public class PortDbSet : FakeDbSet<Port>
    {
        public override Port Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.PortCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<Port> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Port>(() => Find(keyValues));
        }
    }

    public class AgentDocumentDbSet : FakeDbSet<AgentDocument>
    {
        public override AgentDocument Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AgentID == (long)keyValues.FirstOrDefault());
        }
        public override Task<AgentDocument> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<AgentDocument>(() => Find(keyValues));
        }
    }

    public class AuthorizedContactPersonDbSet : FakeDbSet<AuthorizedContactPerson>
    {
        public override AuthorizedContactPerson Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AuthorizedContactPersonID == (long)keyValues.FirstOrDefault());
        }
        public override Task<AuthorizedContactPerson> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<AuthorizedContactPerson>(() => Find(keyValues));
        }
    }

    public class AgentPortDbSet : FakeDbSet<AgentPort>
    {
        public override AgentPort Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AgentID == (long)keyValues.FirstOrDefault());
        }
        public override Task<AgentPort> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<AgentPort>(() => Find(keyValues));
        }
    }

    public class AgentDbSet : FakeDbSet<Agent>
    {
        public override Agent Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AgentID == (long)keyValues.FirstOrDefault());
        }
        public override Task<Agent> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Agent>(() => Find(keyValues));
        }
    }

    public class AddressDbSet : FakeDbSet<Address>
    {
        public override Address Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AddressID == (long)keyValues.FirstOrDefault());
        }
        public override Task<Address> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Address>(() => Find(keyValues));
        }
    }

    public class Email_AlertsDbSet : FakeDbSet<EmailAlert>
    {
        public override EmailAlert Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.EmaliTblPkID == (long)keyValues.FirstOrDefault());
        }
        public override Task<EmailAlert> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<EmailAlert>(() => Find(keyValues));
        }
    }

    public class UserDbSet : FakeDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.UserID == (long)keyValues.FirstOrDefault());
        }
        public override Task<User> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<User>(() => Find(keyValues));
        }
    }

    public class ModulesDbSet : FakeDbSet<Module>
    {
        public override Module Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ModuleID == (long)keyValues.FirstOrDefault());
        }
        public override Task<Module> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Module>(() => Find(keyValues));
        }
    }

    public class Sms_AlertsDbSet : FakeDbSet<SmsAlert>
    {
        public override SmsAlert Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.SmsTblPkID == (long)keyValues.FirstOrDefault());
        }
        public override Task<SmsAlert> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<SmsAlert>(() => Find(keyValues));
        }
    }

    public class Sub_CategoryDbSet : FakeDbSet<SubCategory>
    {
        public override SubCategory Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.SubCatCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<SubCategory> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<SubCategory>(() => Find(keyValues));
        }
    }

    //public class Sub_ModulesDbSet : FakeDbSet<SubModule>
    //{
    //    public override SubModule Find(params object[] keyValues)
    //    {
    //        return this.SingleOrDefault(t => t.SubModuleID == (long)keyValues.FirstOrDefault());
    //    }
    //    public override Task<SubModule> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
    //    {
    //        return new Task<SubModule>(() => Find(keyValues));
    //    }
    //}

    public class Super_CategoryDbSet : FakeDbSet<SuperCategory>
    {
        public override SuperCategory Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.SupCatCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<SuperCategory> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<SuperCategory>(() => Find(keyValues));
        }
    }

    public class PendingRegistrationVerificationDbSet : FakeDbSet<PendingRegistrationVerification>
    {
        public override PendingRegistrationVerification Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.PortID == (long)keyValues.FirstOrDefault());
        }
        //public override Task<Super_Category> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        //{
        //    return new Task<PendingRegistrationVerification>(() => Find(keyValues));
        //}
    }

    public class AuditTrailConfigurationDbSet : FakeDbSet<AuditTrailConfig>
    {
        public override AuditTrailConfig Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AuditTrailConfigID == (long)keyValues.FirstOrDefault());
        }
        public override Task<AuditTrailConfig> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<AuditTrailConfig>(() => Find(keyValues));
        }
    }
    public class BerthDbSet : FakeDbSet<Berth>
    {
        public override Berth Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.BerthCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<Berth> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Berth>(() => Find(keyValues));
        }
    }
    public class QuayDbSet : FakeDbSet<Quay>
    {
        public override Quay Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.QuayCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<Quay> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Quay>(() => Find(keyValues));
        }
    }

    public class AuditTrailDbSet : FakeDbSet<AuditTrail>
    {
        public override AuditTrail Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.AuditTrailID == (long)keyValues.FirstOrDefault());
        }
        public override Task<AuditTrail> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<AuditTrail>(() => Find(keyValues));
        }
    }

    public class BollardDbset : FakeDbSet<Bollard>
    {
        public override Bollard Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.BollardCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<Bollard> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Bollard>(() => Find(keyValues));
        }

    }
    public class QuayMasterDetailsDbset : FakeDbSet<QuayMasterDetails>
    {
        public override QuayMasterDetails Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.QuayCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<QuayMasterDetails> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<QuayMasterDetails>(() => Find(keyValues));
        }
    }
    public class NotificationDbset : FakeDbSet<Notification>
    {
        public override Notification Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.NotificationId == (int)keyValues.FirstOrDefault());
        }
        public override Task<Notification> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Notification>(() => Find(keyValues));
        }
    }
    public class NotificationTemplateDbset : FakeDbSet<NotificationTemplate>
    {
        public override NotificationTemplate Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.NotificationTemplateCode == (string)keyValues.FirstOrDefault());
        }
        public override Task<NotificationTemplate> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<NotificationTemplate>(() => Find(keyValues));
        }
    }
    public class NotificationRoleDbset : FakeDbSet<NotificationRole>
    {
        public override NotificationRole Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.NotificationRoleID == (int)keyValues.FirstOrDefault());
        }
        public override Task<NotificationRole> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<NotificationRole>(() => Find(keyValues));
        }
    }
    public class RoleDbset : FakeDbSet<Role>
    {
        public override Role Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.RoleID == (int)keyValues.FirstOrDefault());
        }
        public override Task<Role> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Role>(() => Find(keyValues));
        }
    }
    public class EntityDbset : FakeDbSet<Entity>
    {
        public override Entity Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.EntityID == (int)keyValues.FirstOrDefault());
        }
        public override Task<Entity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Entity>(() => Find(keyValues));
        }
    }

    public class UserRoleDbset : FakeDbSet<UserRole>
    {
        public override UserRole Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.UserID == (int)keyValues.FirstOrDefault());
        }
        public override Task<UserRole> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<UserRole>(() => Find(keyValues));
        }
    }

    public class RolePrivilegeDbset : FakeDbSet<RolePrivilege>
    {
        public override RolePrivilege Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.RoleID == (int)keyValues.FirstOrDefault());
        }
        public override Task<RolePrivilege> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<RolePrivilege>(() => Find(keyValues));
        }
    }

    public class EntityPrivilegeDbset : FakeDbSet<EntityPrivilege>
    {
        public override EntityPrivilege Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.EntityID == (int)keyValues.FirstOrDefault());
        }
        public override Task<EntityPrivilege> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<EntityPrivilege>(() => Find(keyValues));
        }
    }
}
