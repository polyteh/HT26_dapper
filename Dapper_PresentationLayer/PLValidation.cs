using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer
{
    internal static class PLValidation
    {
        //check if id is positive
        internal static bool ValidateId(int id)
        {
            if (id<0)
            {
                Console.WriteLine("Id should be positive");
                return false;
            }
            return true;
        }
        //check if item is null
        internal static bool IsItemNull(IEntityPL item)
        {
            return item == null ? true : false; 
        }
        //check if year is positive
        internal static bool ValidateYear(int year)
        {
            if (year < 0)
            {
                Console.WriteLine("Year should be positive");
                return false;
            }
            return true;
        }
    }
}
