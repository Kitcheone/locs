using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Locs.Api.Database
{
    static class Program
    {
        static void Main()
        {
            Console.Title = "AliaSQL Database Migrations Visual Studio Runner";
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            var scriptspath = currentDirectory + "\\Scripts\\";
            var deployerpath = scriptspath + "AliaSQL.exe";
            var p = new Process();
            var keySelection = string.Empty;

            while (string.Compare(keySelection, "Exit", StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                if (!string.IsNullOrEmpty(keySelection))
                {
                    Console.WriteLine();
                    var cmdArguments = string.Format("{0} {1} {2} \"{3}", keySelection, GetDatabaseServer(configuration), GetDatabaseName(configuration), scriptspath);
                    p.StartInfo.FileName = deployerpath;
                    p.StartInfo.Arguments = cmdArguments;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();

                    Console.WriteLine(p.StandardOutput.ReadToEnd());
                    Console.WriteLine("Press Any Key to Continue");
                }
                else
                {
                    DrawMenu(configuration);
                }

                var key = Console.ReadKey(true);
                keySelection = GetVerbForKeySelection(key);
            }
        }

        private static void DrawMenu(IConfigurationRoot config)
        {
            Console.Clear();

            Console.WriteLine(" Database: " + GetDatabaseName(config));
            Console.WriteLine(" Server: " + GetDatabaseServer(config));
            Console.WriteLine(" ----------------------------------------------------------------------------");
            Console.WriteLine(" 1. Update");
            Console.WriteLine(" 2. Create");
            Console.WriteLine(" 3. Rebuild");
            Console.WriteLine(" 4. TestData");
            Console.WriteLine(" 5. Baseline");
            Console.WriteLine(" 6. Exit program");
        }

        /// <summary>
        ///returns project name and removes the word ".database."
        /// </summary>
        /// <returns></returns>
        private static string GetDatabaseName(IConfigurationRoot config)
        {
            var databasename = config["DatabaseName"];

            if (string.IsNullOrEmpty(databasename))
            {
                var projectname = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);

                databasename = projectname.Replace("Database.", "").Replace(".Database", "").Replace("Database", "");
            }
            return databasename;
        }

        private static string GetDatabaseServer(IConfigurationRoot config)
        {
            var servername = config["DatabaseServer"];

            if (string.IsNullOrEmpty(servername))
            {
                servername = ".\\sqlexpress";
            }
            return servername;
        }

        private static string GetVerbForKeySelection(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return "Update";
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return "Create";
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return "Rebuild";
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return "TestData";
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    return "Baseline";
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    return "Exit";
                default:
                    return string.Empty;
            }
        }
    }
}
