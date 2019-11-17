using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL
{
    // common interface for all dapper repositories
    public interface ICommonRepository<T>
    {
        T GetById(int id);
        bool Update(T item);
        bool Delete(T item);
        int Add(T item);
        IEnumerable<T> GetAll();
    }
}
