using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
 
/// <summary>
/// Открывает локальный документ(ы)
/// Указатель на документ будет получен через аргументы.    
/// </summary>
public class ExecuterProgram
{


    /// <summary>
    /// В качестве аргумента получает побытия от FileWatcher
    /// </summary>
    /// <param name="args">"ChangeType" "FileName" "FilePath"</param>
    static void Main(string[] args)
    {
        ReadAssociations();
        var dictionary = Get(args, "ChangeType", "FileName", "FilePath");
        switch (dictionary["ChangeType"])
        {
            case "Created":
                break;
            case "Changed":
                break;
            case "Deleted":
                break;
            default:
                throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Разворачивает текст для справочника
    /// </summary>
    private static IDictionary<string, string> Get(string[] args, params string[] keys)
    {
        var resultset = new Dictionary<string, string>();
        if (args.Length != keys.Length)
            throw new ArgumentException("НЕверное колличество аргументов");
        for(int i=0; i<args.Length; i++)
        {
            resultset[keys[i]] = args[i];
        }
        return resultset;
    }


    /// <summary>
    /// Конфигурация исполнителя
    /// </summary>
    static IDictionary<string, string> associations;
    static IDictionary<string, string> associationsDefaults = new Dictionary<string, string>() 
    {
        { "text/html",  "inc-div" },
        { "text/css",   "syntax-interpolation" },
        { "text/*",     "inc-punctuations" }
    };
      

    /// <summary>
    /// Записыванием базовую конфигурацию на диск
    /// </summary>
    static void WriteAssociationDefault()
    {
        if (System.IO.File.Exists(filepath))
            System.IO.File.Delete(filepath);
        System.IO.File.Create(filepath);
        foreach (var kv in associationsDefaults)
        {
            System.IO.File.AppendAllText(filepath, kv.Value+"\r\n");
        }
    }


    // путь к файлу конфигурации, где прописаны прикладные программы 
    // вроде логично файл хранить на рабочем столе (в скрытом виде)
    private static string configutation = "dispatcher.configuration";


    /// путь к рабочему столу
    private static string desctop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


    /// абсолютный путь к файлу конфигурации
    private static string filepath = Path.Combine(desctop, configutation);


    private static void ReadAssociations()
    {

        // если файл конфигурации не найден, то программа пытается создать его в режи
        Dictionary<string, string> readedResult = new Dictionary<string, string>();
        if (System.IO.File.Exists(filepath) == false)
        {
            WriteAssociationDefault();
            associations = associationsDefaults;
        }
        else
        {
            associations.Clear();

            string key = null;
            string value = null;
            int counter = 0;
            foreach (var line in System.IO.File.ReadAllLines(filepath))
            {

                // чётные строки содержат паттерны, нечетные дальнейшие инструкции
                if ((++counter) % 1 == 0)
                {
                    key = line.ToLower();
                }
                else
                {
                    associations[key] = value = line.ToLower();
                }
            }

              
        }


    }
}
 