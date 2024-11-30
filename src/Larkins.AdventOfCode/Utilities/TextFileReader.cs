using System.Text;

namespace Larkins.AdventOfCode.Utilities;

public class TextFileReader
{
    public string ReadAllTextInFile(string filename)
    {
        return File.ReadAllText(filename);
    }

    /// <summary>
    /// Implementation based on: https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string ReadFileByLine(string filePath)
    {
        var stringBuilder = new StringBuilder();
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(filePath);
            //Read the first line of text
            var line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                stringBuilder.AppendLine(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
        
        return stringBuilder.ToString();
    }
}