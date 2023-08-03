using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class // şartımız= T class olmalı
    {
        //bu class crud işlemlerinin gerçekleceği yer Irepositorydeki metotların ne yapacağını belirlediğimiz kısım

        Context c = new Context();
        DbSet<T> _object;

        //T değerine karşılık gelicek sınıfı bulmamız için Constructor(yapıcı metot) oluşturduk
        public GenericRepository()
        {
            _object = c.Set<T>(); //contexte bağlı olarak gönderilen T değerim objeye gönderdik dışardan gelicek değer göre değişecek
        }

        public void Delete(T p)
        {
            var deletedEntity = c.Entry(p);
            deletedEntity.State = EntityState.Deleted;
           // _object.Remove(p);
            c.SaveChanges(); //değişiklikleri kaydet
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter); //bir dizide yada listede sadece bir tane değer döndürmeye yarayan linq metotu
        }

        public void Insert(T p)
        {
            //entity state ile ekleme işlemi
            var addedEntity = c.Entry(p);
            addedEntity.State = EntityState.Added;
            //_object.Add(p);
            c.SaveChanges();
        }

        public List<T> List() //hepsini listele
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter) //şartlı listele
        {
            return _object.Where(filter).ToList(); //filterdan gelen değere göre listeleme yap
        }

        public void Update(T p) //güncelleme
        {
            var updatedEntity = c.Entry(p);
            updatedEntity.State = EntityState.Modified;
            c.SaveChanges();
        }
    }
}
