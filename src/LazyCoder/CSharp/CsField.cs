namespace LazyCoder.CSharp
{
    public class CsField: CsTypeMember
    {
        public CsType Type { get; set; }
        public CsLiteral? Value { get; set; }
    }
}
