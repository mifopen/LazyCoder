using System;
using System.Reflection;

namespace LazyCoder.Runner
{
    public class AssemblyReader
    {
        public Type[] Read(string dllPath)
        {
            return Assembly.LoadFile(dllPath).GetTypes();
        }
    }
}
