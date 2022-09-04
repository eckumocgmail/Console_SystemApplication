using System;
using System.Collections.Generic;
using System.IO;

using static System.IO.Directory;

public class SearchPathProgram
{
    /// <summary>
    /// Каталоги перечисленные в переменной PATH
    /// </summary>    
    public static IEnumerable<string> GetPathDirectories() =>
        Environment.GetEnvironmentVariable("PATH").Split(";");

    /// <summary>
    /// Поиск файлов
    /// </summary> 
    public static IEnumerable<string> SearchFile(string pattern)
    {
        var results = new List<string>();
        foreach (var dir in GetPathDirectories())
        {
            if (Exists(dir) == false)
                continue;

            try
            {
                results.AddRange(GetFiles(dir, pattern, SearchOption.AllDirectories));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return results;
    }   
}
