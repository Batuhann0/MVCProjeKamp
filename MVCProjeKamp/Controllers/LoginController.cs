using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCProjeKamp.Controllers
{
    [AllowAnonymous] //sayfaları veya işlevleri herkese açık hale getirir ve kimlik doğrulamasını gerektirmez.
    public class LoginController : Controller
    {

        #region Admin Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)
        {
            Context c = new Context();
            var admininfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            if (admininfo != null)
            {
                //doğrulama
                FormsAuthentication.SetAuthCookie(admininfo.AdminUserName, false);
                Session["AdminUserName"] = admininfo.AdminUserName;

                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı Kullanıcı Adı veya Şifre Girdiniz";
                return RedirectToAction("Index");
            }
        }
        #endregion

        WriterLoginManager wl = new WriterLoginManager(new EfWriterDal());

        #region Yazar Login

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //Context c = new Context();
            //var writeruserinfo = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
           
            //Login İşleminin Mimariye Taşıdık
            var writeruserinfo = wl.GetWriter(p.WriterMail, p.WriterPassword);

            if (writeruserinfo != null)
            {
                //doğrulama
                FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                Session["WriterMail"] = writeruserinfo.WriterMail;

                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                TempData["ErrorMessage"] = "Hatalı Mail Adresi veya Şifre Girdiniz";
                return RedirectToAction("WriterLogin");
            }
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }

        #endregion

    }
}