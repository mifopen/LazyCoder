namespace LazyCoder.CSharp
{
    public class CsField: CsMember
    {
        public CsType Type { get; set; }
        public CsLiteral? Value { get; set; }
    }
}
