using Dapper_PresentationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer.Interfaces
{
    public interface IGameWorker
    {
        IEnumerable<PLGame> GetGameByPublisherLicense(int licenseNumber);
    }
}
