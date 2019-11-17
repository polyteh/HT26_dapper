using Dapper;
using Dapper_DAL.Entities;
using Dapper_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.CustomRepository
{
    //custom dapper repository for "Game" table
    public class GameDapperRepository : DapperRepository<DALGame>, IGameRepository
    {
        public GameDapperRepository() : base()
        {
            this._tableName = "Game";
        }
        public override int Add(DALGame item)
        {
            //try add new game
            string sqlAddItemString = $"INSERT {_tableName} (GameName,YearOfProduction,GenreId,PublisherID) values (@GameName,@YearOfProduction,@GenreId,@PublisherID)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //try to add item
                try
                {
                    //try to add item
                    var result = connection.Execute(sqlAddItemString, item);
                    return result;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    //catch exception if item with same UNIQUE keys already exists
                    throw new Exception("such item already exists");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<DALGame> GetGameByPublisherLicense(int licenseNumber)
        {
            //SELECT* FROM Publisher pb INNER JOIN Game gm on pb.Id = gm.PublisherID where pb.LicenseNumber = 124365
            //string sqlUpdateItemString = $"SELECT * FROM Game gm INNER JOIN Publisher pb ON gm.PublisherID = pb.Id INNER JOIN Genre gn ON gm.GenreId = gn.Id WHERE pb.LicenseNumber = 124365";
            string sqlUpdateItemString = $"SELECT * FROM Game gm INNER JOIN Publisher pb ON gm.PublisherID = pb.Id INNER JOIN Genre gn ON gm.GenreId = gn.Id WHERE pb.LicenseNumber = {licenseNumber}";
            //string sqlUpdateItemString = $"SELECT *  FROM Game gm INNER JOIN Publisher pb ON gm.PublisherID = pb.Id WHERE pb.LicenseNumber = 124365";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                List<DALGame> newDALList = new List<DALGame>();
                connection.Open();
                var queryResult = connection.Query<DALGame, DALPublisher, DALGenre, DALGame>(sqlUpdateItemString, (game, publisher, genre) =>
                {
                    game.DALPublisher = publisher;
                    game.DALGenre = genre;
                    return game;
                }).ToList();
                //var res = queryResult.ToList();
                return queryResult;
            }
        }

        public override bool Update(DALGame item)
        {
            string sqlUpdateItemString = $"UPDATE {_tableName} SET GameName=@GameName, YearOfProduction=@YearOfProduction,GenreId=@GenreId,PublisherID=@PublisherID WHERE Id=@Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                int updResult;
                connection.Open();
                try
                {
                    updResult = connection.Execute(sqlUpdateItemString, item);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
                return updResult > 0 ? true : false;
            }
        }
    }
}
