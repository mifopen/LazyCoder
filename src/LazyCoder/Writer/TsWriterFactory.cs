using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LazyCoder.Writer
{
    public static class TsWriterFactory
    {
        private static readonly Dictionary<Type, ITsWriter<object>> writersMap = CreateWritersMap();

        public static ITsWriter<object> CreateFor(Type type)
        {
            return writersMap[type];
        }

        private static Dictionary<Type, ITsWriter<object>> CreateWritersMap()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                           .Where(t => t.GetInterfaces()
                                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() ==
                                                  typeof(ITsWriter<>)))
                           .Where(t => !t.IsGenericType ||
                                       t.GetGenericTypeDefinition() != typeof(UntypedTsWriter<>))
                           .Select(x => new
                                        {
                                            Type = x,
                                            GenericType =
                                                x.GetInterfaces().Single().GenericTypeArguments.Single()
                                        })
                           .ToDictionary(x => x.GenericType,
                                         x => CreateUntypedTsWriterFor(x.Type, x.GenericType));
        }

        private static ITsWriter<object> CreateUntypedTsWriterFor(Type writerType, Type genericType)
        {
            var typedWriter = ( (Func<object>)Expression
                                              .Lambda(Expression.New(writerType)).Compile() )();
            var ctorInfo = typeof(UntypedTsWriter<>).MakeGenericType(genericType).GetConstructors().Single();
            var ctor = (Func<ITsWriter<object>>)Expression
                                                .Lambda(Expression.New(ctorInfo, Expression.Constant(typedWriter)))
                                                .Compile();
            return ctor();
        }

        private class UntypedTsWriter<T>: ITsWriter<object>
        {
            private readonly ITsWriter<T> actual;

            public UntypedTsWriter(ITsWriter<T> actual)
            {
                this.actual = actual;
            }

            public void Write(IKeyboard keyboard, object tsThing)
            {
                actual.Write(keyboard, (T)tsThing);
            }
        }
    }
}
