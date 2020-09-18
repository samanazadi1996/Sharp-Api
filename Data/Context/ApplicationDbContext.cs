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
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SharpApiDb;Integrated Security=true;");
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

        private DbSet<User> users;

        public virtual DbSet<User> GetUsers()
        {
            return users;
        }

        public virtual void SetUsers(DbSet<User> value)
        {
            users = value;
        }

        private DbSet<Role> roles;

        public virtual DbSet<Role> GetRoles()
        {
            return roles;
        }

        public virtual void SetRoles(DbSet<Role> value)
        {
            roles = value;
        }

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
