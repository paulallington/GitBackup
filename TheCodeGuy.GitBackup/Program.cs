using System;
using System.Diagnostics;
using System.IO;

namespace TheCodeGuy.GitBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceDirectory = args.Length == 2 ? args[0] : @"C:\Users\paul\Git";
            var destinationDirectory = args.Length == 2 ? args[1] : @"C:\Users\paul\OneDrive\Git Backup";
            var directories = Directory.GetDirectories(sourceDirectory);
            foreach (var directory in directories)
            {
                Console.Write("Bundling " + new DirectoryInfo(directory).Name + "...");

                var startInfo = new ProcessStartInfo();
                startInfo.WorkingDirectory = directory;
                startInfo.FileName = @"git";
                startInfo.Arguments = "bundle create \"" + destinationDirectory + "\\" + new DirectoryInfo(directory).Name + "\" --all";
                startInfo.CreateNoWindow = true;

                Process proc = Process.Start(startInfo);
                proc.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                proc.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
                proc.WaitForExit();

                Console.WriteLine("Complete!");
            }
        }
    }
}
