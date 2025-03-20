using ArchUnitNET.Domain;
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
    private readonly string _rootNamespace;
    public HexArchClassAllModules(ITestOutputHelper output)
    {
        _output = output;
        string outputDomainPath = @"..\..\..\Architecture\DomainTestResults.txt";
        string outputApplicationPath = @"..\..\..\Architecture\ApplicationTestResults.txt";
        _rootNamespace = "Users";
        _filePathDomain = Path.GetFullPath(outputDomainPath);
        _filePathApplication = Path.GetFullPath(outputApplicationPath);
    }

    [Fact]
    public void DomainSholdOnlyDependOnThemselveOrShared()
    {
        CheckLayerDependencies(
            "Domain",
            _filePathDomain,
            (module) => $"{_rootNamespace}\\.{module}\\.Domain(\\..+)?",
            (module, domainPattern) =>
                Types().That().ResideInNamespace(domainPattern, true)
                    .Or().ResideInNamespace($"{_rootNamespace}\\.Shared(\\..+)?$", true)
                    .Or().ResideInNamespace("System(\\..+)?$", true));
    }

    [Fact]
    public void ApplicationShouldOnlyDependOnThemselveOrSharedOrDomain()
    {
        CheckLayerDependencies(
            "Aplication",
            _filePathApplication,
            (module) => $"{_rootNamespace}\\.{module}\\.Application(\\..+)?",
            (module, applicationPattern) => 
                Types().That().ResideInNamespace(applicationPattern, true)
                    .Or().ResideInNamespace($"{_rootNamespace}\\.{module}\\.Domain(\\..+)?", true)
                    .Or().ResideInNamespace($"{_rootNamespace}\\.Shared(\\..+)?$", true)
                    .Or().ResideInNamespace("System(\\..+)?$", true)
        );
    }

    private void CheckLayerDependencies(
        string layerName,
        string outputFilePath,
        Func<string, string> getNamespacePattern,
        Func<string, string, IObjectProvider<IType>> getDependenctyPredicate)
    {
        List<string> modules = DetectModulesFromNamespaces();
        List<string> errors = new List<string>();
        
        File.WriteAllText(outputFilePath, string.Empty);

        foreach (var module in modules)
        {
            string namespacePattern = getNamespacePattern(module);

            IArchRule rule = Types().That().ResideInNamespace(namespacePattern, true)
                .Should().OnlyDependOn(getDependenctyPredicate(module, namespacePattern));

            try
            {
                rule.Check(_architecture);
            }
            catch (Exception ex)
            {
                string violationMessage = $"El módulo '{module}' tiene violaciones de {layerName}";
                AppendToFile(outputFilePath, violationMessage);
                
                ExtractDependencyFromExceptionTrace(ex.Message, _rootNamespace, outputFilePath);
                errors.Add(violationMessage);
            }
        }

        if (errors.IsNullOrEmpty())
        {
            AppendToFile(outputFilePath, $"No se han detectado violaciones de {layerName}");
        }
        
        errors.Should().BeEmpty();
    }
    
    
}