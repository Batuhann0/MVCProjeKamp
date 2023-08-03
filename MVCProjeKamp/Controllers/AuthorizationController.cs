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
    public class AuthorizationController : Controller
    {
        AdminManager Adm = new AdminManager(new EfAdminDal());

        public ActionResult Index()
        {
            var adminvalues = Adm.GetList();
            return View(adminvalues);
        }

        #region ADMİN EKLE
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            Adm.AdminAdd(admin);
            return RedirectToAction("Index");
        }
        #endregion


        #region YETKİ DEĞİŞTİR
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalue = Adm.GetByID(id);
            return View(adminvalue);
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            Adm.AdminUpdate(p);
            return RedirectToAction("Index");
        } 
        #endregion
    }
}