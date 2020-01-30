namespace CRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrmContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        PublishingId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        InventoryNum = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Publishings", t => t.PublishingId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.PublishingId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.CopyesBooks",
                c => new
                    {
                        CopyId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Inventory = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CopyId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        ReaderId = c.Int(nullable: false),
                        CopyId = c.Int(nullable: false),
                        DateOfIssue = c.DateTime(),
                        OfferDate = c.DateTime(),
                        ToAcceptDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.SubscriptionId)
                .ForeignKey("dbo.CopyesBooks", t => t.CopyId, cascadeDelete: true)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId)
                .Index(t => t.CopyId);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        ReaderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        DateOfBirth = c.DateTime(),
                        Adress = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ReaderId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Publishings",
                c => new
                    {
                        PublishingId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PublishingId);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        PasswordID = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 30),
                        Pass = c.String(nullable: false, maxLength: 30),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PasswordID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PublishingId", "dbo.Publishings");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Subscriptions", "ReaderId", "dbo.Readers");
            DropForeignKey("dbo.Subscriptions", "CopyId", "dbo.CopyesBooks");
            DropForeignKey("dbo.CopyesBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Subscriptions", new[] { "CopyId" });
            DropIndex("dbo.Subscriptions", new[] { "ReaderId" });
            DropIndex("dbo.CopyesBooks", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropIndex("dbo.Books", new[] { "PublishingId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Passwords");
            DropTable("dbo.Publishings");
            DropTable("dbo.Genres");
            DropTable("dbo.Readers");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.CopyesBooks");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
