using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataSample.DataAccessLayer.Dapper
{
    public class DapperContext : IDapperContext
    {
        private IDbConnection connection;
        private IDbConnection Connection
        {
            get
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        public DapperContext(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public Task<IEnumerable<T>> GetDataAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null) where T : class
        {
            return Connection.QueryAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<IEnumerable<TReturn>> GetDataAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null) where TFirst : class where TSecond : class where TReturn : class
        {
            return Connection.QueryAsync(sql, map, param, transaction, splitOn: splitOn, commandType: commandType);
        }

        public Task<IEnumerable<TReturn>> GetDataAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null) where TFirst : class where TSecond : class where TThird : class where TReturn : class
        {
            return Connection.QueryAsync(sql, map, param, transaction, splitOn: splitOn, commandType: commandType);
        }

        public Task<T> GetObjectAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null) where T : class
        {
            return Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public async Task<TReturn> GetObjectAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null) where TFirst : class where TSecond : class where TReturn : class
        {
            var result = await Connection.QueryAsync(sql, map, param, transaction, splitOn: splitOn, commandType: commandType);
            return result.FirstOrDefault();
        }

        public async Task<TReturn> GetObjectAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null) where TFirst : class where TSecond : class where TThird : class where TReturn : class
        {
            var result = await Connection.QueryAsync(sql, map, param, transaction, splitOn: splitOn, commandType: commandType);
            return result.FirstOrDefault();
        }

        public Task<T> GetSingleValueAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public Task InsertAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public Task<int> InsertWithIdentityAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            // Adds the SQL query to get the Id of the last inserted record.
            sql += "; SELECT CAST(SCOPE_IDENTITY() AS INT); ";
            return Connection.QuerySingleAsync<int>(sql, param, transaction, commandType: commandType);
        }

        public Task UpdateAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public Task DeleteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public Task ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        public IDbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction();
        }

        /// <summary>
        /// Close and dispose of the database connection
        /// </summary>
        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            connection.Dispose();
            connection = null;
        }
    }
}
