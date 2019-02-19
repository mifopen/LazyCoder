using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsTypeWriter : ITsWriter<TsType>
    {
        public void Write(IKeyboard keyboard,
                          TsType tsType)
        {
            keyboard.Type(tsType.Name);
            if (tsType.Generics.Any())
                keyboard.Type("<");
            var generics = tsType.Generics.ToArray();
            for (int i = 0; i < generics.Length; i++)
            {
                keyboard.Write(generics[i]);
                if (i != generics.Length - 1)
                    keyboard.Type(", ");
            }

            if (tsType.Generics.Any())
                keyboard.Type(">");

            if (tsType.Nullable)
                keyboard.Type(" | null");
        }
    }
}
