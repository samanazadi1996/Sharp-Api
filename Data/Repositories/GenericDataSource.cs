using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{

    public interface IGenericDataSource<T> where T : BaseEntity
    {
        Task<bool> AddData(T data, Func<T, bool> action = null);
        bool DeleteData(int id);
        Task<List<T>> GetAllData();
        Task<T> FindData(int id);
        bool UpdateData(int id, T data, Func<T, bool> action = null);
        T FindData(Predicate<T> predicate);
    }

    public class GenericDataSource<T> : IGenericDataSource<T> where T : BaseEntity
    {
        public readonly IUnitOfWork _uow;
        public readonly DbSet<T> TEntity;
        public GenericDataSource(IUnitOfWork uow)
        {
            _uow = uow;
            TEntity = _uow.Set<T>();

        }
        private List<T> _allData;

        private List<T> AllData
        {
            get
            {
                return _allData ?? (AllData = SeedDataSource());
            }
            set { _allData = value; }
        }

        public async virtual Task<List<T>> GetAllData()
        {
            return await TEntity.ToListAsync();
        }

        public async virtual Task<T> FindData(int id)
        {
            return await TEntity.FindAsync(id);
        }

        public virtual T FindData(Predicate<T> predicate)
        {
            return AllData.Find(predicate);
        }

        public virtual bool DeleteData(int id)
        {
            var item = AllData.Find(x => x.Id == id);
            if (item == null)
            {
                return false;
            }

            AllData.Remove(item);
            return true;
        }

        public virtual async Task<bool> AddData(T data, Func<T, bool> action = null)
        {
            _uow.Insert(data);
            return await _uow.SaveChangesAsync() > 0;


        }

        public virtual bool UpdateData(int id, T data, Func<T, bool> action = null)
        {
            var item = AllData
                .Select((pst, index) => new { Item = pst, Index = index })
                .FirstOrDefault(x => x.Item.Id == id);
            if (item == null || id != data.Id)
            {
                return false;
            }

            if (action != null)
            {
                var result = action(data);
                if (!result)
                {
                    return false;
                }
            }

            AllData[item.Index] = data;
            return true;
        }

        protected virtual List<T> SeedDataSource()
        {
            return new List<T>();
        }
    }
}
