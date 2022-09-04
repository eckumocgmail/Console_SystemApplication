using System;
using System.IO;

namespace Console1_FileWatch
{
   

    using static System.Console;
    using static System.Threading.Thread;

    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    
    public class FileWatchProgram: ConsoleProgram
    {
        public int Run(params string[] args) 
            => Watch(System.IO.Directory.GetCurrentDirectory(), "*.*");

        /// <summary>
        /// Выпорлнение программы
        /// </summary>        
        public static void Main(string[] args)
        {     
            Watch(@"D:\\", "echo");
        }

        
        /// <summary>
        /// Прослушивание событий измекнения файловых ресурсов и передача
        /// изменений в программе через параметры
        /// </summary>
        private static int Watch(string dir, string cmd)
        {
            Console.WriteLine("Начинаем прослушивать изменения файлов "+dir);
            using (var watcher = new FileSystemWatcher(dir, "*.*"))
            {
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security    
                                 | NotifyFilters.Size;
                 
                watcher.Changed += (sender, evt) => {

                    WriteLine($"{evt.FullPath}");

                    if (evt.FullPath.EndsWith(".cs") 
                    && evt.FullPath.StartsWith(@"D:\System-Config")==false
                    && evt.FullPath.StartsWith(@"D:\\System-Config")== false
                    )
                    {
                        string file1 = evt.FullPath;
                        string file2 = evt.FullPath.Substring(0, 2) + "\\System-Config" + evt.FullPath.Substring(2);              
                        if (System.IO.File.Exists(file2))
                        {
                            var comp = new CompareFilesProgram();
                            var patch = comp.Compare(file1, file2);
                            WriteToFile("history.txt", patch);
                        }
                        else
                        {
                            "Нет файла предыдущей версии".WriteOrangle();
                        }
                        Copy(evt.FullPath, file2);
                        //CmdExec(cmd + $@" ""{evt.ChangeType}"" ""{evt.FullPath}""");
                    } 
                    

                };
           

                watcher.Error += (sender, evt) => {
                    WriteLine();
                };

                watcher.Filter = "*.*";
                watcher.IncludeSubdirectories = true;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
                return 1;  
            }

        }

        private static void Copy(string fullPath, string file2)
        {
            throw new NotImplementedException();
        }

        private static void WriteToFile(string v, CompareFilesProgram.TextChangedEvent patch)
        {
            throw new NotImplementedException();
        }
    }

 
    public static class StringExtensions
    {
        public static string ReplaceAll(this string text, string s1, string s2)
        {
            string p = text;
            while (p.IndexOf(s1) != -1)
            {
                p = p.Replace(s1, s2);
            }
            return p;
        }
    }
 
}