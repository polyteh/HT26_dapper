using Dapper;
using Dapper_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL.CustomRepository
{
    //custom dapper repository for "Genre" table
    public class GenreDapperRepository : DapperRepository<DALGenre>
    {
        //set table name worh with in the ctor
        public GenreDapperRepository() : base()
        {
            this._tableName = "Genre";
        }
        //custom logic for add item
        public override int Add(DALGenre item)
        {
            //sql command string
            string sqlAddItemString = $"INSERT {_tableName} (GenreName,Description) values (@GenreName,@Description)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    //try to add item
                    // var result = connection.Execute(sqlAddItemString, new { GenreName = item.GenreName, Description = item.Description });
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
        //custom logic for Update
        public override bool Update(DALGenre item)
        {
            //sql command string
            string sqlUpdateItemString = $"UPDATE {_tableName} SET GenreName=@GenreName, Description=@Description WHERE Id=@Id";
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
