using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer.Entities
{
    public class PLGame : IEntityPL
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public int YearOfProduction { get; set; }
        public int GenreId { get; set; }
        public int PublisherID { get; set; }
    }
}
