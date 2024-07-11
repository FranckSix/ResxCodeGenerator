using System.Linq;

namespace ResxCodeGenerator;

internal class ResxClassGenerator(ResxCodeMetadata metadata)
{
   public const string ClassTemplate = @"public class {0}(IStringLocalizer<{0}> stringLocalizer) : {1}
{{
{2}
}}";

   public const string ParameterTemplate = "\tpublic string {0} => stringLocalizer[\"{0}\"]?.ToString() ?? \"{1}\";";

   public string Generate()
   {
      var sources = metadata
         .Resources
         .Select(n => string.Format(ParameterTemplate, n.Name, n.Value));

      return string.Format(ClassTemplate, metadata.Name, metadata.InterfaceName, string.Join("\n", sources));
   }
}