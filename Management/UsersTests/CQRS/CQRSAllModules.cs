using System.Text.RegularExpressions;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using FluentAssertions;
using Google.Protobuf;
using Microsoft.IdentityModel.Tokens;
using UsersTests.Architecture;
using Xunit.Abstractions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace UsersTests.CQRS;

public class CQRSAllModules : ArchitectureTestCase
{
    
    private readonly string _filePath;
    private readonly string rootNamespace;
    public CQRSAllModules()
    {
       string outputPath = @"..\..\..\CQRS\CQRSTestResults.txt";
       rootNamespace = "UsersManagement";
        _filePath = Path.GetFullPath(outputPath);
    }

    [Fact]
    public void AllModulesShouldOnlyDependOnThemselveAndShared()
    {
        // Detectar módulos a partir de los tipos cargados en la arquitectura
        var modules = DetectModulesFromNamespaces();
        List<string> errors = new List<string>();
        
        // Limpiamos el archivo para una nueva ejecucion
        File.WriteAllText(_filePath, string.Empty);
        
        foreach (var module in modules)
        {
            // Construir patrones de expresiones regulares para espacios de nombres
            string moduleNamespacePattern = $"UsersManagement\\.{module}(\\..+)?";
            
            // Excluir el módulo actual de la lista de "otros módulos"
            var otherModules = modules.Where(m => m != module).ToList();
                
            string otherModulesPattern = string.Join("|", otherModules);
            string otherModulesNamespacePattern = $"UsersManagement\\.({otherModulesPattern})(\\..+)?";
            
            // Construir la regla
            IArchRule moduleRule = Types().That().ResideInNamespace(moduleNamespacePattern, true)
                .Should().NotDependOnAny(
                    Types().That().ResideInNamespace(otherModulesNamespacePattern, true));
            
            try
            {
                moduleRule.Check(_architecture);
            }
            catch (Exception ex)
            {
                string violationMessage = $"El módulo '{module}' tiene violaciones:";
                AppendToFile(_filePath,violationMessage);
                
                string errorMessage = ex.Message;
                ExtractDependencyFromExceptionTrace(errorMessage, rootNamespace, _filePath);
                
                errors.Add(violationMessage + "\n" + ex.Message);
            }
            
        }

        if (errors.IsNullOrEmpty())
        {
            AppendToFile(_filePath,"No existen violaciones de CQRS");
        }
        errors.Should().BeEmpty();
    }

    
    
}
