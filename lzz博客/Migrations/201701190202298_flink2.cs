namespace lzz博客.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flink2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FriendLinks", "Href", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.FriendLinks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FriendLinks", "Name", c => c.String());
            AlterColumn("dbo.FriendLinks", "Href", c => c.String());
        }
    }
}
