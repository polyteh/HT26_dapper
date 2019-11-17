using Dapper_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<DALGame> GetGameByPublisherLicense(int licenseNumber);
    }
}
