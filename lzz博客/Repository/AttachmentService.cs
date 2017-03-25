//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace lzz博客.Repository
//{
//    public class AttachmentService:InterfaceAttachmentService
//    {
//        public IQueryable<Models.Attachment> FindList(Nullable<int> modelID, string owner, string type)
//        {
//            var _attachemts = AttachmentRepository.Entities.Where(a => a.ModelID == modelID);
//            if (!string.IsNullOrEmpty(owner)) _attachemts = _attachemts.Where(a => a.Owner == owner);
//            if (!string.IsNullOrEmpty(type)) _attachemts = _attachemts.Where(a => a.Type == type);
//            return _attachemts;
//        }

//        public IQueryable<Models.Attachment> FindList(int modelID, string owner, string type, bool withModelIDNull)
//        {
//            var _attachemts = CurrentRepository.Entities;
//            if (withModelIDNull) _attachemts = _attachemts.Where(a => a.ModelID == modelID || a.ModelID == null);
//            else _attachemts = _attachemts.Where(a => a.ModelID == modelID);
//            if (!string.IsNullOrEmpty(owner)) _attachemts = _attachemts.Where(a => a.Owner == owner);
//            if (!string.IsNullOrEmpty(type)) _attachemts = _attachemts.Where(a => a.Type == type);
//            return _attachemts;
//        }
//    }
//}