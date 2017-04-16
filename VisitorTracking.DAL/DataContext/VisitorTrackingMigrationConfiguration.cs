using System.Data.Entity.Migrations;

namespace VisitorTracking.DAL.DataContext
{
    class VisitorTrackingMigrationConfiguration : DbMigrationsConfiguration<VisitorTrackingContext>
    {
        public VisitorTrackingMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }
    }
}
