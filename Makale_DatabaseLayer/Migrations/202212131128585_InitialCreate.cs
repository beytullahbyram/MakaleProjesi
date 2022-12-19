namespace Makale_DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Begeniler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kullanıcı_ID = c.Int(),
                        Makaleler_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.Kullanıcı_ID)
                .ForeignKey("dbo.Makaleler", t => t.Makaleler_ID)
                .Index(t => t.Kullanıcı_ID)
                .Index(t => t.Makaleler_ID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Adi = c.String(maxLength: 50),
                        Soyad = c.String(maxLength: 50),
                        KullaniciAdi = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 60),
                        AktivasyonAnahtari = c.Guid(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        KayıtTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Makaleler",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 20),
                        Text = c.String(nullable: false, maxLength: 250),
                        TaslakDurumu = c.Boolean(nullable: false),
                        BegeniSayisi = c.Int(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        KayıtTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        Kullanici_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kategoriler", t => t.KategoriId, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_ID)
                .Index(t => t.KategoriId)
                .Index(t => t.Kullanici_ID);
            
            CreateTable(
                "dbo.Kategoriler",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriBaslik = c.String(nullable: false, maxLength: 50),
                        KategoriAciklama = c.String(maxLength: 300),
                        KayıtTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Yorums",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        KayıtTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        Kullanici_ID = c.Int(),
                        Makaleler_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_ID)
                .ForeignKey("dbo.Makaleler", t => t.Makaleler_ID)
                .Index(t => t.Kullanici_ID)
                .Index(t => t.Makaleler_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorums", "Makaleler_ID", "dbo.Makaleler");
            DropForeignKey("dbo.Yorums", "Kullanici_ID", "dbo.Kullanici");
            DropForeignKey("dbo.Makaleler", "Kullanici_ID", "dbo.Kullanici");
            DropForeignKey("dbo.Makaleler", "KategoriId", "dbo.Kategoriler");
            DropForeignKey("dbo.Begeniler", "Makaleler_ID", "dbo.Makaleler");
            DropForeignKey("dbo.Begeniler", "Kullanıcı_ID", "dbo.Kullanici");
            DropIndex("dbo.Yorums", new[] { "Makaleler_ID" });
            DropIndex("dbo.Yorums", new[] { "Kullanici_ID" });
            DropIndex("dbo.Makaleler", new[] { "Kullanici_ID" });
            DropIndex("dbo.Makaleler", new[] { "KategoriId" });
            DropIndex("dbo.Begeniler", new[] { "Makaleler_ID" });
            DropIndex("dbo.Begeniler", new[] { "Kullanıcı_ID" });
            DropTable("dbo.Yorums");
            DropTable("dbo.Kategoriler");
            DropTable("dbo.Makaleler");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Begeniler");
        }
    }
}
