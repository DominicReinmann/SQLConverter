using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using CommandLine;
using SQLConverter;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 1 && !args[0].StartsWith("-"))
        {
            args = new[] { "-p", args[0] };
        }
        var result = Parser.Default.ParseArguments<ConverterOptions>(args);
        return await result.MapResult(
            options => Execute(options),
            errors => Task.FromResult(DisplayHelp()));
    }

    private static async Task<int> Execute(ConverterOptions options)
    {
        try
        {
            if (options.HelpText)
            {
                DisplayHelp();
                options = await GetOptionsFromInput(options);
                return await Execute(options);
            }

            if (string.IsNullOrEmpty(options.FilePath) && !options.GetAllFiles)
            {
                Console.WriteLine("Please enter a Filepath or Parameters.");
                Console.Write("see -h /--h for help: ");
                options = await GetOptionsFromInput(options);
                return await Execute(options);
            }

            if (string.IsNullOrEmpty(options.FilePath))
            {
                var sqlFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sql");
                if (sqlFiles.Length == 1)
                {
                    options.FilePath = sqlFiles[0];
                }
                else if (sqlFiles.Length > 1 && !options.GetAllFiles)
                {
                    Console.WriteLine("Error: Multiple SQL files found in the active directory.");
                    return 1;
                }
                else if (options.GetAllFiles)
                {
                    foreach (var file in sqlFiles)
                    {
                        ConvertSqlFile(file);
                    }
                    return 0;
                }
            }

            if (File.Exists(options.FilePath))
            {
                ConvertSqlFile(options.FilePath);
                return 0;
            }
            else
            {
                Console.WriteLine("Error: File not found.");
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return 1;
        }
    }

    private static async Task<ConverterOptions> GetOptionsFromInput(ConverterOptions options)
    {
        var input = Console.ReadLine();
        string[] inputArgs = input.Split(' ');
        if (inputArgs.Length == 1 && !inputArgs[0].StartsWith("-"))
        {
            inputArgs = new[] { "-p", inputArgs[0].Replace("\"", "") };
        }
        options = Parser.Default.ParseArguments<ConverterOptions>(inputArgs).MapResult(
            parsedOptions => parsedOptions,
            _ => options);
        return options;
    }

    private static int DisplayHelp()
    {
        Console.WriteLine("Please provide the correct parameters.");
        Console.WriteLine("--a: Get all SQL files in directory");
        Console.WriteLine("--p <FilePath>: Path to the SQL file to be converted");
        Console.WriteLine("--h: Show help text");
        return 1;
    }

    static void ConvertSqlFile(string filePath)
    {
        try
        {
            var fileKeyWords = File.ReadAllText("C:\\var\\SqlConverter\\keyWords.json");
            string[] keyWords = JsonSerializer.Deserialize<string[]>(fileKeyWords);

            void ReplaceInFile(string filePath, string searchText, string replaceText)
            {
                var content = File.ReadAllText(filePath);

                var sections = Regex.Split(content, @"/\/\*.*\n*\*\//", RegexOptions.Singleline);

                for (int i = 0; i < sections.Length; i++)
                {
                    if (!sections[i].StartsWith("/*"))
                    {
                        sections[i] = Regex.Replace(sections[i], $@"(?<!')\b(?i){searchText}\b(?!')", replaceText);
                    }
                }
                content = string.Join("", sections);
                File.WriteAllText(filePath, content);
            }

            foreach (string keyWord in keyWords)
            {
                var wordLower = keyWord.ToLower();
                var wordUpper = keyWord.ToUpper();
                ReplaceInFile(filePath, wordLower, wordUpper);
            }

            Console.WriteLine("Successfully Converted");
            Console.WriteLine("version 1.1");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error converting file: " + ex.Message);
        }
    }
}
