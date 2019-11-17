using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.Entities
{
    public class DALPublisher: IEntityDAL
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public int LicenseNumber { get; set; }
        public ICollection<DALGame> Games { get; set; }
    }
}
