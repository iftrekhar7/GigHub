namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        AttendeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.AttendeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeId, cascadeDelete: true)
                .ForeignKey("dbo.Gigs", t => t.GigId)
                .Index(t => t.GigId)
                .Index(t => t.AttendeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "AttendeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "AttendeId" });
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropTable("dbo.Attendances");
        }
    }
}
