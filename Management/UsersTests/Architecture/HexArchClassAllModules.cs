using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Xunit.Abstractions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace UsersTests.Architecture;

public class HexArchClassAllModules : ArchitectureTestCase
{
    
    
    private readonly string _filePathDomain;
    private readonly string _filePathApplication;
    private readonly ITestOutputHelper _output;
    public HexArchClassAllModules(ITestOutputHelper output)
    {
        _output = output;
        string outputDomainPath = @"..\..\..\Architecture\DomainTestResults.txt";
        string outputApplicationPath = @"..\..\..\Architecture\ApplicationTestResults.txt";
        _filePathDomain = Path.GetFullPath(outputDomainPath);
        _filePathApplication = Path.GetFullPath(outputApplicationPath);
    }

    [Fact]
    public void DomainSholdOnlyDependOnThemselveOrShared()
    {
        List<string> modules = DetectModulesFromNamespaces();
        List<string> errors = new List<string>();
        
        File.WriteAllText(_filePathDomain, string.Empty);
        foreach (var module in modules)
        {
            string domainNamespacePattern = $"UsersManagement\\.{module}\\.Domain(\\..+)?";
            
            // Declaración de la regla
            IArchRule domainRule = Types().That().ResideInNamespace(domainNamespacePattern, true)
                .Should().OnlyDependOn(Types().That().ResideInNamespace(domainNamespacePattern, true)
                    .Or().ResideInNamespace("UsersManagement\\.Shared(\\..+)?$", true)
                    .Or().ResideInNamespace("System(\\..+)?$", true)
                    
                );

            try
            {
                domainRule.Check(_architecture);
                _output.WriteLine($"El modulo '{module}' no tiene violaciones de Dominio");
            }
            catch (Exception ex)
            {
                string violationMessage = $"El módulo '{module}' tiene violaciones de Dominio";
                _output.WriteLine(violationMessage);
                AppendToFile(_filePathDomain,violationMessage);
                
                string errorMessage = ex.Message;
                ExtractDependencyFromExceptionTrace(errorMessage, "UsersManagement", _filePathDomain);
                errors.Add(violationMessage);
            }
            
        }

        if (errors.IsNullOrEmpty())
        {
            AppendToFile(_filePathDomain ,"No se han detectado violaciones de Dominio");
        }
        errors.Should().BeEmpty();
    }

    [Fact]
    public void ApplicationShouldOnlyDependOnThemselveOrSharedOrDomain()
    {
        List<string> modules = DetectModulesFromNamespaces();
        List<string> errors = new List<string>();
        
        File.WriteAllText(_filePathApplication, string.Empty);
        foreach (var module in modules)
        {
            string domainNamespacePattern = $"UsersManagement\\.{module}\\.Domain(\\..+)?";
            string applicationNamespacePattern = $"UsersManagement\\.{module}\\.Application(\\..+)?";
            
            _output.WriteLine($"Comprobando dependencias de dominio y aplicación para el módulo {module}");
            IArchRule domainRule = Types().That().ResideInNamespace(applicationNamespacePattern, true)
                .Should().OnlyDependOn(Types().That().ResideInNamespace(applicationNamespacePattern, true)
                    .Or().ResideInNamespace(domainNamespacePattern, true)
                    .Or().ResideInNamespace("UsersManagement\\.Shared(\\..+)?$", true)
                    .Or().ResideInNamespace("System(\\..+)?$", true)
                );

            try
            {
                domainRule.Check(_architecture);
            }
            catch (Exception ex)
            {
                string violationMessage = $"El módulo '{module}' tiene violaciones de Applicación";
                _output.WriteLine(violationMessage);
                AppendToFile(_filePathApplication,violationMessage);
                
                string errorMessage = ex.Message;
                ExtractDependencyFromExceptionTrace(errorMessage, "UsersManagement", _filePathApplication);
                errors.Add(violationMessage);
            }
            
        }

        if (errors.IsNullOrEmpty())
        {
            AppendToFile(_filePathApplication,"No se han detectado violaciones de Aplicacion");
        }
        errors.Should().BeEmpty();
    }
    
    
    
}