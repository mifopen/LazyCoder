using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsNameWriter: ITsWriter<TsName>
    {
        public void Write(IKeyboard keyboard, TsName tsName)
        {
            keyboard.Type(tsName.Value);
            var generics = tsName.Generics.ToArray();
            if (generics.Length > 0)
            {
                keyboard.Type("<");
                for (int i = 0; i < generics.Length; i++)
                {
                    Write(keyboard, generics[i]);
                    if (i != generics.Length - 1)
                        keyboard.Type(", ");
                }

                keyboard.Type(">");
            }
        }
    }
}
