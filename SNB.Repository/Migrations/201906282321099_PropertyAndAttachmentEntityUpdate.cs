namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyAndAttachmentEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttachementFiles", "FileSize", c => c.Int());
            AddColumn("dbo.Properties", "PropertyTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "PropertyTitle");
            DropColumn("dbo.AttachementFiles", "FileSize");
        }
    }
}
