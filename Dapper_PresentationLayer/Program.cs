using Dapper_PresentationLayer.CustomWorkers;
using Dapper_PresentationLayer.Entities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_PresentationLayer
{
    class Program
    {
      
        static void Main(string[] args)
        {
            //create instance for each worker (one worker works with spific entity)
            PLPublisherWorker myPublWorker = new PLPublisherWorker();
            PLGenreWorker myGenreWorker = new PLGenreWorker();
            PLGameWorker myGameWorker = new PLGameWorker();
            try
            {

                //reading from initial DB
                int getbyId = 3;
                //int getbyId = -1;
                //int getbyId = 100;
                Console.WriteLine($"Check for item with Id={getbyId}");
                //var resultById = myPublWorker.GetById(getbyId);
                var resultById = myGenreWorker.GetById(getbyId);
                //var resultById = myGameWorker.GetById(getbyId);
                PLWorker.PrintItem(resultById);

                Console.WriteLine($"Print all publishers");
                var resultAllPubl = myPublWorker.GetAll();
                foreach (var item in resultAllPubl)
                {
                   PLWorker.PrintItem(item);
                }

                //try to add items
                Console.WriteLine("Try to add new genre");
                myGenreWorker.Add(new PLGenre { GenreName = "MMO RPG", Description = "Play with friends in the open world" });
                Console.WriteLine("Try to add new publisher");
                myPublWorker.Add(new PLPublisher { PublisherName = "Betheda", LicenseNumber = 98671234 });
                Console.WriteLine("Try to add new game");
                //change id to obtain a error
                int idPublisher = 1;
                int idGenre = 2;
                myGameWorker.Add(new PLGame { GameName = "Elder Scroll online", YearOfProduction = 2016, PublisherID = idPublisher, GenreId = idGenre });
                myGameWorker.Add(new PLGame { GameName = "Fallout", YearOfProduction = 2014, PublisherID = idPublisher, GenreId = idGenre });

                //try to update
                int updItemId = 2;
                Console.WriteLine($"Try to update game with id={updItemId}. Before update");
                var gameToUpdate = myGameWorker.GetById(updItemId);
                PLWorker.PrintItem(gameToUpdate);
                gameToUpdate.GameName = "Crazy game";
                myGameWorker.Update(gameToUpdate);
                Console.WriteLine($"After update");
                gameToUpdate = myGameWorker.GetById(updItemId);
                PLWorker.PrintItem(gameToUpdate);

                //try to update with wrong parameter
                int genreId = 1;
                int numberOfCharsInDescription = 700;
                var genreById = myGenreWorker.GetById(genreId);
                PLWorker.PrintItem(genreById);
                //error message
                genreById.Description = new string('b', numberOfCharsInDescription);
                myGenreWorker.Update(genreById);

                //try do delete
                int publIdDelete = 5;
                var publToDelete = myPublWorker.GetById(publIdDelete);
                myPublWorker.Delete(publToDelete);
                //try to get dekleted item
                publToDelete = myPublWorker.GetById(publIdDelete);

                int genreIdDelete = 1;
                var genreToDelete = myGenreWorker.GetById(genreIdDelete);
                //try do delete item with relations
                myGenreWorker.Delete(genreToDelete);

            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
