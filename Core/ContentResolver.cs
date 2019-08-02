using System.IO;
using System.Reflection;

namespace DotNetNinja.Core
{
    internal class ContentResolver
    {
        public static Stream GetEmbeddedResourceStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            resourceName = FormatResourceName(assembly, resourceName);
            return assembly.GetManifestResourceStream(resourceName);
        }

        public static string GetEmbeddedResource(string resourceName)
        {
            using (var reader = new StreamReader(GetEmbeddedResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }

        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }
    }
}
