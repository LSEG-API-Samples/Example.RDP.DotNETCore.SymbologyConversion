using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace EDPSymbologyConvertConsoleApp
{
    internal partial class Program
    {
        private class Options
        {
            [Option('i', "universe", SetName = "set1", Required = true, HelpText =
                "List of symbol or item separate by comma. " +
                "For example, -i IBM.N,037833100,TH0001010014 ," +
                "where 037833100 is CUSIP and TH0001010014 is ISIN. ")]
            public string Universe { get; set; }


            [Option('t', "to", SetName = "set1", Required = false,
                HelpText =
                    "List of field to returns from Symbology Convert service. Set it to empty string or not set, " +
                    "the service will return all available fields for the universe.")]
            public string ToEnum { get; set; }

            [Option('j', "jsonfile", SetName = "set2", Required = true,
                HelpText =
                    "Allow the application to read use request parameter from json file instead. " +
                    "If set it override values from -i and -t.")]
            public string JsonRequestFile { get; set; }

            [Option('o', "csvoutput", Default = "./csvoutput.csv", Required = false,
                HelpText =
                    "File name or absolute path to csv file. " +
                    "It allows the application to write an output to CSV file specify in this option.")]

            public string CsvFilePath { get; set; }

            [Option('f', "itemfile", Default = "", Required = false,
                HelpText =
                    "If set, application will read universe list from the file instead. The format is multi-line item.")]

            public string UniverseListFilePath { get; set; }

            [Option('u', "username", Default = "", Required = false, Hidden = true,
                HelpText =
                    "EDP Username, if set, application will use the username specify by this parameter instead.")]

            public string Username { get; set; }

            [Option('p', "password", Default = "", Required = false, Hidden = true,
                HelpText =
                    "EDP Password, if set, application will use the password specify by this parameter instead.")]

            public string Password { get; set; }

            [Option("refreshtoken", Default = "", Required = false, Hidden = true,
                HelpText = "If set, application will get a new access token using the refresh token instead.")]

            public string RefreshToken { get; set; }

            [Option("accesstoken", Default = "", Required = false, Hidden = true,
                HelpText =
                    "If set, application will pass the access token to the Http request but if the token is expired, user need to input a username and password")]

            public string AccessToken { get; set; }

            [Option("authbaseurl", Default = "", Required = false, Hidden = true,
                HelpText =
                    "Authorization Based Url.")]

            public string AuthBaseURL { get; set; }

            [Option("symbologybaseurl", Default = "", Required = false, Hidden = true,
                HelpText =
                    "Symbolgoy Based URL.")]

            public string SymbologyBaseURL { get; set; }

            // Omitting long name, defaults to name of property, ie "--verbose"
            [Option(Default = false, HelpText = "Print additional logs to console output.")]
            public bool Verbose { get; set; }

            [Usage(ApplicationAlias = "EDPSymbologyConvert")]

            public static IEnumerable<Example> Examples => new List<Example>
            {
                new Example("Convert mulitple RICs", new Options {Universe = "IBM.N,MSFT.O,VOD.L"}),
                new Example("Read convert parameter from Json file",
                    new Options {Universe = "IBM.N,037833100,TH0001010014"}),
                new Example("Convert mulitple RICs to ISIN and CommonName",
                    new Options {Universe = "IBM.N,MSFT.O,VOD.L", ToEnum = "ISIN,CommonName"}),
                new Example("Read convert parameter from Json file", new Options {JsonRequestFile = "./request.json"}),
                new Example("Read convert parameter from Json file",
                    new Options {JsonRequestFile = "./request.json", UniverseListFilePath = "./ISINList.txt"})
            };
        }

        public class Config
        {
            public string Universe { get; set; }
            public string ToEnum { get; set; }
            public string JsonRequestFile { get; set; }
            public bool ExportToCSV => !string.IsNullOrEmpty(CsvFilePath) && CsvFilePath.Contains(".csv");
            public string CsvFilePath { get; set; }

            public bool UseJsonRequestFile =>
                !string.IsNullOrEmpty(JsonRequestFile) && JsonRequestFile.Contains(".json");

            public string UniverseListFilePath { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public string AuthBaseURL { get; set; }
            public string SymbologyBaseURL { get; set; }
            public bool Verbose { get; set; }
        }

        private static int RunOptionsAndReturnExitCode(Options options, out Config config)
        {
            config = new Config
            {
                Verbose = options.Verbose,
                Universe = options.Universe,
                CsvFilePath = options.CsvFilePath,
                ToEnum = options.ToEnum,
                JsonRequestFile = options.JsonRequestFile,
                UniverseListFilePath = options.UniverseListFilePath,
                Username = options.Username,
                Password = options.Password,
                AccessToken = options.AccessToken,
                RefreshToken = options.RefreshToken,
                AuthBaseURL=options.AuthBaseURL,
                SymbologyBaseURL=options.SymbologyBaseURL
            };

            return 1;
        }

        /// <summary>
        ///     Used to read password from console input. It will read key until user press enter.
        ///     It also print '*' to screen instead
        /// </summary>
        public static string ReadPassword()
        {
            var passwordStr = new StringBuilder();
            ConsoleKeyInfo keyInput;
            do
            {
                keyInput = Console.ReadKey(true);
                if (keyInput.Key == ConsoleKey.Enter)
                    break;
                if (passwordStr.Length > 0 && keyInput.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                    passwordStr.Remove(passwordStr.Length - 1, 1);
                }

                if (passwordStr.Length < 0 || keyInput.Key == ConsoleKey.Backspace) continue;
                Console.Write("*");
                passwordStr.Append(keyInput.KeyChar);
            } while (true);

            Console.WriteLine();
            return passwordStr.ToString();
        }

        /// <summary>Used to shows command line arguments required by the application with it's default values.</summary>
        public static bool ShowsConfigCommand(string[] arguments, ref Config commandConfig)
        {
            var config = new Config();
            var configResult = Parser.Default.ParseArguments<Options>(arguments)
                .WithParsed(opts => RunOptionsAndReturnExitCode(opts, out config));
            commandConfig = config;
            return configResult.Tag != ParserResultType.NotParsed;
        }
    }
}