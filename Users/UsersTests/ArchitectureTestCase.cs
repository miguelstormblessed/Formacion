using System.Reflection;
using System.Text.RegularExpressions;
using ArchUnitNET.Loader;

namespace UsersTests;

public class ArchitectureTestCase
{
    public static readonly ArchUnitNET.Domain.Architecture _architecture = new ArchLoader()
        .LoadAssemblies(Assembly.Load("Users"))
        .Build();
    
    
    public List<string> DetectModulesFromNamespaces()
    {
        // Detectar módulos examinando los espacios de nombres de los tipos cargados
        var moduleSet = new HashSet<string>();
        
        // Obtener todos los tipos de la arquitectura
        var allTypes = _architecture.Types;
        
        // Regex para extraer el nombre del módulo desde el espacio de nombres
        var namespaceRegex = new Regex(@"^Users\.([^\.]+)");
        
        foreach (var type in allTypes)
        {
            var match = namespaceRegex.Match(type.FullName);
            if (match.Success)
            {
                string moduleName = match.Groups[1].Value;
                // Excluir "Shared" y otros espacios de nombres que no sean módulos
                if (moduleName != "Shared" && moduleName != "bin" && moduleName != "obj")
                {
                    moduleSet.Add(moduleName);
                }
            }
        }
        
        return moduleSet.ToList();
    }
    
    protected void AppendToFile(string path, string message)
    {
        File.AppendAllText(path, message + Environment.NewLine);
    }

    protected void ExtractDependencyFromExceptionTrace(string message, string rootNamespace, string path)
    {
        var matches = Regex.Matches(message, $@"{Regex.Escape(rootNamespace)}(\.[^\s,]+)+").ToList();
        
        if (matches.Count > 0)
        {
            var namespaceUnderTest = matches.ElementAt(0);
            matches.RemoveAt(0);
            
            AppendToFile(path,$@"  - {namespaceUnderTest} depende de: ");
            
            foreach (var match in matches)
            {
                string dependency = match.Value;
                AppendToFile(path, $"      {dependency}");
            }
        }
    }
}