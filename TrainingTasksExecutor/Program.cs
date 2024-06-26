using DocWorker;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using TrainingTasksExecutor;
using TrainingTasksExecutor.Grouping;
using TrainingTasksExecutor.Http;
using TrainingTasksExecutor.Inheritance;
using TrainingTasksExecutor.JsonSamples;
using TrainingTasksExecutor.OOP;

Console.Title = "Exercises";
Console.ForegroundColor = ConsoleColor.Green;

foreach (var line in File.ReadAllLines(@"C:\Main\Test\a.txt"))
{
    if (!line.Trim().ToUpper().Equals("МАГИСТРАТУРА"))
        File.AppendAllText(@"C:\Main\Test\Faculties.txt", line + "\r\n");
}

Console.WriteLine("\nDone!");

Console.ReadKey();