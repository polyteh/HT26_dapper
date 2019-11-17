using AutoMapper;
using Dapper_BLL.Entities;
using Dapper_BLL.Interfaces;
using Dapper_DAL;
using Dapper_DAL.CustomRepository;
using Dapper_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL.CustomServices
{
    public class GameService : BLLServices<BLLGame, DALGame>, IGameService
    {
        //limitations for game definition
        private int _maxGaneNameLenght = 60;
        private int _minYear = 1980;
        public GameService()
        {
            //create instance of game repository
            this._curDapperRep = new GameDapperRepository();
        }
        public override int Add(BLLGame item)
        {
            //year of production and game name validation, return 0, if not valid
            bool isDescriptionValid = (CheckGameYear(item.YearOfProduction) && CheckGameName(item.GameName));
            if (!isDescriptionValid)
            {
                Console.WriteLine($"Wrong GameName or YearOfProduction characters");
                return 0;
            }
            //validation of navigation key (if id of genre and publisher exist for new game), return 0, if not exist
            bool isIdPublisherAndGenreExists = IsIdPublisherAndGenreExists(item);
            if (!isIdPublisherAndGenreExists)
            {
                Console.WriteLine($"Wrong genre of publisher id. Add genre or publisher first");
                return 0;
            }
            //add item
            int res = base.Add(item);
            return res;
        }
        public override bool Update(BLLGame item)
        {
            bool isDescriptionValid = (CheckGameYear(item.YearOfProduction) && CheckGameName(item.GameName));
            if (!isDescriptionValid)
            {
                Console.WriteLine($"Wrong GameName or YearOfProduction characters");
                return false;
            }
            //validation of navigation key (if id of genre and publisher exist for new game)
            bool isIdPublisherAndGenreExists = IsIdPublisherAndGenreExists(item);
            if (!isIdPublisherAndGenreExists)
            {
                Console.WriteLine($"Wrong genre of publisher id. Add genre or publisher first");
                return false;
            }
            bool res = base.Update(item);
            return res;
        }
        private bool CheckGameYear(int gameYear)
        {
            if ((gameYear < _minYear) && (gameYear > DateTime.Now.Year))
            {
                return false;
            }
            return true;
        }
        private bool CheckGameName(string gameName)
        {
            if (gameName.Length > _maxGaneNameLenght)
            {
                return false;
            }
            return true;
        }
        private bool IsIdPublisherAndGenreExists(BLLGame item)
        {
            GenreDapperRepository locGenreDapperRep = new GenreDapperRepository();
            PublisherDapperRepository locPublcDapperRep = new PublisherDapperRepository();
            var gamePublisher = locPublcDapperRep.GetById(item.PublisherID);
            var gameGenre = locGenreDapperRep.GetById(item.GenreId);
            //check genre and publisher, if not exist return false         
            if ((gameGenre == null) || (gamePublisher == null))
            {
                return false;
            }
            return true;
        }
        public IEnumerable<BLLGame> GetGameByPublisherLicense(int licenseNumber)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DALGame, BLLGame>()).CreateMapper();
            return mapper.Map<IEnumerable<DALGame>, List<BLLGame>>(((GameDapperRepository)_curDapperRep).GetGameByPublisherLicense(licenseNumber));
        }
    }
}
