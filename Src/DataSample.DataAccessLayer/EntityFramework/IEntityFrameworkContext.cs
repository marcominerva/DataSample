using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataSample.DataAccessLayer.EntityFramework
{
    public interface IEntityFrameworkContext : IDisposable
    {
        IQueryable<T> GetData<T>(bool trackingChanges = false) where T : class;

        void Insert<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task SaveAsync();

        // Avvia una transazione.
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
