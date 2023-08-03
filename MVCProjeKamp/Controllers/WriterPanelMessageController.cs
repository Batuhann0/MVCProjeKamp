using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        MessageManager cm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();


        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];

            var messagelist = cm.GetListInbox(p);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];

            var messagelist = cm.GetListSendInbox(p);
            return View(messagelist);
        }

        public ActionResult GetInboxDetails(int id) //gelen kutusundaki mesaj detayı
        {
            var Messagevalues = cm.GetByID(id);
            return View(Messagevalues);
        }

        public ActionResult GetSendboxDetails(int id) //gönderilen kutusundaki mesaj detayı
        {
            var Messagevalues = cm.GetByID(id);
            return View(Messagevalues);
        }

        #region Yeni Mesaj Oluştur

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messagevalidator.Validate(p);
            string sender = (string)Session["WriterMail"];

            if (results.IsValid) //eğer sonuç validasyona uygunsa
            {
                
                p.SenderMail = sender;
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                cm.MessageAdd(p);
                return RedirectToAction("Sendbox");
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

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
    }
}