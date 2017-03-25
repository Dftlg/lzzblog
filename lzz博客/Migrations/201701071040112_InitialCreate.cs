namespace lzz博客.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        AdministratorId = c.Int(nullable: false, identity: true),
                        IsPreset = c.Boolean(nullable: false),
                        AdminName = c.String(nullable: false, maxLength: 20),
                        PassWord = c.String(nullable: false, maxLength: 256),
                        Name = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AdministratorId);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        CommonModelId = c.Int(nullable: false),
                        Source = c.String(maxLength: 255),
                        Author = c.String(maxLength: 50),
                        Intro = c.String(),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.CommonModels", t => t.CommonModelId, cascadeDelete: true)
                .Index(t => t.CommonModelId);
            
            CreateTable(
                "dbo.CommonModels",
                c => new
                    {
                        CommonModelId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Inputer = c.String(nullable: false, maxLength: 255),
                        Model = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 255),
                        Hits = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        PicUrl = c.String(maxLength: 255),
                        CommentStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommonModelId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        ParentId = c.Int(nullable: false),
                        ParentPath = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Model = c.String(),
                        CategoryView = c.String(maxLength: 255),
                        ContentView = c.String(maxLength: 255),
                        Navigation = c.String(maxLength: 255),
                        Order = c.Int(nullable: false),
                        ContentOrder = c.Int(),
                        PageSize = c.Int(),
                        RecordUnit = c.String(maxLength: 255),
                        RecordName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        Extension = c.String(),
                        FilePath = c.String(),
                        Owner = c.String(),
                        Type = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        CommonModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttachmentId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        ReplyContent = c.String(nullable: false),
                        ReplyTime = c.DateTime(nullable: false),
                        AriticlesId = c.Int(nullable: false),
                        User = c.String(),
                        CommonModel_CommonModelId = c.Int(),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.CommonModels", t => t.CommonModel_CommonModelId)
                .Index(t => t.CommonModel_CommonModelId);
            
            CreateTable(
                "dbo.SiteConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        Url = c.String(nullable: false, maxLength: 50),
                        LogoUrl = c.String(maxLength: 255),
                        MetaDescription = c.String(nullable: false, maxLength: 500),
                        MetaKeywords = c.String(nullable: false, maxLength: 500),
                        Copyright = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 12),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserGroupId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 80),
                        Gender = c.Byte(nullable: false),
                        Email = c.String(nullable: false),
                        SecurityQuestion = c.String(nullable: false, maxLength: 20),
                        SecurityAnswer = c.String(nullable: false, maxLength: 20),
                        QQ = c.String(maxLength: 12),
                        Tel = c.String(),
                        Address = c.String(maxLength: 80),
                        PostCode = c.String(),
                        RegTime = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                        RePassword = c.String(),
                        VerificationCode = c.String(maxLength: 6),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "CommonModel_CommonModelId", "dbo.CommonModels");
            DropForeignKey("dbo.Articles", "CommonModelId", "dbo.CommonModels");
            DropForeignKey("dbo.CommonModels", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Replies", new[] { "CommonModel_CommonModelId" });
            DropIndex("dbo.CommonModels", new[] { "CategoryId" });
            DropIndex("dbo.Articles", new[] { "CommonModelId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.SiteConfigs");
            DropTable("dbo.Replies");
            DropTable("dbo.Attachments");
            DropTable("dbo.Categories");
            DropTable("dbo.CommonModels");
            DropTable("dbo.Articles");
            DropTable("dbo.Administrators");
        }
    }
}
