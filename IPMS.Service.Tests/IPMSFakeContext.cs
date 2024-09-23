using Core.Repository.Providers.EntityFramework.Fakes;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Service.Tests
{
    public class IPMSFakeContext : FakeDbContext
    {
        public IPMSFakeContext()
        {
            AddFakeDbSet<Port, PortDbSet>();
            AddFakeDbSet<AgentDocument, AgentDocumentDbSet>();
            AddFakeDbSet<AuthorizedContactPerson,AuthorizedContactPersonDbSet>();
            AddFakeDbSet<AgentPort,AgentPortDbSet>();
              AddFakeDbSet<Agent,AgentDbSet>();
            AddFakeDbSet<Address, AddressDbSet>();
            AddFakeDbSet<EmailAlert, Email_AlertsDbSet>();
            AddFakeDbSet<User, UserDbSet>();
            AddFakeDbSet<Module, ModulesDbSet>();
            AddFakeDbSet<SmsAlert, Sms_AlertsDbSet>();
            AddFakeDbSet<SubCategory, Sub_CategoryDbSet>();
            //AddFakeDbSet<SubModule, Sub_ModulesDbSet>();
            AddFakeDbSet<SuperCategory, Super_CategoryDbSet>();
            AddFakeDbSet<PendingRegistrationVerification, PendingRegistrationVerificationDbSet>();
            AddFakeDbSet<AuditTrailConfig, AuditTrailConfigurationDbSet>();
            AddFakeDbSet<AuditTrail, AuditTrailDbSet>();
            AddFakeDbSet<Berth, BerthDbSet>();
            AddFakeDbSet<Quay, QuayDbSet>();
            AddFakeDbSet<Bollard, BollardDbset>();
            AddFakeDbSet<Notification, NotificationDbset>();
            AddFakeDbSet<NotificationTemplate, NotificationTemplateDbset>();
            AddFakeDbSet<NotificationRole, NotificationRoleDbset>();
            AddFakeDbSet<Role, RoleDbset>();
            AddFakeDbSet<Entity, EntityDbset>();
            AddFakeDbSet<UserRole, UserRoleDbset>();
            AddFakeDbSet<RolePrivilege, RolePrivilegeDbset>();
            AddFakeDbSet<EntityPrivilege, EntityPrivilegeDbset>();
        }
    }
}
