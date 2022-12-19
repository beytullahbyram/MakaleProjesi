namespace Makale_DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilResmiEkle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "ProfilResmi", c => c.String(maxLength: 50));
            Sql("Update Kullanici set [ProfilResmi]='user.jpg'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "ProfilResmi");
        }
    }
}
