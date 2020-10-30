using System;
using Microsoft.CodeAnalysis;

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

        public CsType(ITypeSymbol typeSymbol)
        {
            Name = typeSymbol.Name;
            Namespace = typeSymbol.ContainingNamespace.Name;
            TypeSymbol = typeSymbol;
        }

        public string Name { get; }
        public string Namespace { get; }
        public Type OriginalType { get; }
        public ITypeSymbol TypeSymbol { get; }

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

            if (TypeSymbol != null)
            {
                return TypeSymbol.Equals(other.TypeSymbol);
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
            if (TypeSymbol != null)
            {
                return TypeSymbol.GetHashCode();
            }

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