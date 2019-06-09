using System;

namespace LazyCoder.CSharp
{
    public class CsType: IEquatable<CsType>
    {
        public CsType(Type originalType)
        {
            Name = originalType.Name;
            Namespace = originalType.Namespace;
            OriginalType = originalType;
        }

        public string Name { get; }
        public string Namespace { get; }
        public Type OriginalType { get; }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(CsType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return OriginalType == other.OriginalType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(CsType))
            {
                return false;
            }

            return Equals((CsType)obj);
        }

        public override int GetHashCode()
        {
            return OriginalType.GetHashCode();
        }

        public static bool operator ==(CsType left, CsType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CsType left, CsType right)
        {
            return !Equals(left, right);
        }
    }
}
