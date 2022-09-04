using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using static Newtonsoft.Json.JsonConvert;

public class ProgramFilesExecuter
{
   
    /// <summary>
    /// Файлы с расширением *.exe
    /// </summary>    
    public static IEnumerable<string> GetProgramFiles()
    {
        /// имя файла источника
        string FileName = "ExeFiles.json";

        /// если файл устарел (был записан более часа назад)
        if((DateTime.Now - TextFileProgram.GetUpdateTime(FileName)).TotalHours > 1)                    

            // то обновляем данные
            TextFileProgram.SaveText<IEnumerable<string>>(FileName, () => SearchPathProgram.SearchFile("*.exe"));                

        /// считываем данные из файла, либо  выполняем функцию предоставляющую эти данные
        IEnumerable<string> ExeFiles = TextFileProgram.LoadText<IEnumerable<string>>(
            FileName, 
            () => SearchPathProgram.SearchFile("*.exe")
        );       
        return ExeFiles;
    } 

}
