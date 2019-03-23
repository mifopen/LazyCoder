using System.Collections.Generic;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public interface ICoder
    {
        IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations);
    }
}
