using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetByID(string id);
        void Insert(T theObject);
        void Update(T theObject);
        void Delete(T theObject);
        void SaveChanges();
    }
}
