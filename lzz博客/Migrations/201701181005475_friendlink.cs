namespace lzz博客.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendlink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendLinks",
                c => new
                    {
                        Linkid = c.Int(nullable: false, identity: true),
                        Href = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Linkid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FriendLinks");
        }
    }
}
