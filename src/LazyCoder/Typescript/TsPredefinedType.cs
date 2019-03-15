namespace LazyCoder.Typescript
{
    public class TsPredefinedType: TsType
    {
        private readonly Type type;

        private TsPredefinedType(Type type)
        {
            this.type = type;
        }

        public Type Get() => type;

        public static TsPredefinedType Any() => new TsPredefinedType(Type.Any);
        public static TsPredefinedType Number() => new TsPredefinedType(Type.Number);
        public static TsPredefinedType Boolean() => new TsPredefinedType(Type.Boolean);
        public static TsPredefinedType String() => new TsPredefinedType(Type.String);
        public static TsPredefinedType Symbol() => new TsPredefinedType(Type.Symbol);
        public static TsPredefinedType Void() => new TsPredefinedType(Type.Void);

        public enum Type
        {
            Any,
            Number,
            Boolean,
            String,
            Symbol,
            Void
        }
    }
}