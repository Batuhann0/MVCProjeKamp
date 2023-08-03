using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        #region Listeleme
        public ActionResult Index()
        {
            var Headingvalues = hm.GetList();
            return View(Headingvalues);
        }
        #endregion

        #region Raporlama
        public ActionResult HeadingReport()
        {
            var Headingvalues = hm.GetList();
            return View(Headingvalues);
        }
        #endregion

        #region Başlık Ekleme
        [HttpGet]
        public ActionResult AddHeading()
        {
            //dropdownliste kategori taşıma işlemi
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {

                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()

                                                   }).ToList();

            //dropdownliste yazar taşıma işlemi
            List<SelectListItem> valueWriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {

                                                    Text = x.WriterName + " " + x.WriterSurname,
                                                    Value = x.WriterID.ToString()

                                                }).ToList();


            ViewBag.vlc = valueCategory; //viewe taşıma işlemi
            ViewBag.vwc = valueWriter;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAddBL(heading);
            return RedirectToAction("Index");
        }
        #endregion

        #region Güncelleme

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
            return RedirectToAction("Index");
        }
        #endregion

        #region BAŞLIK SİL
        public ActionResult DeleteHeading(int id)
        {
            //SİL butonuna tıklanınca HeadingStatusü false yapıcak silmicek
            var Headingvalue = hm.GetByID(id);
            Headingvalue.HeadingStatus = false;
            hm.HeadingDelete(Headingvalue);
            return RedirectToAction("Index");
        } 
        #endregion
    }
}