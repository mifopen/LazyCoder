namespace LazyCoder.Typescript
{
    public class TsEnumLiteralType: TsType
    {
        public string Value { get; set; }
        public TsTypeReference EnumType { get; set; }
    }
}
