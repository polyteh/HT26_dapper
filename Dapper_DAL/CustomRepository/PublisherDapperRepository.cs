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
    public class PublisherDapperRepository : DapperRepository<DALPublisher>
    {

        public PublisherDapperRepository() : base()
        {
            this._tableName = "Publisher";
        }

        public override bool Update(DALPublisher item)
        {
            string sqlUpdateItemString = $"UPDATE {_tableName} SET PublisherName=@PublisherName, LicenseNumber=@LicenseNumber WHERE Id=@Id";
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
        public override int Add(DALPublisher item)
        {
            string sqlAddItemString = $"INSERT {_tableName} (PublisherName,LicenseNumber) values (@PublisherName,@LicenseNumber)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var result = connection.Execute(sqlAddItemString, new { PublisherName = item.PublisherName, LicenseNumber = item.LicenseNumber });
                    return result;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception("such item already exists");
                }
                finally 
                {
                    connection.Close();
                }

            }
        }
    }
}
