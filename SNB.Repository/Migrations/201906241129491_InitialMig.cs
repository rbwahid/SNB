namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Guid(nullable: false),
                        EventType = c.String(),
                        TableName = c.String(nullable: false),
                        PrimaryKeyName = c.String(),
                        PrimaryKeyValue = c.String(),
                        ColumnName = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        CreatedUser = c.Int(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AuditLogId)
                .ForeignKey("dbo.Users", t => t.CreatedUser)
                .Index(t => t.CreatedUser);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                        Password = c.String(nullable: false),
                        Mobile = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        SupUser = c.Boolean(nullable: false),
                        ImageFile = c.String(),
                        RoleId = c.Int(),
                        LastPassword = c.String(),
                        LastPassChangeDate = c.DateTime(),
                        PasswordChangedCount = c.Int(),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(),
                        Status = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Status = c.Int(),
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
                "dbo.UserRolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        Permission = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.LoginRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 1000),
                        SessionId = c.String(maxLength: 1000),
                        LoggedIn = c.Boolean(nullable: false),
                        LoggedInDateTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditLogs", "CreatedUser", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.UserRolePermissions", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "CreatedBy", "dbo.Users");
            DropIndex("dbo.UserRolePermissions", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UpdatedBy" });
            DropIndex("dbo.UserRoles", new[] { "CreatedBy" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.AuditLogs", new[] { "CreatedUser" });
            DropTable("dbo.LoginRecords");
            DropTable("dbo.UserRolePermissions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.AuditLogs");
        }
    }
}
