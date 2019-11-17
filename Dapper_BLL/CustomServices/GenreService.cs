using Dapper_BLL.Entities;
using Dapper_DAL.CustomRepository;
using Dapper_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL.CustomServices
{
    //custom logic for Genre
    //description string has limit: 600 characters max. Add and Update methods should check input data
    public class GenreService : BLLServices<BLLGenre, DALGenre>
    {
        private readonly int _maxDescriptionLenght = 600;
        //create instance of genre repository
        public GenreService()
        {
            this._curDapperRep = new GenreDapperRepository();
        }
        public override int Add(BLLGenre item)
        {
            bool isDescriptionValid = CheckDescriptionLenght(item.Description);
            //check description string lenght and return 0, if string is longer
            if (!isDescriptionValid)
            {
                Console.WriteLine($"Description cant be more than {_maxDescriptionLenght} characters");
                return 0;
            }
            //if string lenght is shoter, than 600 - try to add item to base
            int res = base.Add(item);
            return res;
        }
        public override bool Update(BLLGenre item)
        {
            bool isDescriptionValid = CheckDescriptionLenght(item.Description);
            if (!isDescriptionValid)
            {
                Console.WriteLine($"Description cant be more than {_maxDescriptionLenght} characters");
                return false;
            }
            bool res = base.Update(item);
            return res;
        }
        //check string lenght, return true, if string lenght acceptable
        private bool CheckDescriptionLenght(string descriptionString)
        {
            if (descriptionString.Length > _maxDescriptionLenght)
            {
                return false;
            }
            return true;
        }
    }
}
