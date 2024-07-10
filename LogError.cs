using Microsoft.CodeAnalysis;

namespace ResxCodeGenerator;

internal static class LogError
{
   public static void Log(GeneratorExecutionContext context, Exception ex)
   {
      // Créer un diagnostic pour l'erreur
      var descriptor = new DiagnosticDescriptor(
         id: "GEN001",
         title: "Error code generation",
         messageFormat: "Exception was thrown : {0}",
         category: "SourceGenerator",
         DiagnosticSeverity.Error,
         isEnabledByDefault: true);

      var diagnostic = Diagnostic.Create(descriptor, Location.None, ex.ToString());

      // Reporter le diagnostic à Visual Studio
      context.ReportDiagnostic(diagnostic);
   }
}