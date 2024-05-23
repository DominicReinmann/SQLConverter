using CommandLine;

namespace SQLConverter
{
    internal class ConverterOptions
    {
        [Option('p')]
        public string FilePath { get; set; }

        [Option('a', Default = false)]
        public bool GetAllFiles { get; set; }

        [Option('h', Default = false)]
        public bool HelpText { get; set; }
    }
}
