namespace lzz博客.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "CommonModel_CommonModelId", "dbo.CommonModels");
            DropIndex("dbo.Replies", new[] { "CommonModel_CommonModelId" });
            DropColumn("dbo.Replies", "CommonModel_CommonModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "CommonModel_CommonModelId", c => c.Int());
            CreateIndex("dbo.Replies", "CommonModel_CommonModelId");
            AddForeignKey("dbo.Replies", "CommonModel_CommonModelId", "dbo.CommonModels", "CommonModelId");
        }
    }
}
