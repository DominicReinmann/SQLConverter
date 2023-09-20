using System.Text.RegularExpressions;

var fileName = args.AsQueryable().FirstOrDefault();
var filePath = (string)null;

if (fileName == null)
{
    Console.WriteLine("Filepath: ");
    filePath = Console.ReadLine().Replace("\"","");
}
else
{
    filePath = Directory.GetCurrentDirectory() + "\\" + fileName;
}

// 
string[] keyWords = { };

void ReplaceInFile(string filePath, string searchText, string replaceText)
{
    StreamReader reader = new StreamReader(filePath);
    string content = reader.ReadToEnd();
    reader.Close();

    content = Regex.Replace(content, $@"(?<!')\b{searchText}\b(?!')", replaceText); // regex for sql reco

    StreamWriter writer = new StreamWriter(filePath);
    writer.Write(content);
    writer.Close();
}

foreach (string keyWord in keyWords)
{
    var wordLower = keyWord.ToLower();
    var wordUpper = keyWord.ToUpper(); 
    ReplaceInFile(filePath, wordLower, wordUpper);
}
foreach (string keyWord in keyWords)
{
    string wordLower = keyWord.Substring(0,1);
    string rest = keyWord.Substring(1);
    wordLower = wordLower.ToUpper();
    string wordUpper = keyWord.ToUpper();
    ReplaceInFile(filePath, wordLower, wordUpper);
}

