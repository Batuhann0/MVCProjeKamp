using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager Im = new ImageFileManager(new EfImageFileDal());

        public ActionResult Index()
        {
            var ımagevalues = Im.GetList();
            return View(ımagevalues);
        }
    }
}