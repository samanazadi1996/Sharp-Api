using Common.Utilities;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SharpApiDb;Integrated Security=true;");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }

        #region IUnitOfWork

        DbSet<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            try
            {
                return base.SaveChanges(true);

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        #endregion IUnitOfWork

    }
}
