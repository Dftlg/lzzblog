using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Models;

namespace lzz博客.Repository
{
    public class AttachmentRepository:RepositoryBase<Attachment>
    {
        public override bool Add(Attachment attachment)
        {
            dbContext.Attachments.Add(attachment);
            return dbContext.SaveChanges() > 0;
        }
        public IQueryable<Attachment> FindList(Nullable<int> modelId,string owner,string type)
        {
            var _attachemts = dbContext.Attachments.Where(a => a.CommonModelId == modelId);
            if (!string.IsNullOrEmpty(owner)) _attachemts = _attachemts.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachemts = _attachemts.Where(a => a.Type == type);
            return _attachemts;
        }
        public IQueryable<Models.Attachment> FindList(int modelId, string owner, string type, bool withModelIDNull)
        {
            var _attachemts = dbContext.Attachments;
            if (withModelIDNull) _attachemts = (System.Data.Entity.DbSet<Attachment>)_attachemts.Where(a => a.CommonModelId == modelId || a.CommonModelId == null);
            else _attachemts = (System.Data.Entity.DbSet<Attachment>)_attachemts.Where(a => a.CommonModelId == modelId);
            if (!string.IsNullOrEmpty(owner)) _attachemts = (System.Data.Entity.DbSet<Attachment>)_attachemts.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachemts = (System.Data.Entity.DbSet<Attachment>)_attachemts.Where(a => a.Type == type);
            return _attachemts;
        }
    }
}