using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ResxCodeGenerator.Tools;

public static class Extensions
{
   public static void AddLocalizedResources(this IServiceCollection services, Assembly assembly, string @namespace)
   {
      var resourceInterfaces = assembly.GetTypes()
         .Where(t => t.IsInterface && t.Namespace == @namespace)
         .ToList();

      resourceInterfaces.ForEach(ri =>
      {
         var implementation = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Contains(ri));
         if (implementation != null) services.AddSingleton(ri, implementation);
      });
   }
}