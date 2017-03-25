using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using lzz博客.Models;
using lzz博客.Areas.Admin.Models;

namespace lzz博客.Repository
{
    public class CMSContext:DbContext
    {
        //static CMSContext()
        //{
        //    Database.SetInitializer<CMSContext>(new AdminDbInitializer());
        //}
        public CMSContext():base("name=lzzConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<CommonModel> CommonModels { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<SiteConfig> SiteConfig { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Reply> Replys { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }

    }
}