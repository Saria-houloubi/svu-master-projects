using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AMW.API.Framework.Extensions
{
    public static class AssemblyExentions
    {
        public static IEnumerable<T> GetInstancesOfType<T>(this Assembly assembly)
        {
            var types = assembly.GetTypes();

            if (types == null)
            {
                return default;
            }

            return types.Where(item =>
            {
                //If we are seaching for implentation of interface then just skip any interface
                if (typeof(T).IsInterface && item.IsInterface)
                {
                    return false;
                }

                return item.GetInterface(typeof(T).Name) != null;
            }).Select(item =>
            {
                return (T)Activator.CreateInstance(item);
            });
        }
    }
}
