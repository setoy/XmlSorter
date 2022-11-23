using CommandLine;
using XmlSorter.Services;

namespace XmlSorter.CLI
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var exitCode = Parser.Default.ParseArguments<CommandLineOptions>(args).MapResult(RunOptionsAndReturnExitCode, HandleParseError);
            Console.WriteLine("Return code: {0}", exitCode);
            return exitCode;
        }

        private static int RunOptionsAndReturnExitCode(CommandLineOptions o)
        {
            try
            {
                var service = new XmlSortService();
                service.Sort(o.SourcePath,
                             o.OverwriteSourceFile,
                             o.TargetPath,
                             o.SortByTagName,
                             o.SortBySpecificAttributes,
                             o.SortingAttributes,
                             o.SortAttributes);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private static int HandleParseError(IEnumerable<Error> errs)
        {
            return HandleParseError(errs.ToList());
        }

        private static int HandleParseError(ICollection<Error> errs)
        {
            var result = -2;
            Console.WriteLine("errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
            {
                result = -1;
            }

            Console.WriteLine("Exit code {0}", result);
            return result;
        }
    }
}
