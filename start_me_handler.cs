using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

public class StartMeHandler {
    public static void Main(string[] args) {
        if (args.Length != 1) ExitWithError("Expected one argument, a start-me file, got none.");

        if (args[0] == "/say-ok") {
            Console.WriteLine("OK");
            Environment.Exit(0);
        }

        string configFile = Path.Combine(ExecutableDirectoryName(), "start_me_handler.ini");
        Dictionary<string, string> config = ReadConfiguration(configFile);
        if (! config.ContainsKey("dir")) ExitWithError("Missing \"dir\" in configuration file: \"" + configFile + "\"");

        string dir = config["dir"];
        string argFile = args[0];
        string location = ReadTarget(argFile);
        if (IsFishyLocation(location)) ExitWithError("Not a legal location: \"" + location + "\"");
        string file = Path.Combine(config["dir"], location);

        if (! File.Exists(file)) {
            int timeout = 10;
            if (config.ContainsKey("timeout")) {
                if (! Int32.TryParse(config["timeout"], out timeout)) ExitWithError("Given \"timeout\" is not a number: \"" + config["timeout"] + "\"");
            }

            Console.Write("Opening: \"{0}\" ", file);
            for (; ! File.Exists(file) && timeout > 0; timeout--) {
                Thread.Sleep(1000);
                Console.Out.Write(".");
                Console.Out.Flush();
            }

            if (! File.Exists(file)) {
                ExitWithError(" timeout: file not found");
            }
        }

        Process.Start(file);
        File.Delete(argFile);
    }

    public static string ExecutableDirectoryName() {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }

    static Dictionary<string, string> ReadConfiguration(string path) {
        Dictionary<string, string> result = new Dictionary<string, string>();

        try {
            foreach (string line in File.ReadAllLines(path)) {
                Match m = Regex.Match(line, "^(?<key>[a-z]+)\\s*=\\s*(?<val>.*)$");
                if (m.Success) {
                    result[m.Groups["key"].ToString()] = m.Groups["val"].ToString();
                }
            }
        } catch (Exception e) {
            ExitWithError("Can not read configuration file: " + e.Message);
        }

        return result;
    }

    static string ReadTarget(string path) {
        try {
            return File.ReadAllText(path).Trim();
        } catch (Exception e) {
            ExitWithError("Can not read target file: " + e.Message);
            return null; // not reached
        }
    }

    static bool IsFishyLocation(string location) {
        string testDir = Path.GetFullPath(ExecutableDirectoryName());
        return ! Path.GetFullPath(Path.Combine(testDir, location)).StartsWith(testDir);
    }

    static void ExitWithError(string message) {
        Console.Error.WriteLine(message);
        if (Environment.OSVersion.Platform != PlatformID.Unix) {
            Console.Out.Write("\nPress any key to continue . . . ");
            Console.Out.Flush();
            Console.ReadKey(false);
        }
        Environment.Exit(1);
    }
}
