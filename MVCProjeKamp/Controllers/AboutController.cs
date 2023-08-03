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
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var Aboutvalues = abm.GetList();
            return View(Aboutvalues);
        }

        #region Hakkımda Ekleme
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            abm.AboutAdd(about);
            return RedirectToAction("Index");
        } 
        #endregion

        public PartialViewResult AboutPartial() //Hakkımızda Yazı girişi popunu tutan sayfa
        {
            return PartialView();
        }
    }
}