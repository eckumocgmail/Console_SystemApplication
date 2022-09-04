using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.IO.Directory;
using static Newtonsoft.Json.JsonConvert;

/// <summary>
/// Отвечает за операции с текстовыми файлами
/// </summary>
public class TextFileProgram
{
    /// <summary>
    /// Время последнеднего обновления файла
    /// </summary>
    public static DateTime GetUpdateTime(string FileName)
        => new FileInfo(FileName).LastWriteTime;

    /// <summary>
    /// Запись данных в текстовом формате
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="FileName">Имя файла</param>
    /// <param name="GetValue">Функция предоставляющая данные</param>
    /// <returns>ссылка на объект</returns>
    public static T SaveText<T>(string FileName, Func<T> GetValue)
    {
        T item = GetValue();
        System.IO.File.WriteAllText(FileName, SerializeObject(item));
        return item; 
    }
            

    /// <summary>
    /// Загрузка данных из текстового файла
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="FileName">Имя файла</param>
    /// <param name="GetDefaultValue">Функция получения значения по-умолчанию</param>
    /// <returns></returns>
    public static T LoadText<T>(string FileName, Func<T> GetDefaultValue)                    
        =>  System.IO.File.Exists(FileName)?
            DeserializeObject<T>(System.IO.File.ReadAllText(FileName)):
            TextFileProgram.SaveText<T>("ExeFiles.json", GetDefaultValue);             
}

