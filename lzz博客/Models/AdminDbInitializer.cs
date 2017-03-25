using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Repository;

namespace lzz博客.Models
{
    public class AdminDbInitializer:System.Data.Entity.DropCreateDatabaseAlways<CMSContext>
    {
        protected override void Seed(CMSContext context)
        {
            

            context.Administrators.Add(new Areas.Admin.Models.Administrator { AdminName = "lzzdflg", IsPreset = true, Name = "lzzdflg", PassWord = "Qq313510" });
           
            base.Seed(context);
        }
    }
}