using System.Text;
using System.Diagnostics;
using ChoETL;
using System.IO;

namespace JSON2CSV;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Syntax();
            }
            else
            {
                Console.WriteLine();

                string str = GetJsonToConvert(args);

                using (var r = ChoJSONReader.LoadText(str))
                {
                    using (var w = new ChoCSVWriter(Console.Out)
                           .WithFirstLineHeader()
                          )
                        w.Write(r);
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    static string GetJsonToConvert(string[] args)
    {
        string readText;

        Debug.Assert(args.Length > 0);

        if (args[0] == "-f")
        {
            readText = File.ReadAllText(args[1], Encoding.UTF8);
        }
        else
        {
            readText = args[0];
        }

        return (readText);
    }

    static void Syntax()
    {
        Console.WriteLine("Syntax:\n");
        Console.WriteLine("JSON2CSV \"<json object>\"");
        Console.WriteLine("JSON2CSV -f \"<samplefile.json>\"");

        Console.WriteLine("\nImportant: Passing JSON on the command line, requires quotes and special characters to be escaped.");
        Console.WriteLine("A samlple JSON object like this\n");
        Console.WriteLine("{\n  \"config\": {\n    \"script\": {\n      \"name\": \"test\",\n      \"dir\": \"D:\\test\",\n      \"destination\": \"M:\\new\\test\",\n      \"params\": \"/b /s /r:3 /w:5\"\n    }\n  }\n}");

        Console.WriteLine("\nWould need to be passed like this on the command line:\n");
        Console.WriteLine("\"{\\\"config\\\":{\\\"script\\\":{\\\"name\\\":\\\"test\\\",\\\"dir\\\":\\\"D:\\\\\\test\\\",\\\"destination\\\":\\\"M:\\\\\\new\\\\\\test\\\",\\\"params\\\":\\\"/b /s /r:3 /w:5\\\"}}}\"");
    }
}
