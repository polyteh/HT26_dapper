using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL
{
    interface IGeneralService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Delete(T item);
        int Add(T item);
        bool Update(T item);
    }
}
