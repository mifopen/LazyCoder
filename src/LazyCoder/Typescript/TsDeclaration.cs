namespace LazyCoder.Typescript
{
    public abstract class TsDeclaration
    {
        public string Name { get; set; }
        public TsExportKind ExportKind { get; set; }
    }
}
