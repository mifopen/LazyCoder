namespace LazyCoder.CSharp
{
    public abstract class CsClassMember
    {
        public string Name { get; set; }
        public bool IsStatic { get; set; }
        public CsAccessModifier AccessModifier { get; set; }
    }
}