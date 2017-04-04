
using System;

namespace TicTacToe.Domain.Core
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public abstract TId Id { get;  }
        
        // EF requires an empty constructor
        protected Entity()
        {
        }

        // For simple entities, this may suffice
        // As Evans notes earlier in the course, equality of Entities is frequently not a simple operation
        public override bool Equals(object otherObject)
        {
            var entity = otherObject as Entity<TId>;
            if (entity != null)
            {
                return Equals(entity);
            }
            return base.Equals(otherObject);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }
            return Id.Equals(other.Id);
        }
    }
    
}
