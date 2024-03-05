using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Reflections
{
    public static class TypeProvider
    {
        public static Type? GetTypeFromAnyReferencingAssembly(string typeName)
        {
            var referencedAssemblies = Assembly.GetEntryAssembly()?
                .GetReferencedAssemblies()
                .Select(a => a.FullName);

            if (referencedAssemblies == null)
                return null;

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => referencedAssemblies.Contains(a.FullName))
                .SelectMany(a => a.GetTypes().Where(x => x.FullName == typeName || x.Name == typeName))
                .FirstOrDefault();
        }
    }
}
