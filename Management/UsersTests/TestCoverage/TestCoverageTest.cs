using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using FluentAssertions;
using Xunit.Abstractions;
using Assembly = System.Reflection.Assembly;

namespace UsersTests.TestCoverage;

public class TestCoverageTest : ArchitectureTestCase
{
    private static readonly ArchUnitNET.Domain.Architecture _architecture;
    private static readonly string _filePath;
    private static readonly string _factPath;
    static TestCoverageTest()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string solutionPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\.."));
        string usersAPIPath = Path.Combine(solutionPath, "UsersAPI\\bin\\Debug\\net8.0\\UsersAPI.dll");
        string usersManagementPath = Path.Combine(solutionPath, "UsersManagement\\bin\\Debug\\net8.0\\UsersManagement.dll");
        
        string outputPath = @"..\..\..\TestCoverage\TestCoverageResults.txt";
        string outputPath2 = @"..\..\..\TestCoverage\FactResults.txt";
        _filePath = Path.GetFullPath(outputPath);
        _factPath = Path.GetFullPath(outputPath2);
        // Verificar que los archivos existen
        if (!File.Exists(usersAPIPath))
            throw new FileNotFoundException($"No se encontró el archivo en: {usersAPIPath}");
            
        if (!File.Exists(usersManagementPath))
            throw new FileNotFoundException($"No se encontró el archivo en: {usersManagementPath}");
            
        // Cargar los ensamblados usando rutas de archivo
        _architecture = new ArchLoader()
            .LoadAssemblies(
                Assembly.Load("UsersAPI"),
                Assembly.Load("UsersManagement"),
                Assembly.Load("UsersTests"))
            .Build();
    }
    
    
    [Fact]
    public void Classes_Should_Have_Tests()
    {
        File.WriteAllText(_filePath, string.Empty);
        // Obtener todas las clases de los proyectos
        var allClasses = _architecture.Classes.ToList();
        
        // Filtrar clases de UsersAPI
        var usersApiClasses = allClasses
            .Where(c => c.FullName.StartsWith("UsersAPI."))
            .ToList();
        
        // Filtrar clases de UsersManagement
        var usersManagementClasses = allClasses
            .Where(c => c.FullName.StartsWith("UsersManagement."))
            .ToList();
        
        // Filtrar clases de prueba
        var testClasses = allClasses
            .Where(c => c.FullName.StartsWith("UsersTests."))
            .ToList();
        
        
        var classesWithoutTests = new List<Class>();
        
        // Revisar clases de UsersAPI
        foreach (var clazz in usersApiClasses)
        {
            if (!ShouldIgnore(clazz) && !HasCorrespondingTestClass(clazz, testClasses))
            {
                classesWithoutTests.Add(clazz);
            }
        }
        
        // Revisar clases de UsersManagement
        foreach (var clazz in usersManagementClasses)
        {
            if (!ShouldIgnore(clazz) && !HasCorrespondingTestClass(clazz, testClasses))
            {
                classesWithoutTests.Add(clazz);
            }
        }

        if (classesWithoutTests.Any())
        {
            string message = "Las siguientes clases no tienen pruebas correspondienes:\n    ";
            message += string.Join("\n    ", classesWithoutTests.Select(c => c.FullName));
            this.AppendToFile(_filePath, message);
        }
        else
        {
            this.AppendToFile(_filePath, "Todas las clases tienen sus correspondientes test");
        }
        
        classesWithoutTests.Should().BeEmpty();


    }

    [Fact]
    public void ShouldHaveAtLeastOneFact()
    {
        File.WriteAllText(_factPath, string.Empty);
        var testClasses = _architecture.Classes
            .Where(c => c.FullName.StartsWith("UsersTests.")
            && !c.FullName.Contains("TestCase") && !c.FullName.Contains("Mother")
            && !c.FullName.Contains("Configuration")
            && !c.FullName.Contains("Factory")
            && !c.FullName.Contains("Migrations"))
            .ToList();
        
        var testClassesWithoutFacts = new List<Class>();
            
        foreach (var testClass in testClasses)
        {
            if (!HasFactAttribute(testClass))
            {
                testClassesWithoutFacts.Add(testClass);
            }
        }
        
        if (testClassesWithoutFacts.Any())
        {
            string message = "Las siguientes clases de test no tienen al menos una prueba:\n    ";
            message += string.Join("\n    ", testClassesWithoutFacts.Select(c => c.FullName));
            this.AppendToFile(_factPath, message);
        }
        else
        {
            this.AppendToFile(_factPath, "Todas las clases de prueba tienen al menos una prueba");
        }
        
        testClassesWithoutFacts.Should().BeEmpty();
        
    }
    

    private bool HasCorrespondingTestClass(Class clazz, List<Class> testClasses)
    {
        string className = clazz.Name;
            
        // Buscar si existe una clase de prueba que siga la convención de nomenclatura
        return testClasses.Any(testClass => 
            testClass.Name == className + "Test" || 
            testClass.Name == className + "Tests" ||
            testClass.Name.Contains(className));
    }
    
    
    private bool HasFactAttribute(Class testClass)
    {
        // Verificar si la clase tiene al menos un método con atributo [Fact]
        return testClass.Members.Any(method => 
            method.Attributes.Any(attr => 
                attr.Name == "FactAttribute" || 
                attr.FullName == "Xunit.FactAttribute"));
    }
    
    
    private bool ShouldIgnore(Class clazz)
    {
        // Lista de patrones para clases que típicamente no necesitan tests directos
        string[] ignorePatterns = new[]
        {
            "DTO", 
            //"Request", 
            //"Response", 
            "Config", 
            "Startup", 
            "Program",
            "Exception",
            //"ValueObject",
            "Mapper",
            "Specification"
        };

        // Ignorar interfaces
        if (clazz is Interface)
        {
            return true;
        }
            
        // Ignorar clases anidadas
        if (clazz.IsNested)
        {
            return true;
        }

        // Ignorar clases basadas en patrones de nombre
        foreach (var pattern in ignorePatterns)
        {
            if (clazz.Name.Contains(pattern))
            {
                return true;
            }
        }

        return false;
    }

    
    
}