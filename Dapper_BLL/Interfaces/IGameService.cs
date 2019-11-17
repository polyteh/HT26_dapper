using Dapper_BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL.Interfaces
{
    public interface IGameService
    {
        IEnumerable<BLLGame> GetGameByPublisherLicense(int licenseNumber);
    }
}
