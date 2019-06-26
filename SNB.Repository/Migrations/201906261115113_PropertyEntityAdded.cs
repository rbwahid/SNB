namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AreaName = c.String(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttachementFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        OrginalFileName = c.String(),
                        FileExtension = c.String(),
                        FileLocation = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false),
                        Address = c.String(),
                        ContactNo = c.String(),
                        UserId = c.Int(nullable: false),
                        PropertyTypeId = c.Int(nullable: false),
                        AreaId = c.Int(),
                        DistrictId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.PropertyTypes", t => t.PropertyTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PropertyTypeId)
                .Index(t => t.AreaId)
                .Index(t => t.DistrictId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.PropertyImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        AttachementFileId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttachementFiles", t => t.AttachementFileId, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId)
                .Index(t => t.AttachementFileId);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeatingAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.AttachementFiles", t => t.FeaturedImageId)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.SeatingTypes", t => t.SeatingTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.SeatingTypeId)
                .Index(t => t.PropertyId)
                .Index(t => t.FeaturedImageId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.SeatingAllocationImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatingAllocationId = c.Int(nullable: false),
                        AttachementFileId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttachementFiles", t => t.AttachementFileId, cascadeDelete: true)
                .ForeignKey("dbo.SeatingAllocations", t => t.SeatingAllocationId, cascadeDelete: true)
                .Index(t => t.SeatingAllocationId)
                .Index(t => t.AttachementFileId);
            
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
                "dbo.PropertyBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatingAllocationId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ConfirmDate = c.DateTime(),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        Status = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.SeatingAllocations", t => t.SeatingAllocationId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.SeatingAllocationId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.Users", "NationalID", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyBookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.PropertyBookings", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PropertyBookings", "SeatingAllocationId", "dbo.SeatingAllocations");
            DropForeignKey("dbo.PropertyBookings", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Properties", "UserId", "dbo.Users");
            DropForeignKey("dbo.Properties", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.SeatingAllocations", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.SeatingAllocations", "SeatingTypeId", "dbo.SeatingTypes");
            DropForeignKey("dbo.SeatingAllocations", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.SeatingAllocationImages", "SeatingAllocationId", "dbo.SeatingAllocations");
            DropForeignKey("dbo.SeatingAllocationImages", "AttachementFileId", "dbo.AttachementFiles");
            DropForeignKey("dbo.SeatingAllocations", "FeaturedImageId", "dbo.AttachementFiles");
            DropForeignKey("dbo.SeatingAllocations", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Properties", "PropertyTypeId", "dbo.PropertyTypes");
            DropForeignKey("dbo.PropertyImages", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyImages", "AttachementFileId", "dbo.AttachementFiles");
            DropForeignKey("dbo.Properties", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Properties", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Properties", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.AttachementFiles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.AttachementFiles", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Areas", "DistrictId", "dbo.Districts");
            DropIndex("dbo.PropertyBookings", new[] { "UpdatedBy" });
            DropIndex("dbo.PropertyBookings", new[] { "CreatedBy" });
            DropIndex("dbo.PropertyBookings", new[] { "UserId" });
            DropIndex("dbo.PropertyBookings", new[] { "SeatingAllocationId" });
            DropIndex("dbo.SeatingAllocationImages", new[] { "AttachementFileId" });
            DropIndex("dbo.SeatingAllocationImages", new[] { "SeatingAllocationId" });
            DropIndex("dbo.SeatingAllocations", new[] { "UpdatedBy" });
            DropIndex("dbo.SeatingAllocations", new[] { "CreatedBy" });
            DropIndex("dbo.SeatingAllocations", new[] { "FeaturedImageId" });
            DropIndex("dbo.SeatingAllocations", new[] { "PropertyId" });
            DropIndex("dbo.SeatingAllocations", new[] { "SeatingTypeId" });
            DropIndex("dbo.PropertyImages", new[] { "AttachementFileId" });
            DropIndex("dbo.PropertyImages", new[] { "PropertyId" });
            DropIndex("dbo.Properties", new[] { "UpdatedBy" });
            DropIndex("dbo.Properties", new[] { "CreatedBy" });
            DropIndex("dbo.Properties", new[] { "DistrictId" });
            DropIndex("dbo.Properties", new[] { "AreaId" });
            DropIndex("dbo.Properties", new[] { "PropertyTypeId" });
            DropIndex("dbo.Properties", new[] { "UserId" });
            DropIndex("dbo.AttachementFiles", new[] { "UpdatedBy" });
            DropIndex("dbo.AttachementFiles", new[] { "CreatedBy" });
            DropIndex("dbo.Areas", new[] { "DistrictId" });
            DropColumn("dbo.Users", "NationalID");
            DropTable("dbo.PropertyBookings");
            DropTable("dbo.SeatingTypes");
            DropTable("dbo.SeatingAllocationImages");
            DropTable("dbo.SeatingAllocations");
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.PropertyImages");
            DropTable("dbo.Properties");
            DropTable("dbo.AttachementFiles");
            DropTable("dbo.Districts");
            DropTable("dbo.Areas");
        }
    }
}
