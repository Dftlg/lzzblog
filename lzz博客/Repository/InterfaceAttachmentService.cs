using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lzz博客.Repository
{
    public interface InterfaceAttachmentService
    {
        /// <summary>
        ///  查找附件列表
        /// </summary>
        /// <param name="modelID">公共模型ID</param>
        /// <param name="owner">所有者</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IQueryable<Models.Attachment> FindList(Nullable<int> modelID, string owner, string type);
        /// <summary>
        /// 查找附件列表
        /// </summary>
        /// <param name="modelID">公共模型ID</param>
        /// <param name="owner">所有者</param>
        /// <param name="type">所有者</param>
        /// <param name="withModelIDNull">包含ModelID为Null的</param>
        /// <returns></returns>
        IQueryable<Models.Attachment> FindList(int modelID, string owner, string type, bool withModelIDNull);
    }
}