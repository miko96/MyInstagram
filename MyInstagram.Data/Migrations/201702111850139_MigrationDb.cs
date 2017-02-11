namespace MyInstagram.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArticleId = c.Int(nullable: false),
                        CommentText = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArticleId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserArticles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArticleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleID);
            
            CreateTable(
                "dbo.ArticleLikes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArticleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        Country = c.String(),
                        Phone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserArticles", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleLikes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleLikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.UserArticles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.ArticleLikes", new[] { "ArticleId" });
            DropIndex("dbo.ArticleLikes", new[] { "UserId" });
            DropIndex("dbo.UserArticles", new[] { "ArticleId" });
            DropIndex("dbo.UserArticles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComments", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.ArticleLikes");
            DropTable("dbo.Articles");
            DropTable("dbo.UserArticles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ArticleComments");
        }
    }
}
