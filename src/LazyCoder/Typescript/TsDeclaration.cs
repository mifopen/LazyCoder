namespace LazyCoder.Typescript
{
    public abstract class TsDeclaration
    {
        public TsName Name { get; set; }
        public TsExportKind ExportKind { get; set; }
    }
}
