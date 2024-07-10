using Microsoft.CodeAnalysis;
using System.Text;
using System.Xml.Linq;

namespace ResxCodeGenerator;

[Generator]
public class ResxCodeGenerator : ISourceGenerator
{
   public void Initialize(GeneratorInitializationContext context)
   {
   }

   public void Execute(GeneratorExecutionContext context)
   {
      var sourceBuilder = new StringBuilder();

      sourceBuilder.AppendLine($"namespace {context.Compilation.AssemblyName}.Resources;\n");

      try
      {
         // Find all .resx files in the project
         var resxFiles = context.AdditionalFiles.Where(file => file.Path.EndsWith(".resx")).ToList();

         resxFiles.ForEach(r =>
         {
            var text = r.GetText(context.CancellationToken);
            if (text == null) return;

            var baseName = Path.GetFileNameWithoutExtension(r.Path).Split('.').First();
            var document = XDocument.Parse(text.ToString());

            var descendants = document.Descendants("data");

            sourceBuilder.Append(new ResxInterfaceGenerator(baseName, descendants).Generate());
            sourceBuilder.Append(new ResxClassGenerator(baseName, descendants).Generate());

         });
      }
      catch (Exception ex)
      {
         LogError.Log(context, ex);
         throw;
      }
   }
}