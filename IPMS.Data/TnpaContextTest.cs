using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using IPMS.Data.Context;
using IPMS.Data.Mapping;
using IPMS.Domain.Models;

namespace IPMS.Data.Tests
{
     public partial class TnpaContextTest : DataContext
    {
         static TnpaContextTest()
        {
            Database.SetInitializer<TnpaContextTest>(null);
        }

        public TnpaContextTest()
          : base("Name=tnpa_testContext")
        {
            //Database.SetInitializer<TnpaContext>(null);
          //  Configuration.ProxyCreationEnabled = false;
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<TnpaTestContext, TnpaContextMigrationConfiguration>());
            Database.SetInitializer<TnpaContextTest>(null);


            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<TnpaContextTest, TnpaTestContextMigrationConfiguration>());

        }
        public TnpaContextTest(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        //Generic set.
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<AuditTrailConfig> AuditTrailConfigs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentDocument> AgentDocuments { get; set; }
        public DbSet<AgentPort> AgentPorts { get; set; }
        //   public DbSet<ArrivalNotification> ArrivalNotifications { get; set; }
        public DbSet<AuthorizedContactPerson> AuthorizedContactPersons { get; set; }
        public DbSet<Berth> Berths { get; set; }
        public DbSet<Bollard> Bollards { get; set; }
        //public DbSet<Bunkering> Bunkerings { get; set; }
        public DbSet<BerthPlanningConfigurations> BerthPlanningConfigurations { get; set; }
        public DbSet<CargoManifest> CargoManifests { get; set; }
        //public DbSet<ChangeETA> ChangeETAs { get; set; }
        public DbSet<CraftOutOfCommission> CraftOutOfCommissions { get; set; }
        //public DbSet<Diving> Divings { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityPrivilege> EntityPrivileges { get; set; }
        //public DbSet<FireEquipment> FireEquipments { get; set; }
        //public DbSet<FireProtection> FireProtections { get; set; }
        //public DbSet<FloatingCrane> FloatingCranes { get; set; }
        //public DbSet<LicenseRequest> LicenseRequests { get; set; }
        //public DbSet<LicenseRequestPort> LicenseRequestPorts { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<News> News { get; set; }
        //public DbSet<PestControl> PestControls { get; set; }
        //public DbSet<Pilot> Pilots { get; set; }
        //public DbSet<PollutionControl> PollutionControls { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<PortConfiguration> PortConfigurations { get; set; }
        public DbSet<Quay> Quays { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        //public DbSet<Stevedore> Stevedores { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SuperCategory> SuperCategories { get; set; }
        public DbSet<TerminalOperator> TerminalOperators { get; set; }
        public DbSet<TerminalOperatorBerth> TerminalOperatorBerths { get; set; }
        public DbSet<TerminalOperatorCargoHandling> TerminalOperatorCargoHandlings { get; set; }
        public DbSet<UserPort> UserPorts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationRole> NotificationRoles { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<ResourceGroup> ResourceGroup { get; set; }
        public DbSet<VesselCallMovement> VesselCallMovement { get; set; }
        //public DbSet<Vessel> Vessels { get; set; }
        //public DbSet<VesselApproval> VesselApprovals { get; set; }
        //public DbSet<VesselEngine> VesselEngines { get; set; }
        //public DbSet<VesselGear> VesselGears { get; set; }
        //public DbSet<VesselGrab> VesselGrabs { get; set; }
        //public DbSet<VesselHatchHold> VesselHatchHolds { get; set; }

            //anusha28-05-2024
        public DbSet<ResetPasswordLogs> ResetPasswordLogss { get; set; }
        public DbSet<CommonAllData> CommonAllDatas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                modelBuilder.Configurations.Add(new AuditTrailMap());
                modelBuilder.Configurations.Add(new AuditTrailConfigMap());
                modelBuilder.Configurations.Add(new AddressMap());
                modelBuilder.Configurations.Add(new AgentMap());
                modelBuilder.Configurations.Add(new AgentDocumentMap());
                modelBuilder.Configurations.Add(new AgentPortMap());
                modelBuilder.Configurations.Add(new AuthorizedContactPersonMap());
                modelBuilder.Configurations.Add(new BerthMap());
                modelBuilder.Configurations.Add(new BerthPlanningConfigurationsMap());
                modelBuilder.Configurations.Add(new BollardMap());
                modelBuilder.Configurations.Add(new CargoManifestMap());
                modelBuilder.Configurations.Add(new CraftOutOfCommissionMap());
                modelBuilder.Configurations.Add(new DocumentMap());
                modelBuilder.Configurations.Add(new EmployeeMap());
                modelBuilder.Configurations.Add(new EntityMap());
                modelBuilder.Configurations.Add(new EntityPrivilegeMap());
                modelBuilder.Configurations.Add(new ModuleMap());
                modelBuilder.Configurations.Add(new NewsMap());
                modelBuilder.Configurations.Add(new PortMap());
                modelBuilder.Configurations.Add(new QuayMap());
                modelBuilder.Configurations.Add(new RoleMap());
                modelBuilder.Configurations.Add(new RolePrivilegeMap());
                modelBuilder.Configurations.Add(new SubCategoryMap());
                modelBuilder.Configurations.Add(new SuperCategoryMap());
                modelBuilder.Configurations.Add(new TerminalOperatorMap());
                modelBuilder.Configurations.Add(new TerminalOperatorBerthMap());
                modelBuilder.Configurations.Add(new TerminalOperatorCargoHandlingMap());
                modelBuilder.Configurations.Add(new UserPortMap());
                modelBuilder.Configurations.Add(new UserRoleMap());
                modelBuilder.Configurations.Add(new UserMap());
                modelBuilder.Configurations.Add(new NotificationMap());
                modelBuilder.Configurations.Add(new NotificationRoleMap());
                modelBuilder.Configurations.Add(new NotificationTemplateMap());
                modelBuilder.Configurations.Add(new PortConfigurationMap());
                modelBuilder.Configurations.Add(new ResourceGroupMap());
                modelBuilder.Configurations.Add(new VesselCallMovementMap());
                //anusha28-05-2024
                modelBuilder.Configurations.Add(new ResetPasswordLogsMap());
                modelBuilder.Configurations.Add(new CommonAllDataMap());
            }
        }



    }
}
