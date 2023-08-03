using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ContentManager : IContentService
    {

        IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public void ContentAddBL(Content content)
        {
            _contentDal.Insert(content);
        }

        public void ContentDelete(Content content)
        {
            throw new NotImplementedException();
        }

        public void ContentUpdate(Content content)
        {
            throw new NotImplementedException();
        }

        public Content GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Content> GetList(string p)
        {
            return _contentDal.List(x=>x.ContentValue.Contains(p)); //arama işlemi 
        }

        public List<Content> GetListByHeadingID(int id) //şartlı listeleme
        {
            return _contentDal.List(x => x.HeadingID == id); //başlık ıd si içerik ıd sine eşit olanları geriye döndür
        }

        public List<Content> GetListByWriter(int id) //id ye göre yazarı getircek
        {
            return _contentDal.List(x => x.WriterID == id);
        }
    }
}
 