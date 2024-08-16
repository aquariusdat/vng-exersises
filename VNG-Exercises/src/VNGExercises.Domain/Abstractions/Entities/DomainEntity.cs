using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNGExercises.Domain.Abstractions.Entities
{
    public abstract class DomainEntity<TKey> : IDomainEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
