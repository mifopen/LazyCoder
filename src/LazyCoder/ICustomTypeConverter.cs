using System;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public interface ICustomTypeConverter
    {
        bool TryConvert(Type csType, out TsType type);
    }
}
