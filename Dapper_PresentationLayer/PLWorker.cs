using Dapper_BLL;
using Dapper_BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer
{
    public class PLWorker
    {
        /// <summary>
        /// print PL entity properties name and value using reflection
        /// </summary>
        /// <param name="itemToPtint"></param>
        public static void PrintItem(IEntityPL itemToPtint)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (itemToPtint!=null)
            {
                foreach (var prop in itemToPtint.GetType().GetProperties())
                {
                    Console.WriteLine($"{prop.Name}={prop.GetValue(itemToPtint)}");
                }
            }
            Console.WriteLine(new string('=',40));
            Console.ForegroundColor = ConsoleColor.White;
        }   
    }
}
