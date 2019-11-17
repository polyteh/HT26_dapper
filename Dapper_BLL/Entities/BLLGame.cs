using Dapper_BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL.Entities
{
    public class BLLGame : IEntityBLL
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public int YearOfProduction { get; set; }
        public int GenreId { get; set; }
        public int PublisherID { get; set; }

    }
}
