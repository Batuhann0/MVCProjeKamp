using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class ErrorPageController : Controller
    {
        
        public ActionResult Page403()
        {
            Response.StatusCode = 403; //HTTP yanıtında kullanılacak durum kodunu 403 olarak ayarlar
            Response.TrySkipIisCustomErrors = true; // Bu, uygulamanın kendi hata sayfasının gösterilmesini sağlar.
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}