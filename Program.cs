using System.Text.RegularExpressions;
internal class Program
{
    static public void ReplaceWords(string filePath, string searchText, string replaceText)
    {
        StreamReader reader = new StreamReader(filePath);
        string content = reader.ReadToEnd();
        reader.Close();

        content = Regex.Replace(content, searchText, replaceText);

        StreamWriter writer = new StreamWriter(filePath);
        writer.Write(content);
        writer.Close();
    }

    static void Main(string[] args)
    {

        string[] keyWords = {"add", "constraint", "all", "alter", "column", "table", "and"
        , "any", "as", "asc", "backup database", "between", "case", "check", "column"
        , "create", "index"};



        Console.WriteLine("FilePath");
        string filePath = Console.ReadLine().Replace("\""," ");

        foreach (string word in keyWords)
        {
            string upperWord = word.ToUpper();
            string lowerWord = word.ToLower();

            ReplaceWords(filePath, lowerWord, upperWord);
        }
    }
}

