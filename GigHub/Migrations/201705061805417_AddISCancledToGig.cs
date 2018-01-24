namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddISCancledToGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "ISCancled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "ISCancled");
        }
    }
}
