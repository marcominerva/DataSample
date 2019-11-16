﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataSample.DataAccessLayer.Dapper
{
    public interface IDapperContext : IDisposable
    {
        Task<IEnumerable<T>> GetDataAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
            where T : class;

        Task<IEnumerable<TReturn>> GetDataAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null)
            where TFirst : class where TSecond : class where TReturn : class;

        Task<IEnumerable<TReturn>> GetDataAsync<TFirst, TSecond, TThrid, TReturn>(string sql, Func<TFirst, TSecond, TThrid, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null)
            where TFirst : class where TSecond : class where TThrid : class where TReturn : class;

        Task<T> GetObjectAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null) where T : class;

        Task<TReturn> GetObjectAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null)
            where TFirst : class where TSecond : class where TReturn : class;

        Task<TReturn> GetObjectAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id", IDbTransaction transaction = null, CommandType? commandType = null)
            where TFirst : class where TSecond : class where TThird : class where TReturn : class;

        Task<T> GetSingleValueAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        Task InsertAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        Task<int> InsertWithIdentityAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        Task UpdateAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        Task DeleteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        Task ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null);

        IDbTransaction BeginTransaction();
    }
}
