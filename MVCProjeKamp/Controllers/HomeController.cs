using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKamp.Controllers
{
    public class HomeController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        MessageManager ms = new MessageManager(new EfMessageDal());

        [AllowAnonymous]

        public ActionResult HomePage()
        {
            var headingvalues = hm.GetList();
            var writervalues = wm.GetList();
            var messagevalues = ms.GetList();

            //Tuple, birden fazla değeri gruplamak için kullanılan bir veri yapısıdır. 
            //Tuple kullanarak farklı model verilerini birleştirebilir

            var combined = Tuple.Create(headingvalues, writervalues, messagevalues);

            return View(combined);
        }
    }
}