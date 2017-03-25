namespace lzz博客.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class why : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "QQ", c => c.String(maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "QQ", c => c.String(maxLength: 12));
        }
    }
}
