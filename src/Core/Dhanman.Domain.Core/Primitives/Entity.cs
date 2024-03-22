using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.Domain.Core.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; protected set; }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public bool Equals(Entity? other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || Id == other.Id;
        }
        
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (!(obj is Entity other))
            {
                return false;
            }

            if (Id == Guid.Empty || other.Id == Guid.Empty)
            {
                return false;
            }

            return Id == other.Id;
        }
        
        public override int GetHashCode() => Id.GetHashCode() * 41;
    }
}
