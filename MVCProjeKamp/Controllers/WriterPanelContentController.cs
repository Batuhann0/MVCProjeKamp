using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager Cm = new ContentManager(new EfContentDal());

        Context c = new Context();
        public ActionResult MyContent(string p)
        {
            //SİSTEME GİRİŞ YAPAN YAZARIN MAİL BİLGİSENE GÖRE İÇERİĞİNİ GETİR

            p = (string)Session["WriterMail"]; //Session nesnesi, ASP.NET MVC uygulamalarında kullanılan oturum verilerini saklamak için kullanılır
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();

            var contentValues = Cm.GetListByWriter(writeridinfo).OrderBy(x => x.ContentDate).ToList();
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            string mail = (string)Session["WriterMail"]; 
            var writeridinfo = c.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterID).FirstOrDefault();
            p.WriterID = writeridinfo;
            p.ContentStatus = true;

            Cm.ContentAddBL(p);
            return RedirectToAction("MyContent");
        }

    }
}