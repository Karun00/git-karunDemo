using Core.Repository;
using IPMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Data
{

    class TnpaContextMigrationConfiguration : DbMigrationsConfiguration<TnpaContext>
    {
        public TnpaContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

        }

#if DEBUG
        protected override void Seed(TnpaContext context)
        {
            new TnpaDataSeeder(context, new UnitOfWork(new TnpaContext())).Seed();
        }
#endif

    }

}
