using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ResxCodeGenerator;

internal class ResxCodeMetadata(string fileName, string @namespace, IEnumerable<XElement> resources)
{
   public string Name { get; } = fileName;
   public string InterfaceName => $"ILocalized{Name}Resources";
   public string Namespace { get; } = $"{@namespace}.Resources";
   public IEnumerable<ResourceDefinition> Resources { get; } = resources.Select(r => new ResourceDefinition
   {
      Name = r.Attribute("name").Value,
      Value = r.Element("value").Value
   });
}