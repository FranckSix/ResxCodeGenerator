using System.Text;
using System.Xml.Linq;

namespace ResxCodeGenerator;

internal class ResxClassGenerator(string baseName, IEnumerable<XElement> nodes)
{
   public string Generate()
   {
      var builder = new StringBuilder();

      builder.AppendLine($"public class {baseName} : I{baseName}");
      builder.AppendLine("{");
      builder.AppendLine($"\tprivate readonly IStringLocalizer<{baseName}> _stringLocalizer;");
      nodes.ToList().ForEach(n =>
      {
         var name = n.Attribute("name");
         builder.AppendLine($"\tpublic string {name} => _stringLocalizer[\"{name}\"]?.ToString() ?? \"{n.Value}\";");
      });
      builder.AppendLine("}");

      return builder.ToString();
   }
}