using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class indexController : Controller
    {
        KaoShiEntities db = new KaoShiEntities();
        // GET: index
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.type = db.TypeInfo.ToList();
            return View(db.V_PersonInfo.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Name,string Remark,int ? ID)
        {

            ViewBag.type = db.TypeInfo.ToList();
            var linq = db.V_PersonInfo as IEnumerable<V_PersonInfo>;
            if (ID > 0)
            {
                linq = linq.Where(c => c.TypeInfoId == ID);
            }
            
            if (Name != "" || Name != null)
            {
                linq = linq.Where(c => c.Name.Contains(Name));
            }
            
            if (Remark != "" || Remark != null)
            {
                linq = linq.Where(c => c.Remark.Contains(Remark));
            }
         

            return View(linq.ToList());
        }

  
        public ActionResult delete(int Id)
        {
            var ls = db.PersonInfo.Find(Id);
            db.PersonInfo.Remove(ls);
            if (db.SaveChanges() > 0)
            {
                //return Content("<script>alert('删除成功')</script>");
                return Redirect("/index/index");
            }
            else {
                return Content("<script>alert('删除失败')</script>");
            }
        }
        
    }
}