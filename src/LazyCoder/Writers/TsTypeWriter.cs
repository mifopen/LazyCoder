using System;
using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsTypeWriter: ITsWriter<TsType>
    {
        public void Write(IKeyboard keyboard,
                          TsType tsType)
        {
            switch (tsType)
            {
                case TsNull _:
                    keyboard.Type("null");
                    break;
                case TsPredefinedType tsPredefinedType:
                    switch (tsPredefinedType.Get())
                    {
                        case TsPredefinedType.Type.Any:
                            keyboard.Type("any");
                            break;
                        case TsPredefinedType.Type.Number:
                            keyboard.Type("number");
                            break;
                        case TsPredefinedType.Type.Boolean:
                            keyboard.Type("boolean");
                            break;
                        case TsPredefinedType.Type.String:
                            keyboard.Type("string");
                            break;
                        case TsPredefinedType.Type.Symbol:
                            keyboard.Type("Symbol");
                            break;
                        case TsPredefinedType.Type.Void:
                            keyboard.Type("void");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(tsPredefinedType),
                                                                  tsPredefinedType.Get(), null);
                    }

                    break;
                case TsStringLiteralType tsStringLiteralType:
                    keyboard.Write(tsStringLiteralType.String);
                    break;
                case TsTypeReference tsTypeReference:
                    Write(keyboard, tsTypeReference);
                    break;
                case TsUnionType tsUnionType:
                    Write(keyboard, tsUnionType);
                    break;
                case TsObjectType tsObjectType:
                    Write(keyboard, tsObjectType);
                    break;
                case TsArrayType tsArrayType:
                    Write(keyboard, tsArrayType.ElementType);
                    keyboard.Type("[]");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsType), tsType.GetType().Name,
                                                          null);
            }
        }

        private void Write(IKeyboard keyboard,
                           TsUnionType tsUnionType)
        {
            keyboard.Type("(");
            for (var i = 0; i < tsUnionType.Types.Length; i++)
            {
                Write(keyboard, tsUnionType.Types[i]);
                if (i != tsUnionType.Types.Length - 1)
                    keyboard.Type(" | ");
            }
            keyboard.Type(")");
        }

        private void Write(IKeyboard keyboard,
                           TsTypeReference tsTypeReference)
        {
            if (tsTypeReference.TypeName.Namespace.Length > 0)
            {
                keyboard.TypeJoin(".",
                                  tsTypeReference.TypeName.Namespace
                                                 .Concat(new[]
                                                         {
                                                             tsTypeReference.TypeName.Identifier
                                                         })
                                                 .ToArray());
            }
            else
            {
                keyboard.Type(tsTypeReference.TypeName.Identifier);
            }

            if (tsTypeReference.TypeArguments.Length > 0)
            {
                keyboard.Type("<");
                for (var i = 0; i < tsTypeReference.TypeArguments.Length; i++)
                {
                    Write(keyboard, tsTypeReference.TypeArguments[i]);
                    if (i != tsTypeReference.TypeArguments.Length - 1)
                        keyboard.Type(", ");
                }

                keyboard.Type(">");
            }
        }

        private void Write(IKeyboard keyboard,
                           TsObjectType tsObjectType)
        {
            using (keyboard.Block())
            {
                foreach (var tsTypeMember in tsObjectType.Members)
                {
                    switch (tsTypeMember)
                    {
                        case TsIndexSignature tsIndexSignature:
                            keyboard.Indent()
                                    .Type("[key: ")
                                    .Write(tsIndexSignature.IndexType)
                                    .Type("]: ")
                                    .Write(tsIndexSignature.ValueType)
                                    .Type(";");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(tsTypeMember),
                                                                  tsTypeMember.GetType().Name,
                                                                  null);
                    }
                }
            }
        }
    }
}
