using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public static class DefaultCoder
    {
        public static TsFile Rewrite(CsDeclaration csDeclaration)
        {
            return new Coder().Rewrite(csDeclaration);
        }

        private class Coder: BaseCoder
        {
        }
    }
}
