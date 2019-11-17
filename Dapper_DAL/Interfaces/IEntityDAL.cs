using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL
{
    //common interface for all entities (need for generic)
    public interface IEntityDAL
    {
        int Id { get; set; }
    }
}
