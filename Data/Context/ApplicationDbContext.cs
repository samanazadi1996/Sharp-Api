using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SharpApiDb;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }

        static ApplicationDbContext()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.Migrate();
            }

        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public ApplicationDbContext() : base()
        {

        }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);
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

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void MarkAsAdd<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        #endregion IUnitOfWork

    }
}
