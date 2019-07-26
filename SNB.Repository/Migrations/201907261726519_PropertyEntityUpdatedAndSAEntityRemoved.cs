namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyEntityUpdatedAndSAEntityRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeatingAllocations", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.SeatingAllocations", "FeaturedImageId", "dbo.AttachmentFiles");
            DropForeignKey("dbo.SeatingAllocationImages", "AttachementFileId", "dbo.AttachmentFiles");
            DropForeignKey("dbo.SeatingAllocationImages", "SeatingAllocationId", "dbo.SeatingAllocations");
            DropForeignKey("dbo.SeatingAllocations", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.SeatingAllocations", "SeatingTypeId", "dbo.SeatingTypes");
            DropForeignKey("dbo.SeatingAllocations", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PropertyBookings", "SeatingAllocationId", "dbo.SeatingAllocations");
            DropIndex("dbo.SeatingAllocations", new[] { "SeatingTypeId" });
            DropIndex("dbo.SeatingAllocations", new[] { "PropertyId" });
            DropIndex("dbo.SeatingAllocations", new[] { "FeaturedImageId" });
            DropIndex("dbo.SeatingAllocations", new[] { "CreatedBy" });
            DropIndex("dbo.SeatingAllocations", new[] { "UpdatedBy" });
            DropIndex("dbo.SeatingAllocationImages", new[] { "SeatingAllocationId" });
            DropIndex("dbo.SeatingAllocationImages", new[] { "AttachementFileId" });
            DropIndex("dbo.PropertyBookings", new[] { "SeatingAllocationId" });
            AddColumn("dbo.Properties", "TotalSeat", c => c.Int());
            AddColumn("dbo.Properties", "AvailableSeat", c => c.Int());
            AddColumn("dbo.Properties", "Description", c => c.String());
            AddColumn("dbo.Properties", "Rent", c => c.Double(nullable: false));
            AddColumn("dbo.Properties", "AvailableFrom", c => c.DateTime());
            AddColumn("dbo.Properties", "Status", c => c.Int());
            AddColumn("dbo.PropertyBookings", "PropertyId", c => c.Int(nullable: false));
            AddColumn("dbo.PropertyBookings", "TotalSeat", c => c.Int());
            CreateIndex("dbo.PropertyBookings", "PropertyId");
            AddForeignKey("dbo.PropertyBookings", "PropertyId", "dbo.Properties", "Id", cascadeDelete: true);
            DropColumn("dbo.PropertyBookings", "SeatingAllocationId");
            DropTable("dbo.SeatingAllocations");
            DropTable("dbo.SeatingAllocationImages");
            DropTable("dbo.SeatingTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SeatingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeatingAllocationImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatingAllocationId = c.Int(nullable: false),
                        AttachementFileId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeatingAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatingAllocationTitle = c.String(nullable: false),
                        SeatingTypeId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                        FeaturedImageId = c.Int(),
                        Description = c.String(),
                        Rent = c.Double(nullable: false),
                        AvailableDate = c.DateTime(),
                        Status = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PropertyBookings", "SeatingAllocationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PropertyBookings", "PropertyId", "dbo.Properties");
            DropIndex("dbo.PropertyBookings", new[] { "PropertyId" });
            DropColumn("dbo.PropertyBookings", "TotalSeat");
            DropColumn("dbo.PropertyBookings", "PropertyId");
            DropColumn("dbo.Properties", "Status");
            DropColumn("dbo.Properties", "AvailableFrom");
            DropColumn("dbo.Properties", "Rent");
            DropColumn("dbo.Properties", "Description");
            DropColumn("dbo.Properties", "AvailableSeat");
            DropColumn("dbo.Properties", "TotalSeat");
            CreateIndex("dbo.PropertyBookings", "SeatingAllocationId");
            CreateIndex("dbo.SeatingAllocationImages", "AttachementFileId");
            CreateIndex("dbo.SeatingAllocationImages", "SeatingAllocationId");
            CreateIndex("dbo.SeatingAllocations", "UpdatedBy");
            CreateIndex("dbo.SeatingAllocations", "CreatedBy");
            CreateIndex("dbo.SeatingAllocations", "FeaturedImageId");
            CreateIndex("dbo.SeatingAllocations", "PropertyId");
            CreateIndex("dbo.SeatingAllocations", "SeatingTypeId");
            AddForeignKey("dbo.PropertyBookings", "SeatingAllocationId", "dbo.SeatingAllocations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeatingAllocations", "UpdatedBy", "dbo.Users", "Id");
            AddForeignKey("dbo.SeatingAllocations", "SeatingTypeId", "dbo.SeatingTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeatingAllocations", "PropertyId", "dbo.Properties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeatingAllocationImages", "SeatingAllocationId", "dbo.SeatingAllocations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeatingAllocationImages", "AttachementFileId", "dbo.AttachmentFiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeatingAllocations", "FeaturedImageId", "dbo.AttachmentFiles", "Id");
            AddForeignKey("dbo.SeatingAllocations", "CreatedBy", "dbo.Users", "Id");
        }
    }
}
