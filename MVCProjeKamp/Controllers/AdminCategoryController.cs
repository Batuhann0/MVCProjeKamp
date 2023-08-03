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
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory

        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        #region listeleme
        [Authorize(Roles="B")] //SADECE B ROLÜNE SAHİP ADMİN GÖREBİLİR BU SAYFAYI //bunun sayesiyle sisteme otantike(giriş) olmadan görme izni vermez hata verir
        public ActionResult Index()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }
        #endregion

        #region ekleme
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(Category p)
        {
            CategoryValidatior categoryValidatior = new CategoryValidatior();
            ValidationResult results = categoryValidatior.Validate(p);
            if (results.IsValid) //eğer sonuç validasyona uygunsa
            {
                cm.CategoryAddBL(p);
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


        #region SİLME

        public ActionResult DeleteCategory(int id)
        {
            var categoryvalue = cm.GetByID(id);//ıd ye göre getir
            cm.CategoryDelete(categoryvalue);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryvalue = cm.GetByID(id);
            return View(categoryvalue);
        }

        [HttpPost]
        public ActionResult EditCategory(Category p)
        {
            cm.CategoryUpdate(p);
            return RedirectToAction("Index");   
        }


    }
}