namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CPassword");
        }
    }
}
