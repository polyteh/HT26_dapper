using Dapper_BLL;
using Dapper_BLL.CustomServices;
using Dapper_BLL.Entities;
using Dapper_PresentationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer
{
    public class PLGenreWorker : IGeneralWorker<PLGenre>
    {
        private GenreService _curGenrelWorkerService;
        public PLGenreWorker()
        {
            _curGenrelWorkerService = new GenreService();
        }
        public int Add(PLGenre item)
        {
            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine("Cant add null");
                return 0;
            }
            int addResult = _curGenrelWorkerService.Add(new BLLGenre { GenreName = item.GenreName, Description = item.Description });
            if (addResult > 0)
            {
                Console.WriteLine($"Item {item.GenreName} was added");
            }
            else
            {
                Console.WriteLine($"Item add error");
            }
            return addResult;
        }
        public bool Delete(PLGenre item)
        {
            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine("Cant delete null");
            }
            bool deleteResult = _curGenrelWorkerService.Delete(new BLLGenre { Id = item.Id, GenreName = item.GenreName, Description = item.Description });
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
        public IEnumerable<PLGenre> GetAll()
        {
            IEnumerable<BLLGenre> resultBLL = _curGenrelWorkerService.GetAll();
            var resultPL = resultBLL.Select(item => new PLGenre { Id = item.Id, GenreName = item.GenreName, Description = item.Description });
            return resultPL;
        }
        public PLGenre GetById(int id)
        {
            //check id
            if (!PLValidation.ValidateId(id))
            {
                return null;
            }
            var resultBLL = _curGenrelWorkerService.GetById(id);
            if (resultBLL != null)
            {
                PLGenre resultPL = new PLGenre { Id = resultBLL.Id, GenreName = resultBLL.GenreName, Description = resultBLL.Description };
                return resultPL;
            }
            return null;
        }
        public bool Update(PLGenre item)
        {
            if (PLValidation.IsItemNull(item))
            {
                Console.WriteLine("Cant update null");
                return false;
            }
                bool updResult = _curGenrelWorkerService.Update(new BLLGenre { Id = item.Id, GenreName = item.GenreName, Description = item.Description });
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
