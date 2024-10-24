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
        void Insert(T theObject);
        void Update(int index, T theObject);
        void Delete(int index);
        void SaveChanges();
    }
}
