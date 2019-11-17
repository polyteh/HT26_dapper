using Dapper_BLL.CustomServices;
using Dapper_BLL.Entities;
using Dapper_PresentationLayer.Entities;
using Dapper_PresentationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer.CustomWorkers
{
    public class PLGameWorker : IGeneralWorker<PLGame>, IGameWorker
    {
        private GameService _curGameWorkerService;
        public PLGameWorker()
        {
            _curGameWorkerService = new GameService();
        }
        public int Add(PLGame item)
        {
            //I hate this point, but how can i simplify?

            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine($"Cant add null");
                return 0;
            }
            if ((!PLValidation.ValidateId(item.GenreId)) || (!PLValidation.ValidateId(item.PublisherID)) || (!PLValidation.ValidateYear(item.YearOfProduction)))
            {
                return 0;
            }
            int addResult = _curGameWorkerService.Add(new BLLGame { GameName = item.GameName, YearOfProduction = item.YearOfProduction, GenreId = item.GenreId, PublisherID = item.PublisherID });
            if (addResult > 0)
            {
                Console.WriteLine($"Item {item.GameName} was added");
            }
            else
            {
                Console.WriteLine($"Item add error");
            }
            return addResult;
        }
        public bool Delete(PLGame item)
        {
            if (PLValidation.IsItemNull(item))

            {
                Console.WriteLine($"Cant delete null");
                return false;
            }
            bool deleteResult = _curGameWorkerService.Delete(new BLLGame
            {
                Id = item.Id,
                GameName = item.GameName,
                YearOfProduction = item.YearOfProduction,
                GenreId = item.GenreId,
                PublisherID = item.PublisherID
            });
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
        public IEnumerable<PLGame> GetAll()
        {
            IEnumerable<BLLGame> resultBLL = _curGameWorkerService.GetAll();
            var resultPL = resultBLL.Select(item => new PLGame
            {
                Id = item.Id,
                GameName = item.GameName,
                YearOfProduction = item.YearOfProduction,
                GenreId = item.GenreId,
                PublisherID = item.PublisherID
            });
            return resultPL;
        }
        public PLGame GetById(int id)
        {
            if (!PLValidation.ValidateId(id))
            {
                return null;
            }
            var resultBLL = _curGameWorkerService.GetById(id);
            if (resultBLL != null)
            {
                PLGame resultPL = new PLGame
                {
                    Id = resultBLL.Id,
                    GameName = resultBLL.GameName,
                    YearOfProduction = resultBLL.YearOfProduction,
                    GenreId = resultBLL.GenreId,
                    PublisherID = resultBLL.PublisherID
                };
                return resultPL;
            }
            return null;
        }
        public bool Update(PLGame item)
        {
            if (PLValidation.IsItemNull(item))

            {
                Console.WriteLine($"Cant update null");
                return false;
            }

            if ((!PLValidation.ValidateId(item.GenreId)) || (!PLValidation.ValidateId(item.PublisherID)) || (!PLValidation.ValidateYear(item.YearOfProduction)))
            {
                return false;
            }
            bool updResult = _curGameWorkerService.Update(new BLLGame
            {
                Id = item.Id,
                GameName = item.GameName,
                YearOfProduction = item.YearOfProduction,
                GenreId = item.GenreId,
                PublisherID = item.PublisherID
            });
            return updResult;
        }
        public IEnumerable<PLGame> GetGameByPublisherLicense(int licenseNumber)
        {
            IEnumerable<BLLGame> resultBLL = _curGameWorkerService.GetGameByPublisherLicense(licenseNumber);
            if (resultBLL==null)
            {
                Console.WriteLine($"No games for publisher license = {licenseNumber}");
                return null;
            }
            var resultPL = resultBLL.Select(item => new PLGame
            {
                Id = item.Id,
                GameName = item.GameName,
                YearOfProduction = item.YearOfProduction,
                GenreId = item.GenreId,
                PublisherID = item.PublisherID
            });
            return resultPL;
        }

    }
}
