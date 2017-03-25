using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lzz博客.Areas.Admin.Models;
using lzz博客.Models;
using lzz博客.Repository;
using System.Data.Entity;

namespace lzz博客.Areas.Admin.Repository
{
    public class AdministratorRepository:IAdministrator
    {
        private CMSContext db;
        public bool Add(Administrator admin)
        {
            using (db = new CMSContext())
            {
                if (db.Administrators.Any(a => a.AdminName == admin.AdminName)) return false;
                db.Administrators.Add(admin);
                return db.SaveChanges() > 0;
            }

        }
        /// <summary>
        /// 用户验证【1-成功；-1-用户名不存在；0-密码错误】
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int Authentication(string adminName, string passWord)
        {
            using (db = new CMSContext())
            {
                if (db.Administrators.Any(a => a.AdminName == adminName))
                {
                    var _admin = db.Administrators.SingleOrDefault(a => a.AdminName == adminName);
                    if (_admin.PassWord == passWord) return 1;
                    else return 0;
                }
                else return -1;
            }
        }
        public bool Delete(int adminId)
        {
            using (db = new CMSContext())
            {
                db.Administrators.Remove(db.Administrators.SingleOrDefault(a => a.AdministratorId == adminId));
                return db.SaveChanges() > 0;
            }
        }
        public bool Delete(Administrator admin)
        {
            using (db = new CMSContext())
            {
                db.Administrators.Remove(admin);
                return db.SaveChanges() > 0;
            }
        }
        public Administrator Find(int adminId)
        {
            using (db = new CMSContext())
            {
                return db.Administrators.SingleOrDefault(a => a.AdministratorId == adminId);
            }
        }
        public Administrator Find(string adminName)
        {
            using (db = new CMSContext())
            {
                return db.Administrators.SingleOrDefault(a => a.AdminName == adminName);
            }
        }
        public List<Administrator> Find()
        {
            using (db = new CMSContext())
            {
                return db.Administrators.ToList();
            }
        }
        public bool Modify(Administrator admin)
        {
            using (db = new CMSContext())
            {
                db.Administrators.Attach(admin);
                db.Entry<Administrator>(admin).State =EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
        public bool FindName(string adminName)
        {
            using (db = new CMSContext())
            {
                if (db.Administrators.SingleOrDefault(a => a.AdminName == adminName) == null)
                {
                    return false;
                }
                else
                    return true;
            }          
        }
        
       
    }
}