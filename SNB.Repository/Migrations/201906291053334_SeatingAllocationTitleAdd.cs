namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeatingAllocationTitleAdd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AttachementFiles", newName: "AttachmentFiles");
            AddColumn("dbo.SeatingAllocations", "SeatingAllocationTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeatingAllocations", "SeatingAllocationTitle");
            RenameTable(name: "dbo.AttachmentFiles", newName: "AttachementFiles");
        }
    }
}
