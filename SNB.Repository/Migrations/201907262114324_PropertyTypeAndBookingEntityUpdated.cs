namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyTypeAndBookingEntityUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyTypes", "IsSeatingType", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropertyBookings", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropertyBookings", "Remarks");
            DropColumn("dbo.PropertyTypes", "IsSeatingType");
        }
    }
}
