using CommandLine;

namespace XmlSorter.CLI
{
    public class CommandLineOptions
    {
        #region Properties

        [Option('s', Required = true, HelpText = "Source path.")]
        public string SourcePath { get; set; }

        [Option("overwrite", Required = false, HelpText = "Whether to overwrite the source path.")]
        public bool OverwriteSourceFile { get; set; }

        [Option('t', Required = false, HelpText = $"Target path. Required if {nameof(OverwriteSourceFile)} is false.")]
        public string TargetPath { get; set; }

        [Option("sortby-tagname")]
        public bool SortByTagName { get; set; }

        [Option("sortby-attributes")]
        public bool SortBySpecificAttributes { get; set; }

        [Option("sorting-attributes", Separator = ',')]
        public IEnumerable<string> SortingAttributes { get; set; }

        [Option("sa", HelpText = "Sort attributes.")]
        public bool SortAttributes { get; set; }

        #endregion Properties

        #region Constructor

        public CommandLineOptions()
        {
            SortingAttributes = new List<string>();
        }

        #endregion Constructor
    }
}
