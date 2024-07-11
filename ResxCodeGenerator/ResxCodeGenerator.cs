using System;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Microsoft.CodeAnalysis.Text;

namespace ResxCodeGenerator;

[Generator]
public class ResxCodeGenerator : ISourceGenerator
{
   public static string FileTemplate = @"// GENERATED CLASS
using Microsoft.Extensions.Localization;
using System.Text;

namespace {0};
{1}
{2}";

   public void Initialize(GeneratorInitializationContext context)
   {
   }

   public void Execute(GeneratorExecutionContext context)
   {
      try
      {
         // Find all .resx files in the project
         var resxFiles = context.AdditionalFiles.Where(file => file.Path.EndsWith(".resx")).ToList();

         resxFiles.ForEach(r =>
         {
            var text = r.GetText(context.CancellationToken);
            if (text == null) return;

            var baseName = Path.GetFileNameWithoutExtension(r.Path).Split('.').First();
            var metadata = new ResxCodeMetadata(baseName, context.Compilation.AssemblyName, XDocument.Parse(text.ToString()).Descendants("data"));

            var source = string.Format(
               FileTemplate,
               metadata.Namespace,
               new ResxInterfaceGenerator(metadata).Generate(),
               new ResxClassGenerator(metadata).Generate());

            if (!context.CancellationToken.IsCancellationRequested)
               context.AddSource(baseName + "Localisation", SourceText.From(source, Encoding.UTF8));
         });
      }
      catch (Exception ex)
      {
         LogError.Log(context, ex);
         throw;
      }
   }
}