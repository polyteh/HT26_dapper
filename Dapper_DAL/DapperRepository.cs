using Dapper;
using Dapper_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_DAL
{
    //abstract class of DapperRepositiry
    //for Delete, GetById and GetAll methods all logic is the same, so the can use base methods of abstract class
    //we used _tableName string in the custom realization of the base class to make sql string command for specific table
    public abstract class DapperRepository<T> : ICommonRepository<T> where T : class, IEntityDAL
    {
        protected readonly string _connectionString;
        protected string _tableName;
        //connection to DB
        public DapperRepository()
        {
            _connectionString = @"Data Source=.\IPSQLSERVER;Initial Catalog=ComputerGames;Integrated Security=True";
        }
        //custom logic for each repository, because of each realization has specific column names in DB 
        public abstract int Add(T item);

        //delete item logic. Actually, we need only id to delete item (we received instance, but use id only)
        //return int id item was deleted or throw exception
        public virtual bool Delete(T item)
        {
            string sqlDeleteString = $"DELETE FROM {_tableName} WHERE ID=@ItemId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var result = connection.Execute(sqlDeleteString, new { ItemId = item.Id });
                    return true;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    // i would like make a comment for SqlException or throw general exeption for presentation evel, but can't do it
                    //throw new Exception("Cant delete item with external references");
                   throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            var sql = $"SELECT * FROM {_tableName}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //strongly typed query
                var result = connection.Query<T>(sql);
                connection.Close();
                return result.ToList();
            }
        }
        public virtual T GetById(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE ID={id}";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //strongly typed query
                var result = connection.Query<T>(sql).FirstOrDefault();
                connection.Close();
                if (result != null)
                {
                    return result;
                }
                else return null;
            }
        }
        public abstract bool Update(T item);

    }
}
