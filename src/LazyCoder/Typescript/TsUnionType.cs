namespace LazyCoder.Typescript
{
    public class TsUnionType: TsType
    {
        public TsUnionType(params TsType[] types)
        {
            Types = types;
        }

        public TsType[] Types { get; }
    }
}
