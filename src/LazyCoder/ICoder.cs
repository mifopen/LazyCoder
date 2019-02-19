using System;
using System.Collections.Generic;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public interface ICoder
    {
        IEnumerable<TsFile> Rewrite(Type[] types);
    }
}
