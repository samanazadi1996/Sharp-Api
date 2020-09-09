using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public interface IEntity
    {
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        protected BaseEntity()
        {
            IsActive = true;
            InsertDateTime = DateTime.Now;
        }
        [Key]
        [ScaffoldColumn(false)]
        public TKey Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDateTime { get; set; }

    }
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
