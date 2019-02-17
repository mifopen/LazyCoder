using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LazyCoder.Runner.Writer
{
    public static class TsWriterFactory
    {
        private static readonly Dictionary<Type, object> writersMap;

        static TsWriterFactory()
        {
            writersMap = Assembly.GetExecutingAssembly().GetTypes()
                                 .Where(t => t.GetInterfaces()
                                              .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() ==
                                                        typeof(ITsWriter<>)))
                                 .Select(x => new
                                              {
                                                  Type = x,
                                                  GenericType =
                                                      x.GetInterfaces().Single().GenericTypeArguments.Single()
                                              })
                                 .ToDictionary(x => x.GenericType,
                                               x => ( (Func<object>)Expression
                                                                    .Lambda(Expression.New(x.Type)).Compile() )
                                                   ());
        }

        public static ITsWriter<T> CreateFor<T>()
        {
            return (ITsWriter<T>)writersMap[typeof(T)];
        }
    }
}
