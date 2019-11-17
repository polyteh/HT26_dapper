using Dapper_BLL;
using Dapper_BLL.Entities;
using Dapper_PresentationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ПОЧЕМУ СБОРКА ПРОСИТ ССЫЛКУ НА Dappel_DAL, если она его нигде не использует (в using ее нет)??
namespace Dapper_PresentationLayer
{
    public class PLPublisherWorker : IGeneralWorker<PLPublisher>
    {
        private PublisherService _curPublWorkerService;
        public PLPublisherWorker()
        {
            _curPublWorkerService = new PublisherService();
        }
        public int Add(PLPublisher item)
        {
            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine("Cant add null");
                return 0;
            }
            int addResult = _curPublWorkerService.Add(new BLLPublisher { PublisherName = item.PublisherName, LicenseNumber = item.LicenseNumber });
            if (addResult > 0)
            {
                Console.WriteLine($"Item {item.PublisherName} was added");
            }
            else
            {
                Console.WriteLine($"Item add error");
            }
            return addResult;
        }
        public bool Delete(PLPublisher item)
        {
            if (PLValidation.IsItemNull(item))

            {
                Console.WriteLine("Cant add delete");
                return false;
            }
            bool deleteResult = _curPublWorkerService.Delete(new BLLPublisher { Id = item.Id, PublisherName = item.PublisherName, LicenseNumber = item.LicenseNumber });
            if (deleteResult)
            {
                Console.WriteLine($"Item with Id={item.Id} was deleted");
            }
            else
            {
                Console.WriteLine($"Item delete error");
            }
            return deleteResult;
        }
        public IEnumerable<PLPublisher> GetAll()
        {
            IEnumerable<BLLPublisher> resultBLL = _curPublWorkerService.GetAll();
            var resultPL = resultBLL.Select(item => new PLPublisher { Id = item.Id, PublisherName = item.PublisherName, LicenseNumber = item.LicenseNumber });
            return resultPL;
        }
        public PLPublisher GetById(int id)
        {
            if (!PLValidation.ValidateId(id))
            {
                return null;
            }
            var resultBLL = _curPublWorkerService.GetById(id);
            if (resultBLL != null)
            {
                PLPublisher resultPL = new PLPublisher { Id = resultBLL.Id, PublisherName = resultBLL.PublisherName, LicenseNumber = resultBLL.LicenseNumber };
                return resultPL;
            }
            return null;
        }
        public bool Update(PLPublisher item)
        {

            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine("Cant update null");
                return false;
            }
            bool updResult = _curPublWorkerService.Update(new BLLPublisher { Id = item.Id, PublisherName = item.PublisherName, LicenseNumber = item.LicenseNumber });
            if (updResult)
            {
                Console.WriteLine($"Item with Id={item.Id} was updated");
            }
            else
            {
                Console.WriteLine($"Item update error");
            }
            return updResult;

        }
    }
}
