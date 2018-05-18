namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "Image", c => c.Binary(maxLength: 8000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "Image");
        }
    }
}
