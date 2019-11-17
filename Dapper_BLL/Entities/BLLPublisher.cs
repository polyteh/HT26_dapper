using Dapper_BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL.Entities
{
    public class BLLPublisher:IEntityBLL
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public int LicenseNumber { get; set; }
    }
}
