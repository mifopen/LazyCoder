namespace LazyCoder.Typescript
{
    public class TsIndexSignature: TsTypeMember
    {
        public static TsIndexSignature ByString(TsType valueType)
        {
            return new TsIndexSignature
                   {
                       IndexType = TsPredefinedType.String(),
                       ValueType = valueType
                   };
        }

        public static TsIndexSignature ByNumber(TsType valueType)
        {
            return new TsIndexSignature
                   {
                       IndexType = TsPredefinedType.Number(),
                       ValueType = valueType
                   };
        }

        public TsType IndexType { get; private set; }
        public TsType ValueType { get; private set; }
    }
}