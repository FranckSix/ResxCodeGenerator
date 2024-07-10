using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ResxCodeGenerator
{
   internal class ResxInterfaceGenerator (string baseName, IEnumerable<XElement> nodes)
   {
      public string Generate()
      {
         var builder = new StringBuilder();

         builder.AppendLine($"public interface I{baseName}");
         builder.AppendLine("{");
         nodes.ToList().ForEach(n => builder.AppendLine($"\tstring {n.Attribute("name")} {{ get; }}"));
         builder.AppendLine("}\n");

         return builder.ToString();
      }
   }
}
