using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.Entities
{
    public class DALGenre : IEntityDAL
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public ICollection<DALGame> Games { get; set; }
    }
}
