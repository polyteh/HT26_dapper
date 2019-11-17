using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer.Entities
{
    public class PLGenre: IEntityPL
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
    }
}
