using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace MVCProjeKamp.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidatior Writervalidator = new WriterValidatior();

        Context c = new Context();

        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            string p = (string)Session["WriterMail"];
            id = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writervalue = wm.GetByID(id);
            return View(writervalue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer w)
        {
            ValidationResult results = Writervalidator.Validate(w);
            if (results.IsValid) //eğer sonuç validasyona uygunsa
            {
                wm.WriterUpdate(w);
                return RedirectToAction("AllHeading","WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors) //resultdan gelen errorlardan döngü oluştur
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        #region Başlıklarım
        public ActionResult MyHeading(string p)
        {

            p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();

            var values = hm.GetListByWriter(writeridinfo);
            return View(values);
        }
        #endregion

        #region Yeni Başlık Ekleme

        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {

                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()

                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string Writermailinfo = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == Writermailinfo).Select(y => y.WriterID).FirstOrDefault();

            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = writeridinfo;
            heading.HeadingStatus = true;
            hm.HeadingAddBL(heading);
            return RedirectToAction("MyHeading");
        }

        #endregion

        #region Başlık Güncelle
        [HttpGet]
        public ActionResult EditHeading(int id)//diğer sayfaya taşıma
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {

                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()

                                                  }).ToList();
            ViewBag.vlc = valueCategory;

            var HeadingValue = hm.GetByID(id); // ID ye göre getir
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading h)//güncelle
        {
            hm.HeadingUpdate(h);
            return RedirectToAction("MyHeading");
        }

        #endregion


        #region Başlık Sil
        public ActionResult DeleteHeading(int id)
        {
            var values = hm.GetByID(id);
            values.HeadingStatus = false;
            hm.HeadingDelete(values);
            return RedirectToAction("MyHeading");

        }
        #endregion


        #region Tüm Başlıklar

        public ActionResult AllHeading(int p = 1) //sayfalamanın 1 den başlacağını belirttik
        {
            var headings = hm.GetList().ToPagedList(p, 4);
            return View(headings);
        }

        #endregion
    }
}