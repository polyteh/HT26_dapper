using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.Entities
{
    public class DALGame : IEntityDAL
    {
        public int Id { get ; set; }
        public string GameName { get; set; }
        public int YearOfProduction { get; set; }
        public int GenreId { get; set; }
        public DALGenre DALGenre { get; set; }
        public int PublisherID { get; set; }
        public DALPublisher DALPublisher { get; set; }
    }
}
