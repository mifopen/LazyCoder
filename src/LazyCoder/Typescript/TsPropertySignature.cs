namespace LazyCoder.Typescript
{
    public class TsPropertySignature: TsTypeMember
    {
        public string Name { get; set; }
        public TsType Type { get; set; }
        public bool Optional { get; set; }
    }
}
