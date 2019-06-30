namespace SNB.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEntityUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserType");
        }
    }
}
