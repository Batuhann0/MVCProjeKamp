using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramwork;
using EntityLayer.Concreate;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidatior Writervalidator = new WriterValidatior();
       
        #region listeleme
        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        } 
        #endregion

        #region Yazar EKLEME
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer w)
        {
            ValidationResult results = Writervalidator.Validate(w);
            if (results.IsValid) //eğer sonuç validasyona uygunsa
            {
                wm.WriterAdd(w);
                return RedirectToAction("Index");
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
        #endregion

        #region Yazar GÜNCELLEME
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetByID(id); // ıd ye göre sayfaya taşıma işlemi
            return View(writervalue);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer w)
        {
            ValidationResult results = Writervalidator.Validate(w);
            if (results.IsValid) //eğer sonuç validasyona uygunsa
            {
                wm.WriterUpdate(w);
                return RedirectToAction("Index");
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
        #endregion
    }
}