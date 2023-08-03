using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class ContentController : Controller
    {
        ContentManager Cm = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllContent(string p="") //arama kutusu boş olunca bütün içeriği göstermesi için
        {
            var values = Cm.GetList(p);
            return View(values);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentValues = Cm.GetListByHeadingID(id).OrderBy(x => x.ContentDate).ToList();
            return View(contentValues);
        }
    }
}