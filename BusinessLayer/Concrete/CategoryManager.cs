using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
         
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void CategoryAddBL(Category category) //EKLEME
        {
            _categoryDal.Insert(category);
        }

        public void CategoryDelete(Category category) //SİLME
        {
            _categoryDal.Delete(category);
        }

        public void CategoryUpdate(Category category) //güncelleme
        {
            _categoryDal.Update(category);
        }

        public Category GetByID(int id) //ıd ye göre getirme
        {
            return _categoryDal.Get(x => x.CategoryID == id); //ıd den gelen değer eşitmi kontrol ediyoruz eşitse çalışacak
        }

        //ICategoryServicedeki metotu implement ettik
        public List<Category> GetList() //listeleme
        {
            return _categoryDal.List();
        }
    }
}
