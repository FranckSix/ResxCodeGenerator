using System.Linq;

namespace ResxCodeGenerator;

internal class ResxInterfaceGenerator (ResxCodeMetadata metadata)
{
   public const string InterfaceTemplate = @"public interface {0}
{{
{1}
}}";

   public const string ParameterTemplate = "\tstring {0} {{ get; }}";

   public string Generate()
   {
      var sources = metadata
         .Resources
         .Select(n => string.Format(ParameterTemplate, n.Name));

      return string.Format(InterfaceTemplate, metadata.InterfaceName, string.Join("\n", sources));
   }
}