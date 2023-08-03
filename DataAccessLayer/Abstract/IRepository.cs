using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> //burdaki T type türüm oluyo dışardan gelicek yani entityi karşılıcak
    {
        //CRUD(CREATE - READ - UPDATE - DELETE) OPERASYONLARINI METOT OLARAK TANIMLADIK

        List<T> List(); //listeleme

        T Get(Expression<Func<T, bool>> filter); //ID YE GÖRE GETİRME İŞLEMİ

        void Insert(T p); //ekleme

        void Delete(T p); //silme

        void Update(T p); //güncelleme

        List<T> List(Expression<Func<T, bool>> filter); // şartlı listeleme

        //burdaki metotların hepsi tüm interfaceler için eklendi 
    }
}
