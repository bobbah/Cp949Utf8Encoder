// Command-line utility to convert cp949-encoded files to UTF-8

using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine($"Cp949Utf8Encoder v{Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion}");

if (args.Length != 3)
{
    Console.WriteLine("Please call with 3 arguments, [input directory] [pattern] [output directory]");
    return 1;
}

if (!Directory.Exists(args[0]))
{
    Console.WriteLine($"Provided input directory doesn't exist: \"{args[0]}\"");
    return 1;
}

var filesToHandle = Directory.GetFiles(args[0], args[1], SearchOption.AllDirectories);
if (filesToHandle.Length == 0)
{
    Console.WriteLine("Path and pattern matched no files.");
    return 0;
}

// If output directory is non-empty, verify we want to empty it
var outputDir = args[2];
if (Directory.GetFiles(outputDir).Length != 0)
{
    Console.WriteLine($"The output directory ({outputDir}) is not empty. Do you wish to empty it? (y/n)");
    if (!Regex.IsMatch("^[Yy]$", Console.ReadLine() ?? string.Empty))
        return 0;
    
    // Delete directory and contents
    Directory.Delete(outputDir, true);
}

// Ensure output directory is created
Directory.CreateDirectory(outputDir);

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var koreanEncoding = Encoding.GetEncoding(949);

foreach (var path in filesToHandle)
{
    await using var fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
    using var fileContents = new StreamReader(fs, koreanEncoding);

    // Ensure full path exists and write file
    var relativePath = Path.GetRelativePath(args[0], path);
    var newPath = Path.Combine(outputDir, relativePath);
    Directory.CreateDirectory(Path.GetDirectoryName(newPath));
    await using var outputFs = File.Open(newPath, FileMode.CreateNew, FileAccess.ReadWrite);
    await using var output = new StreamWriter(outputFs, Encoding.UTF8);

    // Write file data
    while (!fileContents.EndOfStream)
    {
        await output.WriteLineAsync(await fileContents.ReadLineAsync());
    }

    Console.WriteLine($"Re-encoded \"{relativePath}\"");
}

return 0;