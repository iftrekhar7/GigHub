namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres(Id,Name) values(1,'juzz')");
            Sql("Insert into Genres(Id,Name) values(2,'Blues')");
            Sql("Insert into Genres(Id,Name) values(3,'Rock')");
            Sql("Insert into Genres(Id,Name) values(4,'Country')");
        }
        
        public override void Down()
        {
            Sql("delete from Genres where Id in (1,2,3,4)");
        }
    }
}
