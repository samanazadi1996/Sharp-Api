using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());





        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        void Insert<TEntity>(TEntity entity) where TEntity : class;

        int SaveAllChanges();


    }

}